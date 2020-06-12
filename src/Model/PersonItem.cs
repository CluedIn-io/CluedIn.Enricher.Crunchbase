using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("properties")]
        public PersonProperties Properties { get; set; }
    }
}