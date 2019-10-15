using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Paging
    {
        [JsonProperty("total_items")]
        public int? TotalItems { get; set; }

        [JsonProperty("number_of_pages")]
        public int? NumberOfPages { get; set; }

        [JsonProperty("current_page")]
        public int? CurrentPage { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("items_per_page")]
        public int? ItemsPerPage { get; set; }

        [JsonProperty("next_page_url")]
        public string NextPageUrl { get; set; }

        [JsonProperty("prev_page_url")]
        public string PrevPageUrl { get; set; }

        [JsonProperty("key_set_url")]
        public string KeySetUrl { get; set; }

        [JsonProperty("collection_url")]
        public string CollectionUrl { get; set; }

        [JsonProperty("updated_since")]
        public string UpdatedSince { get; set; }
    }
}