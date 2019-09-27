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
using ExternalSearch.Providers.Crunchbase.Model;
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
            //var organizationName = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website, new HashSet<string>());

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

            var isPremium = this.CheckPremium(context, sharedApiToken);
            var url = isPremium ? "organizations" : "odm-organizations";

            var request = new RestRequest(string.Format(url), Method.GET);
            request.AddQueryParameter("user_key", sharedApiToken);
            request.AddQueryParameter("domain_name", name);


            var response = client.ExecuteTaskAsync<OrganizationResponse>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                    yield return new ExternalSearchQueryResult<OrganizationResponse>(query, response.Data);
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
            var resultItem = result.As<OrganizationResponse>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);

            this.PopulateMetadata(clue.Data.EntityData, resultItem);

            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(resultItem.Data.Data.Properties.ProfileImageUrl)) //Get Full Quality Image
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

            //foreach(var relationship in resultItem.Data.Data.Relationships.AcquiredBy.)

            foreach (var relationship in resultItem.Data.Data.Relationships.Acquisitions.Items)
            {
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Categories.Items)
            {
                var relationshipCode = this.GetMarketOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateCategoryItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Competitors.Items)
            {
                var relationshipCode = this.GetItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Customers.Items)
            {
                var relationshipCode = this.GetItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.FundingRounds.Items)
            {
                var relationshipCode = this.GetFundingRoundOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateFundingRoundMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            //foreach (var relationship in resultItem.Data.Data.Relationships.Funds.Items)
            //{
            //    var relationshipCode = this.GetItemOriginEntityCode(relationship);

            //    var relationshipClue = new Clue(relationshipCode, context.Organization);

            //    this.PopulateItemMetadata(relationshipClue.Data.EntityData, relationship);
            //}

            foreach (var relationship in resultItem.Data.Data.Relationships.Images.Items)
            {
                var relationshipCode = this.GetImageOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateImageMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Investments.Items)
            {
                var relationshipCode = this.GetItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Investors.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);
                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            //foreach (var relationship in resultItem.Data.Data.Relationships.Ipo.Items)
            //{
            //    var relationshipCode = this.GetItemOriginEntityCode(relationship);

            //    var relationshipClue = new Clue(relationshipCode, context.Organization);

            //    this.PopulateItemMetadata(relationshipClue.Data.EntityData, relationship);
            //}

            foreach (var relationship in resultItem.Data.Data.Relationships.News.Items)
            {
                var relationshipCode = this.GetNewsOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateNewsMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Offices.Items)
            {
                var relationshipCode = this.GetAddressOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateAddressMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Products.Items)
            {
                var relationshipCode = this.GetProductItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateProductItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.SubOrganizations.Items)
            {
                var relationshipCode = this.GetItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }

            //foreach (var relationship in resultItem.Data.Data.Relationships.Videos.Items)
            //{
            //    var relationshipCode = this.GetItemOriginEntityCode(relationship);

            //    var relationshipClue = new Clue(relationshipCode, context.Organization);

            //    this.PopulateItemMetadata(relationshipClue.Data.EntityData, relationship);
            //}

            foreach (var relationship in resultItem.Data.Data.Relationships.Websites.Items)
            {
                var relationshipCode = this.GetWebOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulateWebsiteMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                yield return relationshipClue;
            }


            foreach (var relationship in resultItem.Data.Data.Relationships.BoardMembersAndAdvisors.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);
                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.CurrentTeam.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Founders.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Investors.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            foreach (var relationship in resultItem.Data.Data.Relationships.Members.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            //foreach (var relationship in resultItem.Data.Data.Relationships.Memberships.Items)
            //{
            //    var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

            //    var relationshipClue = new Clue(relationshipCode, context.Organization);

            //    this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship);
            //}

            //foreach (var relationship in resultItem.Data.Data.Relationships.OwnedBy.Items)
            //{
            //    var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

            //    var relationshipClue = new Clue(relationshipCode, context.Organization);

            //    this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship);

            //this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);
            //}

            foreach (var relationship in resultItem.Data.Data.Relationships.PastTeam.Items)
            {
                var relationshipCode = this.GetPersonItemOriginEntityCode(relationship);

                var relationshipClue = new Clue(relationshipCode, context.Organization);

                this.PopulatePersonItemMetadata(relationshipClue.Data.EntityData, relationship, resultItem);

                this.DownloadPreviewImage(context, new Uri(relationship.Properties.profile_image_url), relationshipClue);

                yield return relationshipClue;
            }

            yield return clue;
        }

        private void PopulateWebsiteMetadata(IEntityMetadataPart metadata, WebPresence relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetWebOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Infrastructure.Site;
            metadata.Name = relationship.website_name;
            metadata.OriginEntityCode = code;
            
            metadata.Properties[CrunchbaseVocabulary.Website.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Website.UpdatedAt] = relationship.updated_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Website.Url] = relationship.url.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Website.WebsiteType] = relationship.website_type.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateProductItemMetadata(IEntityMetadataPart metadata, Product relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetProductItemOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Product;
            metadata.Name = relationship.name;
            metadata.Description = relationship.short_description;
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.Product.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.UpdatedAt] = relationship.updated_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.ClosedOn] = relationship.closed_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.ClosedOnTrustCode] = relationship.closed_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.HomepageUrl] = relationship.homepage_url.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.LaunchedOn] = relationship.launched_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.LaunchedOnTrustCode] = relationship.launched_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.LifeCycleStage] = relationship.lifecycle_stage.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.Permalink] = relationship.permalink.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.ProfileImageUrl] = relationship.profile_image_url.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Product.WebPath] = relationship.web_path.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateAddressMetadata(IEntityMetadataPart metadata, Address relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetAddressOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Infrastructure.Location;
            metadata.Name = relationship.name.ToString();
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.Address.City] = relationship.city.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Country] = relationship.country.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.CountryCode2] = relationship.country_code2.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.CountryCode3] = relationship.country_code3.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Latitude] = relationship.latitude.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Longitude] = relationship.longitude.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.PostalCode] = relationship.postal_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Region] = relationship.region.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.RegionCode2] = relationship.region_code2.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Street1] = relationship.street_1.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.Street2] = relationship.street_2.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Address.UpdatedAt] = relationship.updated_at.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateNewsMetadata(IEntityMetadataPart metadata, NewsItem relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetNewsOriginEntityCode(relationship);

            metadata.EntityType = EntityType.News;
            metadata.Name = relationship.title;
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.NewsItem.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.NewsItem.UpdatedAt] = relationship.updated_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.NewsItem.Url] = relationship.url.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.NewsItem.Author] = relationship.author.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.NewsItem.PostedOn] = relationship.posted_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.NewsItem.PostedOnTrustCode] = relationship.posted_on_trust_code.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateImageMetadata(IEntityMetadataPart metadata, Image relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetImageOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Images.Image;
            metadata.Name = relationship.asset_path;
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.Image.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.UpdatedAt] = relationship.updated_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.AssetUrl] = relationship.asset_url.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.ContentType] = relationship.content_type.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.FileSize] = relationship.filesize.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.Height] = relationship.height.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Image.Width] = relationship.width.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateFundingRoundMetadata(IEntityMetadataPart metadata, FundingRound relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetFundingRoundOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Activity;
            metadata.Name = relationship.series;
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.FundingRound.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.UpdatedAt] = relationship.updated_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.AnnouncedOn] = relationship.announced_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.AnnouncedOnTrustCode] = relationship.announced_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.ClosedOn] = relationship.closed_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.ClosedOnTrustCode] = relationship.closed_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.FundingType] = relationship.funding_type.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.MoneyRaisedCurrencyCode] = relationship.money_raised_currency_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.MoneyRaised] = relationship.money_raised.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.MoneyRaisedUsd] = relationship.money_raised_usd.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.PermaLink] = relationship.permalink.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.Series] = relationship.series.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.SeriesQualifier] = relationship.series_qualifier.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.TargetMoneyRaised] = relationship.target_money_raised.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.TargetMoneyRaisedCurrencyCode] = relationship.target_money_raised_currency_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.FundingRound.TargetMoneyRaisedUsd] = relationship.target_money_raised_usd.PrintIfAvailable();


            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulateCategoryItemMetadata(IEntityMetadataPart metadata, Market relationship, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetMarketOriginEntityCode(relationship);

            metadata.EntityType = EntityType.Tag;
            metadata.Name = relationship.name;
            metadata.OriginEntityCode = code;

            metadata.Properties[CrunchbaseVocabulary.Market.CreatedAt] = relationship.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Market.UpdatedAt] = relationship.updated_at.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<OrganizationResponse>();
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
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<OrganizationResponse> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<OrganizationResponse> resultItem)
        {
            return new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Data.Data.Uuid);
        }

        private EntityCode GetImageOriginEntityCode(Image resultItem)
        {
            return new EntityCode(EntityType.Images.Image, this.GetCodeOrigin(), resultItem.asset_url);
        }

        private EntityCode GetItemOriginEntityCode(Item resultItem)
        {
            return new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Uuid);
        }

        private EntityCode GetPersonItemOriginEntityCode(Person resultItem)
        {
            return new EntityCode(EntityType.Person, this.GetCodeOrigin(), resultItem.Uuid);
        }

        private EntityCode GetWebOriginEntityCode(WebPresence resultItem)
        {
            return new EntityCode(EntityType.Infrastructure.Site, this.GetCodeOrigin(), resultItem.url);
        }

        private EntityCode GetProductItemOriginEntityCode(Product resultItem)
        {
            return new EntityCode(EntityType.Product, this.GetCodeOrigin(), resultItem.name);
        }

        private EntityCode GetNewsOriginEntityCode(NewsItem resultItem)
        {
            return new EntityCode(EntityType.Product, this.GetCodeOrigin(), resultItem.url);
        }

        private EntityCode GetFundingRoundOriginEntityCode(FundingRound resultItem)
        {
            return new EntityCode(EntityType.Activity, this.GetCodeOrigin(), resultItem.permalink);
        }

        private EntityCode GetAddressOriginEntityCode(Address resultItem)
        {
            return new EntityCode(EntityType.Infrastructure.Location, this.GetCodeOrigin(), resultItem.city_web_path);
        }

        private EntityCode GetMarketOriginEntityCode(Market resultItem)
        {
            return new EntityCode(EntityType.Tag, this.GetCodeOrigin(), resultItem.name);
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
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<OrganizationResponse> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = EntityType.Organization;
            metadata.Name = resultItem.Data.Data.Properties.Name;
            metadata.Description = resultItem.Data.Data.Properties.Description;
            metadata.OriginEntityCode = code;

            metadata.Aliases.AddRange(resultItem.Data.Data.Properties.AlsoKnownAs);

            metadata.Properties[CrunchbaseVocabulary.Organization.ShortDescription] = resultItem.Data.Data.Properties.ShortDescription.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.PrimaryRole] = resultItem.Data.Data.Properties.PrimaryRole.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleCompany] = resultItem.Data.Data.Properties.RoleCompany.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleInvestor] = resultItem.Data.Data.Properties.RoleInvestor.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleGroup] = resultItem.Data.Data.Properties.RoleGroup.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleSchool] = resultItem.Data.Data.Properties.RoleSchool.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.FoundedOn] = resultItem.Data.Data.Properties.FoundedOn.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.FoundedOnTrustCode] = resultItem.Data.Data.Properties.FoundedOnTrustCode.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.IsClosed] = resultItem.Data.Data.Properties.IsClosed.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.ClosedOn] = resultItem.Data.Data.Properties.ClosedOn.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.ClosedOnTrustCode] = resultItem.Data.Data.Properties.ClosedOnTrustCode.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumEmployeesMin] = resultItem.Data.Data.Properties.NumEmployeesMin.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumEmployeesMax] = resultItem.Data.Data.Properties.NumEmployeesMax.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.StockExchange] = resultItem.Data.Data.Properties.StockExchange.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.StockSymbol] = resultItem.Data.Data.Properties.StockSymbol.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.TotalFundingUsd] = resultItem.Data.Data.Properties.TotalFundingUsd.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumberOfInvestments] = resultItem.Data.Data.Properties.NumberOfInvestments.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.HomepageUrl] = resultItem.Data.Data.Properties.HomepageUrl.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.CreatedAt] = resultItem.Data.Data.Properties.CreatedAt.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.UpdatedAt] = resultItem.Data.Data.Properties.UpdatedAt.PrintIfAvailable();

            //resultItem.Data.Data.Relationships.Headquarters.Item
            //resultItem.Data.Data.Relationships.PrimaryImage.Item
         
            metadata.Codes.Add(code);

        }

        private void PopulateMetadata(IEntityMetadata metadata, Item item, IExternalSearchQueryResult<OrganizationResponse> resultItem)
        {
            var code = this.GetItemOriginEntityCode(item);

            metadata.EntityType = EntityType.Organization;
            metadata.Name = item.Properties.Name;
            metadata.Description = item.Properties.Description;
            metadata.OriginEntityCode = code;

            metadata.Aliases.AddRange(item.Properties.AlsoKnownAs);

            metadata.Properties[CrunchbaseVocabulary.Organization.ShortDescription] = item.Properties.ShortDescription.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.PrimaryRole] = item.Properties.PrimaryRole.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleCompany] = item.Properties.RoleCompany.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleInvestor] = item.Properties.RoleInvestor.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleGroup] = item.Properties.RoleGroup.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.RoleSchool] = item.Properties.RoleSchool.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.FoundedOn] = item.Properties.FoundedOn.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.FoundedOnTrustCode] = item.Properties.FoundedOnTrustCode.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.IsClosed] = item.Properties.IsClosed.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.ClosedOn] = item.Properties.ClosedOn.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.ClosedOnTrustCode] = item.Properties.ClosedOnTrustCode.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumEmployeesMin] = item.Properties.NumEmployeesMin.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumEmployeesMax] = item.Properties.NumEmployeesMax.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.StockExchange] = item.Properties.StockExchange.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.StockSymbol] = item.Properties.StockSymbol.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.TotalFundingUsd] = item.Properties.TotalFundingUsd.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.NumberOfInvestments] = item.Properties.NumberOfInvestments.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.HomepageUrl] = item.Properties.HomepageUrl.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.CreatedAt] = item.Properties.CreatedAt.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Organization.UpdatedAt] = item.Properties.UpdatedAt.PrintIfAvailable();

            //resultItem.Data.Data.Relationships.Headquarters.Item
            //resultItem.Data.Data.Relationships.PrimaryImage.Item
            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(resultItem));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);

        }


        private void PopulateCategoryItemMetadata(IEntityMetadata metadata, Market resultItem, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetMarketOriginEntityCode(resultItem);

            metadata.EntityType = code.Type;

            metadata.Name = resultItem.name;
            metadata.OriginEntityCode = code;
            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);
            metadata.Codes.Add(code);
        }

        private void PopulatePersonItemMetadata(IEntityMetadata metadata, Person resultItem, IExternalSearchQueryResult<OrganizationResponse> company)
        {
            var code = this.GetPersonItemOriginEntityCode(resultItem);

            metadata.EntityType = code.Type;

            metadata.Name = string.Format("{0} {1}", resultItem.Properties.first_name, resultItem.Properties.last_name);
            metadata.Description = resultItem.Properties.bio.ToString();
            metadata.OriginEntityCode = code;

            metadata.Aliases.AddRange(resultItem.Properties.also_known_as);

            metadata.Properties[CrunchbaseVocabulary.Person.BornOn] = resultItem.Properties.born_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.BornOnTrustCode] = resultItem.Properties.born_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.CreatedAt] = resultItem.Properties.created_at.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.DiedOn] = resultItem.Properties.died_on.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.DiedOnTrustCode] = resultItem.Properties.died_on_trust_code.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.FirstName] = resultItem.Properties.first_name.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.Gender] = resultItem.Properties.gender.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.LastName] = resultItem.Properties.last_name.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.PermaLink] = resultItem.Properties.permalink.PrintIfAvailable();

            //Download this picture
            metadata.Properties[CrunchbaseVocabulary.Person.ProfileImageUrl] = resultItem.Properties.profile_image_url.PrintIfAvailable();

            metadata.Properties[CrunchbaseVocabulary.Person.RoleInvestor] = resultItem.Properties.role_investor.PrintIfAvailable();
            metadata.Properties[CrunchbaseVocabulary.Person.UpdatedAt] = resultItem.Properties.updated_at.PrintIfAvailable();

            var from = new EntityReference(code);
            var to = new EntityReference(this.GetOriginEntityCode(company));

            var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);

            metadata.OutgoingEdges.Add(edge);

            metadata.Codes.Add(code);

        }
    }
}
