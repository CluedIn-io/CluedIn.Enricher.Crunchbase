// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacebookGraphOrganizationVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FacebookGraphOrganizationVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies
{
    /// <summary>The facebook graph organization vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class CrunchbaseOrganizationVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbaseOrganizationVocabulary"/> class.
        /// </summary>
        public CrunchbaseOrganizationVocabulary()
        {
            this.VocabularyName = "Crunchbase Organization";
            this.KeyPrefix      = "crunchbase.organization";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Organization;

            this.Type             = this.Add(new VocabularyKey("type", VocabularyKeyDataType.Text));
            this.Uuid             = this.Add(new VocabularyKey("uuid", VocabularyKeyDataType.Guid));
            this.Permalink        = this.Add(new VocabularyKey("permalink", VocabularyKeyDataType.Uri));
            this.ShortDescription = this.Add(new VocabularyKey("shortDescription", VocabularyKeyDataType.Text));
            this.ApiPath          = this.Add(new VocabularyKey("apiPath", VocabularyKeyDataType.Uri));
            this.WebPath          = this.Add(new VocabularyKey("webPath", VocabularyKeyDataType.Uri));
            this.ApiUrl           = this.Add(new VocabularyKey("apiUrl", VocabularyKeyDataType.Uri));
            this.Name             = this.Add(new VocabularyKey("name", VocabularyKeyDataType.Text));
            this.StockExchange    = this.Add(new VocabularyKey("stockExchange", VocabularyKeyDataType.Text));
            this.StockSymbol      = this.Add(new VocabularyKey("stockSymbol", VocabularyKeyDataType.Text));
            this.PrimaryRole      = this.Add(new VocabularyKey("primaryRole", VocabularyKeyDataType.Text));
            this.ProfileImageUrl  = this.Add(new VocabularyKey("profileImageUrl", VocabularyKeyDataType.Uri));
            this.Domain           = this.Add(new VocabularyKey("domain", VocabularyKeyDataType.Uri));
            this.HomepageUrl      = this.Add(new VocabularyKey("homepageUrl", VocabularyKeyDataType.Uri));
            this.FacebookUrl      = this.Add(new VocabularyKey("facebookUrl", VocabularyKeyDataType.Uri));
            this.TwitterUrl       = this.Add(new VocabularyKey("twitterUrl", VocabularyKeyDataType.Uri));
            this.LinkedinUrl      = this.Add(new VocabularyKey("linkedInUrl", VocabularyKeyDataType.Uri));
            this.CityName         = this.Add(new VocabularyKey("cityName", VocabularyKeyDataType.Text));
            this.RegionName       = this.Add(new VocabularyKey("regionName", VocabularyKeyDataType.Text));
            this.CountryCode      = this.Add(new VocabularyKey("countryCode", VocabularyKeyDataType.Text));
            this.CreatedAt        = this.Add(new VocabularyKey("createdAt", VocabularyKeyDataType.DateTime));
            this.UpdatedAt        = this.Add(new VocabularyKey("updatedAt", VocabularyKeyDataType.DateTime));

            this.AddMapping(this.Name, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName);
            this.AddMapping(this.CountryCode, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCountryCode);
            this.AddMapping(this.CityName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
            this.AddMapping(this.HomepageUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);

            this.AddMapping(this.TwitterUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Twitter);
            this.AddMapping(this.FacebookUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Facebook);
            this.AddMapping(this.LinkedinUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.LinkedIn);
        }

        public VocabularyKey ShortDescription { get; internal set; }
        public VocabularyKey Type             { get; internal set; }
        public VocabularyKey Uuid             { get; internal set; }
        public VocabularyKey Permalink        { get; internal set; }
        public VocabularyKey ApiPath          { get; internal set; }
        public VocabularyKey WebPath          { get; internal set; }
        public VocabularyKey ApiUrl           { get; internal set; }
        public VocabularyKey Name             { get; internal set; }
        public VocabularyKey StockExchange    { get; internal set; }
        public VocabularyKey StockSymbol      { get; internal set; }
        public VocabularyKey PrimaryRole      { get; internal set; }
        public VocabularyKey ProfileImageUrl  { get; internal set; }
        public VocabularyKey Domain           { get; internal set; }
        public VocabularyKey HomepageUrl      { get; internal set; }
        public VocabularyKey FacebookUrl      { get; internal set; }
        public VocabularyKey TwitterUrl       { get; internal set; }
        public VocabularyKey LinkedinUrl      { get; internal set; }
        public VocabularyKey CityName         { get; internal set; }
        public VocabularyKey RegionName       { get; internal set; }
        public VocabularyKey CountryCode      { get; internal set; }
        public VocabularyKey CreatedAt        { get; internal set; }
        public VocabularyKey UpdatedAt        { get; internal set; }
    }
}
