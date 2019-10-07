using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Metadata
    {

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("www_path_prefix")]
        public string WwwPathPrefix { get; set; }

        [JsonProperty("api_path_prefix")]
        public string ApiPathPrefix { get; set; }

        [JsonProperty("image_path_prefix")]
        public string ImagePathPrefix { get; set; }
    }
}