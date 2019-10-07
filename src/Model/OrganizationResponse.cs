using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class OrganizationResponse
    {

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
