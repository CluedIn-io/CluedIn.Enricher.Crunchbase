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
    public class CrunchbaseProductVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseProductVocabulary()
        {
            this.VocabularyName = "Crunchbase Product";
            this.KeyPrefix      = "crunchbase.product";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Product;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.ProfileImageUrl = this.Add(new VocabularyKey("ProfileImageUrl"));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
            this.Permalink = this.Add(new VocabularyKey("Permalink"));


            this.ClosedOn = this.Add(new VocabularyKey("ClosedOn"));
            this.ClosedOnTrustCode = this.Add(new VocabularyKey("ClosedOnTrustCode"));
            this.HomepageUrl = this.Add(new VocabularyKey("HomepageUrl"));
            this.LaunchedOn = this.Add(new VocabularyKey("LaunchedOn"));
            this.LaunchedOnTrustCode = this.Add(new VocabularyKey("LaunchedOnTrustCode"));
            this.LifeCycleStage = this.Add(new VocabularyKey("LifeCycleStage"));
            this.WebPath = this.Add(new VocabularyKey("WebPath"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey Permalink { get; internal set; }
        public VocabularyKey ProfileImageUrl { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
        public VocabularyKey ClosedOn { get; internal set; }
        public VocabularyKey ClosedOnTrustCode { get; internal set; }
        public VocabularyKey HomepageUrl { get; internal set; }
        public VocabularyKey LaunchedOn { get; internal set; }
        public VocabularyKey LaunchedOnTrustCode { get; internal set; }
        public VocabularyKey LifeCycleStage { get; internal set; }
        public VocabularyKey WebPath { get; internal set; }
    }
}
