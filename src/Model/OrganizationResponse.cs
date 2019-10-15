using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class OrganizationResponse
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("data")]
        public OrganizationData Data { get; set; }
    }
}
