namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Product
    {
        public string permalink { get; set; }
        public string api_path { get; set; }
        public string web_path { get; set; }
        public string name { get; set; }
        public string lifecycle_stage { get; set; }
        public string short_description { get; set; }
        public string profile_image_url { get; set; }
        public string launched_on { get; set; }
        public int launched_on_trust_code { get; set; }
        public object closed_on { get; set; }
        public object closed_on_trust_code { get; set; }
        public string homepage_url { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }
}