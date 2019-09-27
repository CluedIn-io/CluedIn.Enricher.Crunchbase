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
    public class CrunchbaseAddressVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseAddressVocabulary()
        {
            this.VocabularyName = "Crunchbase Address";
            this.KeyPrefix      = "crunchbase.address";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Infrastructure.Location;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
            this.City = this.Add(new VocabularyKey("City"));
            this.Country = this.Add(new VocabularyKey("Country"));
            this.CountryCode2 = this.Add(new VocabularyKey("CountryCode2"));
            this.CountryCode3 = this.Add(new VocabularyKey("CountryCode3"));
            this.Latitude = this.Add(new VocabularyKey("Latitude"));
            this.Longitude = this.Add(new VocabularyKey("Longitude"));
            this.PostalCode = this.Add(new VocabularyKey("PostalCode"));
            this.Region = this.Add(new VocabularyKey("Region"));
            this.RegionCode2 = this.Add(new VocabularyKey("RegionCode2"));
            this.Street1 = this.Add(new VocabularyKey("Street1"));
            this.Street2 = this.Add(new VocabularyKey("Street2"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
        public VocabularyKey City { get; internal set; }
        public VocabularyKey Country { get; internal set; }
        public VocabularyKey CountryCode2 { get; internal set; }
        public VocabularyKey CountryCode3 { get; internal set; }
        public VocabularyKey Latitude { get; internal set; }
        public VocabularyKey Longitude { get; internal set; }
        public VocabularyKey PostalCode { get; internal set; }
        public VocabularyKey Region { get; internal set; }
        public VocabularyKey RegionCode2 { get; internal set; }
        public VocabularyKey Street1 { get; internal set; }
        public VocabularyKey Street2 { get; internal set; }
    }
}
