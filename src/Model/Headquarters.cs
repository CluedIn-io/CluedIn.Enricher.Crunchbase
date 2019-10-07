using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Headquarters
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }
}