namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class NewsItem
    {
        public string title { get; set; }
        public string author { get; set; }
        public string posted_on { get; set; }
        public object posted_on_trust_code { get; set; }
        public string url { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }
}