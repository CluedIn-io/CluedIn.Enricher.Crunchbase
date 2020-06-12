using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonResponse
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("data")]
        public PersonData Data { get; set; }
    }
}
