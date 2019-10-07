using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Properties
    {

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("api_path")]
        public string ApiPath { get; set; }

        [JsonProperty("web_path")]
        public string WebPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("also_known_as")]
        public List<string> AlsoKnownAs { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("primary_role")]
        public string PrimaryRole { get; set; }

        [JsonProperty("role_company")]
        public bool RoleCompany { get; set; }

        [JsonProperty("role_investor")]
        public object RoleInvestor { get; set; }

        [JsonProperty("role_group")]
        public object RoleGroup { get; set; }

        [JsonProperty("role_school")]
        public object RoleSchool { get; set; }

        [JsonProperty("founded_on")]
        public string FoundedOn { get; set; }

        [JsonProperty("founded_on_trust_code")]
        public int FoundedOnTrustCode { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("closed_on")]
        public object ClosedOn { get; set; }

        [JsonProperty("closed_on_trust_code")]
        public int ClosedOnTrustCode { get; set; }

        [JsonProperty("num_employees_min")]
        public int NumEmployeesMin { get; set; }

        [JsonProperty("num_employees_max")]
        public int NumEmployeesMax { get; set; }

        [JsonProperty("stock_exchange")]
        public object StockExchange { get; set; }

        [JsonProperty("stock_symbol")]
        public object StockSymbol { get; set; }

        [JsonProperty("total_funding_usd")]
        public int TotalFundingUsd { get; set; }

        [JsonProperty("number_of_investments")]
        public int NumberOfInvestments { get; set; }

        [JsonProperty("homepage_url")]
        public string HomepageUrl { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public int UpdatedAt { get; set; }

        public string UuId { get; set; }
    }
}