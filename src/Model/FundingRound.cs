namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class FundingRound
    {
        public string permalink { get; set; }
        public string api_path { get; set; }
        public string web_path { get; set; }
        public string funding_type { get; set; }
        public string series { get; set; }
        public object series_qualifier { get; set; }
        public string announced_on { get; set; }
        public int announced_on_trust_code { get; set; }
        public object closed_on { get; set; }
        public object closed_on_trust_code { get; set; }
        public int money_raised { get; set; }
        public string money_raised_currency_code { get; set; }
        public int money_raised_usd { get; set; }
        public object target_money_raised { get; set; }
        public string target_money_raised_currency_code { get; set; }
        public object target_money_raised_usd { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }
}