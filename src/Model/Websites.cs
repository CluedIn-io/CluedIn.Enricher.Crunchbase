using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Websites
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<WebPresence> Items { get; set; }
    }
}