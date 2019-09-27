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
    public class CrunchbasePersonVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbasePersonVocabulary()
        {
            this.VocabularyName = "Crunchbase Person";
            this.KeyPrefix      = "crunchbase.person";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Infrastructure.User;

            this.OrganizationPermalink = this.Add(new VocabularyKey("organizationPermalink"));
            this.Permalink             = this.Add(new VocabularyKey("permalink"));
            this.ApiPath               = this.Add(new VocabularyKey("apiPath"));
            this.WebPath               = this.Add(new VocabularyKey("webPath"));
            this.ApiUrl                = this.Add(new VocabularyKey("apiUrl"));
            this.FirstName             = this.Add(new VocabularyKey("firstName"));
            this.LastName              = this.Add(new VocabularyKey("lastName"));
            this.Gender                = this.Add(new VocabularyKey("gender"));
            this.Title                 = this.Add(new VocabularyKey("title"));
            this.OrganizationApiPath   = this.Add(new VocabularyKey("organizationApiPath"));
            this.OrganizationWebPath   = this.Add(new VocabularyKey("organizationWebPath"));
            this.OrganizationName      = this.Add(new VocabularyKey("organizationName"));
            this.ProfileImageUrl       = this.Add(new VocabularyKey("profileImageUrl"));
            this.HomepageUrl           = this.Add(new VocabularyKey("homepageUrl"));
            this.FacebookUrl           = this.Add(new VocabularyKey("facebookUrl"));
            this.TwitterUrl            = this.Add(new VocabularyKey("twitterUrl"));
            this.LinkedinUrl           = this.Add(new VocabularyKey("linkedinUrl"));
            this.CityName              = this.Add(new VocabularyKey("cityName"));
            this.RegionName            = this.Add(new VocabularyKey("regionName"));
            this.CountryCode           = this.Add(new VocabularyKey("countryCode"));
            this.CreatedAt             = this.Add(new VocabularyKey("createdAt"));
            this.UpdatedAt             = this.Add(new VocabularyKey("updatedAt"));

            this.AddMapping(this.FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FirstName);
            this.AddMapping(this.LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LastName);
            this.AddMapping(this.Gender, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Gender);
            this.AddMapping(this.HomepageUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Website);
            this.AddMapping(this.FacebookUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialFacebook);
            this.AddMapping(this.TwitterUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialTwitter);
            this.AddMapping(this.LinkedinUrl, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialLinkedIn);
            this.AddMapping(this.CityName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.HomeAddressCity);
            this.AddMapping(this.CountryCode, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.HomeAddressCountryCode);
            this.AddMapping(this.OrganizationName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName);
        }

        public VocabularyKey OrganizationPermalink { get; internal set; }
        public VocabularyKey Permalink             { get; internal set; }
        public VocabularyKey ApiPath               { get; internal set; }
        public VocabularyKey WebPath               { get; internal set; }
        public VocabularyKey ApiUrl                { get; internal set; }
        public VocabularyKey FirstName             { get; internal set; }
        public VocabularyKey LastName              { get; internal set; }
        public VocabularyKey Gender                { get; internal set; }
        public VocabularyKey Title                 { get; internal set; }
        public VocabularyKey OrganizationApiPath   { get; internal set; }
        public VocabularyKey OrganizationWebPath   { get; internal set; }
        public VocabularyKey OrganizationName      { get; internal set; }
        public VocabularyKey ProfileImageUrl       { get; internal set; }
        public VocabularyKey HomepageUrl           { get; internal set; }
        public VocabularyKey FacebookUrl           { get; internal set; }
        public VocabularyKey TwitterUrl            { get; internal set; }
        public VocabularyKey LinkedinUrl           { get; internal set; }
        public VocabularyKey CityName              { get; internal set; }
        public VocabularyKey RegionName            { get; internal set; }
        public VocabularyKey CountryCode           { get; internal set; }
        public VocabularyKey CreatedAt             { get; internal set; }
        public VocabularyKey UpdatedAt             { get; internal set; }
    }
}
