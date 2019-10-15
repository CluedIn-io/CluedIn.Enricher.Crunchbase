using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class OrganizationProperties
    {
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("api_path")]
        public string ApiPath { get; set; }

        [JsonProperty("web_path")]
        public string WebPath { get; set; }

        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("stock_exchange")]
        public string StockExchange { get; set; }

        [JsonProperty("stock_symbol")]
        public string StockSymbol { get; set; }

        [JsonProperty("primary_role")]
        public string PrimaryRole { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("homepage_url")]
        public string HomepageUrl { get; set; }

        [JsonProperty("facebook_url")]
        public string FacebookUrl { get; set; }

        [JsonProperty("twitter_url")]
        public string TwitterUrl { get; set; }

        [JsonProperty("linkedin_url")]
        public string LinkedinUrl { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("created_at")]
        public int? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public int? UpdatedAt { get; set; }
    }
}