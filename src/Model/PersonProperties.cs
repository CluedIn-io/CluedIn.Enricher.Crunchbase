namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PersonProperties
    {
        public string permalink { get; set; }
        public string api_path { get; set; }
        public string web_path { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string[] also_known_as { get; set; }
        public string bio { get; set; }
        public string profile_image_url { get; set; }
        public object role_investor { get; set; }
        public string born_on { get; set; }
        public object born_on_trust_code { get; set; }
        public string died_on { get; set; }
        public object died_on_trust_code { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string uuid { get; set; }
    }
}