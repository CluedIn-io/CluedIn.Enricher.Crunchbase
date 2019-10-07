using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonData
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("properties")]
        public PersonProperties Properties { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }
}