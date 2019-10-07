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

            this.Description         = this.Add(new VocabularyKey("description"));
            this.Category            = this.Add(new VocabularyKey("category"));
            this.Mission             = this.Add(new VocabularyKey("mission"));
            this.AcquiredCount = this.Add(new VocabularyKey("acquiredCount", VocabularyKeyDataType.Integer));
            this.Rounds = this.Add(new VocabularyKey("rounds", VocabularyKeyDataType.Integer));
            this.Valuation = this.Add(new VocabularyKey("valuation", VocabularyKeyDataType.Number));
            this.FundingTotal = this.Add(new VocabularyKey("fundingTotal", VocabularyKeyDataType.Number));
            this.CurrentRound = this.Add(new VocabularyKey("currentRound", VocabularyKeyDataType.Integer));
            this.IsIpo = this.Add(new VocabularyKey("isIpo", VocabularyKeyDataType.Boolean));
            this.Categories = this.Add(new VocabularyKey("categories", VocabularyKeyDataType.Text));
            this.ShortDescription = this.Add(new VocabularyKey("ShortDescription", VocabularyKeyDataType.Text));
            this.PrimaryRole = this.Add(new VocabularyKey("PrimaryRole", VocabularyKeyDataType.Text));
            this.RoleCompany = this.Add(new VocabularyKey("RoleCompany", VocabularyKeyDataType.Text));
            this.RoleInvestor = this.Add(new VocabularyKey("RoleInvestor", VocabularyKeyDataType.Text));
            this.RoleGroup = this.Add(new VocabularyKey("RoleGroup", VocabularyKeyDataType.Text));
            this.RoleSchool = this.Add(new VocabularyKey("RoleSchool", VocabularyKeyDataType.Text));
            this.FoundedOn = this.Add(new VocabularyKey("FoundedOn", VocabularyKeyDataType.Text));
            this.FoundedOnTrustCode = this.Add(new VocabularyKey("FoundedOnTrustCode", VocabularyKeyDataType.Text));
            this.IsClosed = this.Add(new VocabularyKey("IsClosed", VocabularyKeyDataType.Text));
            this.ClosedOn = this.Add(new VocabularyKey("ClosedOn", VocabularyKeyDataType.Text));
            this.ClosedOnTrustCode = this.Add(new VocabularyKey("ClosedOnTrustCode", VocabularyKeyDataType.Text));
            this.NumEmployeesMin = this.Add(new VocabularyKey("NumEmployeesMin", VocabularyKeyDataType.Text));
            this.StockExchange = this.Add(new VocabularyKey("StockExchange", VocabularyKeyDataType.Text));
            this.NumEmployeesMax = this.Add(new VocabularyKey("NumEmployeesMax", VocabularyKeyDataType.Text));
            this.StockSymbol = this.Add(new VocabularyKey("StockSymbol", VocabularyKeyDataType.Text));
            this.TotalFundingUsd = this.Add(new VocabularyKey("TotalFundingUsd", VocabularyKeyDataType.Text));
            this.NumberOfInvestments = this.Add(new VocabularyKey("NumberOfInvestments", VocabularyKeyDataType.Text));
            this.HomepageUrl = this.Add(new VocabularyKey("HomepageUrl", VocabularyKeyDataType.Text));
            this.CreatedAt = this.Add(new VocabularyKey("CreatedAt", VocabularyKeyDataType.Text));
            this.UpdatedAt = this.Add(new VocabularyKey("UpdatedAt", VocabularyKeyDataType.Text));
        }

        public VocabularyKey Description { get; set; }
        public VocabularyKey Category { get; set; }
        public VocabularyKey Mission { get; set; }

        public VocabularyKey AcquiredCount { get; set; }
        public VocabularyKey Rounds { get; set; }

        public VocabularyKey Valuation { get; set; }
        public VocabularyKey FundingTotal { get; set; }

        public VocabularyKey CurrentRound { get; set; }
        public VocabularyKey IsIpo { get; set; }

        public VocabularyKey Categories { get; set; }
        public VocabularyKey ShortDescription { get; internal set; }
        public VocabularyKey PrimaryRole { get; internal set; }
        public VocabularyKey RoleCompany { get; internal set; }
        public VocabularyKey RoleInvestor { get; internal set; }
        public VocabularyKey RoleGroup { get; internal set; }
        public VocabularyKey RoleSchool { get; internal set; }
        public VocabularyKey FoundedOn { get; internal set; }
        public VocabularyKey FoundedOnTrustCode { get; internal set; }
        public VocabularyKey IsClosed { get; internal set; }
        public VocabularyKey ClosedOn { get; internal set; }
        public VocabularyKey ClosedOnTrustCode { get; internal set; }
        public VocabularyKey NumEmployeesMin { get; internal set; }
        public VocabularyKey StockExchange { get; internal set; }
        public VocabularyKey NumEmployeesMax { get; internal set; }
        public VocabularyKey StockSymbol { get; internal set; }
        public VocabularyKey TotalFundingUsd { get; internal set; }
        public VocabularyKey NumberOfInvestments { get; internal set; }
        public VocabularyKey HomepageUrl { get; internal set; }
        public VocabularyKey CreatedAt { get; internal set; }
        public VocabularyKey UpdatedAt { get; internal set; }
    }
}
