using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Relationships
    {

        [JsonProperty("primary_image")]
        public PrimaryImage PrimaryImage { get; set; }

        [JsonProperty("founders")]
        public Founders Founders { get; set; }

        [JsonProperty("current_team")]
        public CurrentTeam CurrentTeam { get; set; }

        [JsonProperty("past_team")]
        public PastTeam PastTeam { get; set; }

        [JsonProperty("board_members_and_advisors")]
        public BoardMembersAndAdvisors BoardMembersAndAdvisors { get; set; }

        [JsonProperty("investors")]
        public Investors Investors { get; set; }

        [JsonProperty("owned_by")]
        public OwnedBy OwnedBy { get; set; }

        [JsonProperty("sub_organizations")]
        public SubOrganizations SubOrganizations { get; set; }

        [JsonProperty("offices")]
        public Offices Offices { get; set; }

        [JsonProperty("headquarters")]
        public Headquarters Headquarters { get; set; }

        [JsonProperty("products")]
        public Products Products { get; set; }

        [JsonProperty("categories")]
        public Categories Categories { get; set; }

        [JsonProperty("customers")]
        public Customers Customers { get; set; }

        [JsonProperty("competitors")]
        public Competitors Competitors { get; set; }

        [JsonProperty("memberships")]
        public Memberships Memberships { get; set; }

        [JsonProperty("members")]
        public Members Members { get; set; }

        [JsonProperty("funding_rounds")]
        public FundingRounds FundingRounds { get; set; }

        [JsonProperty("investments")]
        public Investments Investments { get; set; }

        [JsonProperty("acquisitions")]
        public Acquisitions Acquisitions { get; set; }

        [JsonProperty("acquired_by")]
        public AcquiredBy AcquiredBy { get; set; }

        [JsonProperty("ipo")]
        public Ipo Ipo { get; set; }

        [JsonProperty("funds")]
        public Funds Funds { get; set; }

        [JsonProperty("websites")]
        public Websites Websites { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("videos")]
        public Videos Videos { get; set; }

        [JsonProperty("news")]
        public News News { get; set; }
    }
}