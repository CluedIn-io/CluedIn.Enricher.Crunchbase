using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Ipo
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}