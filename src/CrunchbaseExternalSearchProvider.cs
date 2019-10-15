// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacebookGraphExternalSearchProvider.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FacebookGraphExternalSearchProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.ExternalSearch.Filters;
using CluedIn.ExternalSearch.Providers.Crunchbase.Model;
using CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies;
using RestSharp;
using CluedIn.Crawling.Helpers;
using CluedIn.Core.Utilities;
using CluedIn.Core.FileTypes;

namespace CluedIn.ExternalSearch.Providers.Crunchbase
{
    /// <summary>The facebook graph external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class CrunchbaseExternalSearchProvider : ExternalSearchProviderBase
    {
        /**********************************************************************************************************
         * FIELDS
         **********************************************************************************************************/

        /// <summary>The shared API tokens</summary>
        private List<string> sharedApiTokens = new List<string>()
            {
                "d83a882b10484899a4b3de58854f03ab"
            };

        /// <summary>The shared API tokens index</summary>
        private int sharedApiTokensIdx = 0;

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookGraphExternalSearchProvider" /> class.
        /// </summary>
        public CrunchbaseExternalSearchProvider()
            : base(Constants.ExternalSearchProviders.CrunchbaseId, EntityType.Organization)
        {
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Builds the queries.</summary>
        /// <param name="context">The context.</param>
        /// <param name="request">The request.</param>
        /// <returns>The search queries.</returns>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<OrganizationResponse>(this).ToList();

            Func<string, bool> nameFilter = value => OrganizationFilters.NameFilter(context, value) || existingResults.Any(r => string.Equals(r.Data.Data.Items.First().Properties.HomepageUrl, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType = request.EntityMetaData.EntityType;
            var website = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website, new HashSet<string>());

            var organizationName = new HashSet<string>();

            if (!string.IsNullOrEmpty(request.EntityMetaData.Name))
                organizationName.Add(request.EntityMetaData.Name);
            if (!string.IsNullOrEmpty(request.EntityMetaData.DisplayName))
                organizationName.Add(request.EntityMetaData.DisplayName);

            if (organizationName.Any())
            {
                var values = organizationName.Where(n => n != null)
                                             .GetOrganizationNameVariants()
                                             .Select(NameNormalization.Normalize)
                                             .ToHashSet();

                foreach (var value in values.Where(v => !nameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        /// <summary>Executes the search.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <returns>The results.</returns>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var name = query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();

            if (string.IsNullOrEmpty(name))
                yield break;

            name = HttpUtility.UrlEncode(name);

            var client = new RestClient("https://api.crunchbase.com/v/3");

            string sharedApiToken;

            lock (this)
            {
                sharedApiToken = this.sharedApiTokens[this.sharedApiTokensIdx++];

                if (this.sharedApiTokensIdx >= this.sharedApiTokens.Count)
                    this.sharedApiTokensIdx = 0;
            }

            var url = "organizations";

            var request = new RestRequest(string.Format(url), Method.GET);
            request.AddQueryParameter("user_key", sharedApiToken);
            request.AddQueryParameter("name", name);

            var response = client.ExecuteTaskAsync<OrganizationResponse>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data?.Data?.Items != null)
                    foreach (var item in response.Data.Data.Items)
                        yield return new ExternalSearchQueryResult<OrganizationItem>(query, item);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                yield break;
            else if (response.ErrorException != null)
                throw new AggregateException(response.ErrorException.Message, response.ErrorException);
            else
                throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode + "; Content: " + response.Content);
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<OrganizationItem>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);

            this.PopulateMetadata(clue.Data.EntityData, resultItem);

            var c = clue.Serialize();

            yield return clue;
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<OrganizationItem>();
            return this.CreateMetadata(resultItem);
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return null;
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<OrganizationItem> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<OrganizationItem> resultItem)
        {
            return new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Data.Uuid);
        }

        /// <summary>Gets the code origin.</summary>
        /// <returns>The code origin</returns>
        private CodeOrigin GetCodeOrigin()
        {
            return CodeOrigin.CluedIn.CreateSpecific("crunchBase");
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<OrganizationItem> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = EntityType.Organization;
            metadata.Name = resultItem.Data?.Properties?.Name;
            metadata.Description = resultItem.Data?.Properties?.ShortDescription;
            metadata.OriginEntityCode = code;

            if (resultItem.Data != null)
            {
                metadata.Properties[CrunchbaseVocabulary.Organization.Type] = resultItem.Data.Type?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.Uuid] = resultItem.Data.Uuid?.PrintIfAvailable();
            }
            
            if (resultItem.Data?.Properties != null)
            {
                metadata.Properties[CrunchbaseVocabulary.Organization.ShortDescription] = resultItem.Data.Properties.ShortDescription?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.Permalink] = resultItem.Data.Properties.Permalink?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.ApiPath] = resultItem.Data.Properties.ApiPath?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.WebPath] = resultItem.Data.Properties.WebPath?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.ApiUrl] = resultItem.Data.Properties.ApiUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.Name] = resultItem.Data.Properties.Name?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.StockExchange] = resultItem.Data.Properties?.StockExchange.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.StockSymbol] = resultItem.Data.Properties.StockSymbol?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.PrimaryRole] = resultItem.Data.Properties.PrimaryRole?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.ProfileImageUrl] = resultItem.Data.Properties.ProfileImageUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.Domain] = resultItem.Data.Properties.Domain?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.HomepageUrl] = resultItem.Data.Properties.HomepageUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.FacebookUrl] = resultItem.Data.Properties.FacebookUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.TwitterUrl] = resultItem.Data.Properties.TwitterUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.LinkedinUrl] = resultItem.Data.Properties.LinkedinUrl?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.CityName] = resultItem.Data.Properties.CityName?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.RegionName] = resultItem.Data.Properties.RegionName?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.CountryCode] = resultItem.Data.Properties.CountryCode?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.CreatedAt] = resultItem.Data.Properties.CreatedAt?.PrintIfAvailable();
                metadata.Properties[CrunchbaseVocabulary.Organization.UpdatedAt] = resultItem.Data.Properties.UpdatedAt?.PrintIfAvailable();
            }

            metadata.Codes.Add(code);
        }
    }
}
