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
    public class CrunchbaseNewsItemVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseNewsItemVocabulary()
        {
            this.VocabularyName = "Crunchbase NewsItem";
            this.KeyPrefix      = "crunchbase.news";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.News;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.PermaLink = this.Add(new VocabularyKey("PermaLink"));
            this.ProfileImageUrl = this.Add(new VocabularyKey("ProfileImageUrl"));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
            this.Url = this.Add(new VocabularyKey("Url"));
            this.Author = this.Add(new VocabularyKey("Author"));
            this.PostedOn = this.Add(new VocabularyKey("PostedOn"));
            this.PostedOnTrustCode = this.Add(new VocabularyKey("PostedOnTrustCode"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey PermaLink { get; internal set; }
        public VocabularyKey ProfileImageUrl { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
        public VocabularyKey Url { get; internal set; }
        public VocabularyKey Author { get; internal set; }
        public VocabularyKey PostedOn { get; internal set; }
        public VocabularyKey PostedOnTrustCode { get; internal set; }
    }
}
