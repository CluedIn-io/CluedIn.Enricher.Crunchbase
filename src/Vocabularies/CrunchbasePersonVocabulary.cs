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
            this.Grouping       = EntityType.Person;

            this.BornOn = this.Add(new VocabularyKey("BornOn"));
            this.BornOnTrustCode = this.Add(new VocabularyKey("BornOnTrustCode"));
            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.DiedOn = this.Add(new VocabularyKey("DiedOn"));
            this.DiedOnTrustCode = this.Add(new VocabularyKey("DiedOnTrustCode"));
            this.FirstName = this.Add(new VocabularyKey("FirstName"));
            this.Gender = this.Add(new VocabularyKey("Gender"));
            this.LastName = this.Add(new VocabularyKey("LastName"));
            this.PermaLink = this.Add(new VocabularyKey("PermaLink"));
            this.ProfileImageUrl = this.Add(new VocabularyKey("ProfileImageUrl"));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
        }

        public VocabularyKey BornOn { get; internal set; }
        public VocabularyKey BornOnTrustCode { get; internal set; }
        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey DiedOn { get; internal set; }
        public VocabularyKey DiedOnTrustCode { get; internal set; }
        public VocabularyKey FirstName { get; internal set; }
        public VocabularyKey Gender { get; internal set; }
        public VocabularyKey LastName { get; internal set; }
        public VocabularyKey PermaLink { get; internal set; }
        public VocabularyKey ProfileImageUrl { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
    }
}
