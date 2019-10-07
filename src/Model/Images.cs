using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Images
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Image> Items { get; set; }
    }
}