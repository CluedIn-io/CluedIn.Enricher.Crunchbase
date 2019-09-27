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
    public class CrunchbaseMarketVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseMarketVocabulary()
        {
            this.VocabularyName = "Crunchbase Market";
            this.KeyPrefix      = "crunchbase.market";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Tag;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.PermaLink = this.Add(new VocabularyKey("PermaLink"));
            this.ProfileImageUrl = this.Add(new VocabularyKey("ProfileImageUrl"));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey PermaLink { get; internal set; }
        public VocabularyKey ProfileImageUrl { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
    }
}
