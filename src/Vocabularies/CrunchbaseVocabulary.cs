// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Vocabularies
{
    /// <summary>The clear bit vocabulary.</summary>
    public static class CrunchbaseVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="CrunchbaseVocabulary" /> class.
        /// </summary>
        static CrunchbaseVocabulary()
        {
            Organization = new CrunchbaseOrganizationVocabulary();
            Person = new CrunchbasePersonVocabulary();
            Website = new CrunchbaseWebsiteVocabulary();
            Product = new CrunchbaseProductVocabulary();
            Address = new CrunchbaseAddressVocabulary();
            NewsItem = new CrunchbaseNewsItemVocabulary();
            Image = new CrunchbaseImageVocabulary();
            FundingRound = new CrunchbaseFundingRoundVocabulary();
            Market = new CrunchbaseMarketVocabulary();
        }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        public static CrunchbaseOrganizationVocabulary Organization { get; private set; }

        public static CrunchbasePersonVocabulary Person { get; private set; }

        public static CrunchbaseWebsiteVocabulary Website { get; private set; }

        public static CrunchbaseProductVocabulary Product { get; private set; }

        public static CrunchbaseAddressVocabulary Address { get; private set; }

        public static CrunchbaseNewsItemVocabulary NewsItem { get; private set; }

        public static CrunchbaseImageVocabulary Image { get; private set; }

        public static CrunchbaseFundingRoundVocabulary FundingRound { get; private set; }

        public static CrunchbaseMarketVocabulary Market { get; private set; }
    }
}