using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class OrganizationData
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<OrganizationItem> Items { get; set; }
    }
}