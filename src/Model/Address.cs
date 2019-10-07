namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Address
    {
        public object name { get; set; }
        public string street_1 { get; set; }
        public object street_2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string city_web_path { get; set; }
        public string region { get; set; }
        public string region_code2 { get; set; }
        public string region_web_path { get; set; }
        public string country { get; set; }
        public string country_code2 { get; set; }
        public string country_code3 { get; set; }
        public string country_web_path { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }
}