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
    public class CrunchbaseImageVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseImageVocabulary()
        {
            this.VocabularyName = "Crunchbase Image";
            this.KeyPrefix      = "crunchbase.image";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Images.Image;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.PermaLink = this.Add(new VocabularyKey("PermaLink"));
            this.ProfileImageUrl = this.Add(new VocabularyKey("ProfileImageUrl"));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
            this.AssetUrl = this.Add(new VocabularyKey("AssetUrl"));
            this.ContentType = this.Add(new VocabularyKey("ContentType"));
            this.FileSize = this.Add(new VocabularyKey("FileSize"));
            this.Height = this.Add(new VocabularyKey("Height"));
            this.Width = this.Add(new VocabularyKey("Width"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey PermaLink { get; internal set; }
        public VocabularyKey ProfileImageUrl { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
        public VocabularyKey AssetUrl { get; internal set; }
        public VocabularyKey ContentType { get; internal set; }
        public VocabularyKey FileSize { get; internal set; }
        public VocabularyKey Height { get; internal set; }
        public VocabularyKey Width { get; internal set; }
    }
}
