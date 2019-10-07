using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Paging
    {

        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("first_page_url")]
        public string FirstPageUrl { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }
    }
}