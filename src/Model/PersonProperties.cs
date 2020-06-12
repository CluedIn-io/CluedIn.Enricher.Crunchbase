using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonProperties
    {
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("api_path")]
        public string ApiPath { get; set; }

        [JsonProperty("web_path")]
        public string WebPath { get; set; }

        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("organization_permalink")]
        public string OrganizationPermalink { get; set; }

        [JsonProperty("organization_api_path")]
        public string OrganizationApiPath { get; set; }

        [JsonProperty("organization_web_path")]
        public string OrganizationWebPath { get; set; }

        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

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