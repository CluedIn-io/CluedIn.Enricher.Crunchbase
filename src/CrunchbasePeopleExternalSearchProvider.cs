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
using System.Web;

using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.ExternalSearch.Filters;
using CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies;
using RestSharp;
using CluedIn.Crawling.Helpers;
using CluedIn.Core.Utilities;
using CluedIn.Core.FileTypes;
using CluedIn.ExternalSearch.Providers.Crunchbase.Model;

namespace CluedIn.ExternalSearch.Providers.Crunchbase
{
    /// <summary>The facebook graph external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class CrunchbasePeopleExternalSearchProvider : ExternalSearchProviderBase
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
        public CrunchbasePeopleExternalSearchProvider()
            : base(Constants.ExternalSearchProviders.CrunchbaseId, EntityType.Person, EntityType.Infrastructure.User, EntityType.Infrastructure.Contact)
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

            var existingResults = request.GetQueryResults<PersonProperties>(this).ToList();

            Func<string, bool> nameFilter = value => existingResults.Any(r => string.Equals(r.Data.first_name, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType = request.EntityMetaData.EntityType;
            var jobTitle = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.JobTitle, new HashSet<string>());
            var organiztaion = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Organization, new HashSet<string>());

            var fullName = new HashSet<string>();

            if (!string.IsNullOrEmpty(request.EntityMetaData.Name))
                fullName.Add(request.EntityMetaData.Name);
            if (!string.IsNullOrEmpty(request.EntityMetaData.DisplayName))
                fullName.Add(request.EntityMetaData.DisplayName);

            if (jobTitle.Any() && fullName.Any() && organiztaion.Any())
            {
                var values = fullName.Where(n => n != null)
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

            var isPremium = this.CheckPremium(context, sharedApiToken);
            var url = isPremium ? "people" : "odm-people";

            var request = new RestRequest(string.Format(url), Method.GET);
            request.AddQueryParameter("user_key", sharedApiToken);
            request.AddQueryParameter("query", name);

            var response = client.ExecuteTaskAsync<PersonResponse>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    foreach (var person in response.Data.Data.Items)
                    {
                        person.Properties.uuid = person.Uuid;
                        yield return new ExternalSearchQueryResult<PersonProperties>(query, person.Properties);
                    }
            }
            else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                yield break;
            else if (response.ErrorException != null)
                throw new AggregateException(response.ErrorException.Message, response.ErrorException);
            else
                throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode + "; Content: " + response.Content);
        }

        protected bool CheckPremium(ExecutionContext context, string apiKey)// TODO: This should be executed only once(thread lock)
        {
            return context.ApplicationContext.System.Cache.GetItem<bool>(apiKey, () =>
            {
                var client = new RestClient("https://api.crunchbase.com/v/3");
                var request = new RestRequest("organizations", Method.GET);
                request.AddHeader("user_key", apiKey);
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;

                return false;
            },
            true,
            policy => policy.WithAbsoluteExpiration(DateTimeOffset.UtcNow.AddHours(4)));
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<PersonProperties>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);

            this.PopulateMetadata(clue.Data.EntityData, resultItem);

            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(resultItem.Data.profile_image_url)) //Get Full Quality Image
                {
                    var inArray = StreamUtilies.ReadFully(stream);
                    if (inArray != null)
                    {
                        var rawDataPart = new RawDataPart()
                        {
                            Type = "/RawData/PreviewImage",
                            MimeType = MimeType.Jpeg.Code,
                            FileName = "preview_{0}".FormatWith(code.Key),
                            RawDataMD5 = FileHashUtility.GetMD5Base64String(inArray),
                            RawData = Convert.ToBase64String(inArray)
                        };

                        clue.Details.RawData.Add(rawDataPart);

                        clue.Data.EntityData.PreviewImage = new ImageReferencePart(rawDataPart);
                    }
                }
            }
            catch (Exception)
            {
                //Swallow
            }



            yield return clue;
        }



        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<PersonProperties>();
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
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<PersonProperties> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<PersonProperties> resultItem)
        {
            return new EntityCode(EntityType.Person, this.GetCodeOrigin(), resultItem.Data.uuid);
        }

        /// <summary>Gets the code origin.</summary>
        /// <returns>The code origin</returns>
        private CodeOrigin GetCodeOrigin()
        {
            return CodeOrigin.CluedIn.CreateSpecific("crunchBase");
        }


        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<PersonProperties> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = code.Type;
            metadata.OriginEntityCode = code;

            if (resultItem.Data.also_known_as != null)
                metadata.Aliases.AddRange(resultItem.Data.also_known_as);

            metadata.Name = string.Format("{0} {1}", resultItem.Data.first_name, resultItem.Data.last_name);
            metadata.Description = resultItem.Data.bio.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.BornOn] = resultItem.Data.born_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.BornOnTrustCode] = resultItem.Data.born_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.CreatedAt] = resultItem.Data.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.DiedOn] = resultItem.Data.died_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.DiedOnTrustCode] = resultItem.Data.died_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.FirstName] = resultItem.Data.first_name.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.Gender] = resultItem.Data.gender.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.LastName] = resultItem.Data.last_name.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.PermaLink] = resultItem.Data.permalink.PrintIfAvailable();

            //Download this picture
            metadata.Properties[CrunchbaseVocabulary.Person.ProfileImageUrl] = resultItem.Data.profile_image_url.PrintIfAvailable();

            metadata.Properties[CrunchbaseVocabulary.Person.RoleInvestor] = resultItem.Data.role_investor.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.UpdatedAt] = resultItem.Data.updated_at.PrintIfAvailable();
        
            metadata.Codes.Add(code);

        }
    }
}
