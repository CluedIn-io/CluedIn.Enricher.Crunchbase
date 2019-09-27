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
    public class CrunchbaseFundingRoundVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrunchbasePersonVocabulary"/> class.
        /// </summary>
        public CrunchbaseFundingRoundVocabulary()
        {
            this.VocabularyName = "Crunchbase Funding Round";
            this.KeyPrefix      = "crunchbase.funding";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Activity;

            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt"));
            this.PermaLink = this.Add(new VocabularyKey("PermaLink"));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt"));
            this.AnnouncedOn = this.Add(new VocabularyKey("AnnouncedOn"));
            this.AnnouncedOnTrustCode = this.Add(new VocabularyKey("AnnouncedOnTrustCode"));
            this.ClosedOn = this.Add(new VocabularyKey("ClosedOn"));
            this.ClosedOnTrustCode = this.Add(new VocabularyKey("ClosedOnTrustCode"));
            this.FundingType = this.Add(new VocabularyKey("FundingType"));
            this.MoneyRaisedCurrencyCode = this.Add(new VocabularyKey("MoneyRaisedCurrencyCode"));
            this.MoneyRaised = this.Add(new VocabularyKey("MoneyRaised"));
            this.MoneyRaisedUsd = this.Add(new VocabularyKey("MoneyRaisedUsd"));
            this.Series = this.Add(new VocabularyKey("Series"));
            this.SeriesQualifier = this.Add(new VocabularyKey("SeriesQualifier"));
            this.TargetMoneyRaised = this.Add(new VocabularyKey("TargetMoneyRaised"));
            this.TargetMoneyRaisedCurrencyCode = this.Add(new VocabularyKey("TargetMoneyRaisedCurrencyCode"));
            this.TargetMoneyRaisedUsd = this.Add(new VocabularyKey("TargetMoneyRaisedUsd"));
        }

        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey PermaLink { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
        public VocabularyKey AnnouncedOn { get; internal set; }
        public VocabularyKey AnnouncedOnTrustCode { get; internal set; }
        public VocabularyKey ClosedOn { get; internal set; }
        public VocabularyKey ClosedOnTrustCode { get; internal set; }
        public VocabularyKey FundingType { get; internal set; }
        public VocabularyKey MoneyRaisedCurrencyCode { get; internal set; }
        public VocabularyKey MoneyRaised { get; internal set; }
        public VocabularyKey MoneyRaisedUsd { get; internal set; }
        public VocabularyKey Series { get; internal set; }
        public VocabularyKey SeriesQualifier { get; internal set; }
        public VocabularyKey TargetMoneyRaised { get; internal set; }
        public VocabularyKey TargetMoneyRaisedCurrencyCode { get; internal set; }
        public VocabularyKey TargetMoneyRaisedUsd { get; internal set; }
    }
}
