using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonRelationships
    {

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}