using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalSearch.Providers.Crunchbase.Model
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

    public class Properties
    {

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("api_path")]
        public string ApiPath { get; set; }

        [JsonProperty("web_path")]
        public string WebPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("also_known_as")]
        public List<string> AlsoKnownAs { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("primary_role")]
        public string PrimaryRole { get; set; }

        [JsonProperty("role_company")]
        public bool RoleCompany { get; set; }

        [JsonProperty("role_investor")]
        public object RoleInvestor { get; set; }

        [JsonProperty("role_group")]
        public object RoleGroup { get; set; }

        [JsonProperty("role_school")]
        public object RoleSchool { get; set; }

        [JsonProperty("founded_on")]
        public string FoundedOn { get; set; }

        [JsonProperty("founded_on_trust_code")]
        public int FoundedOnTrustCode { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("closed_on")]
        public object ClosedOn { get; set; }

        [JsonProperty("closed_on_trust_code")]
        public int ClosedOnTrustCode { get; set; }

        [JsonProperty("num_employees_min")]
        public int NumEmployeesMin { get; set; }

        [JsonProperty("num_employees_max")]
        public int NumEmployeesMax { get; set; }

        [JsonProperty("stock_exchange")]
        public object StockExchange { get; set; }

        [JsonProperty("stock_symbol")]
        public object StockSymbol { get; set; }

        [JsonProperty("total_funding_usd")]
        public int TotalFundingUsd { get; set; }

        [JsonProperty("number_of_investments")]
        public int NumberOfInvestments { get; set; }

        [JsonProperty("homepage_url")]
        public string HomepageUrl { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public int UpdatedAt { get; set; }

        public string UuId { get; set; }
    }

    public class Paging
    {

        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("first_page_url")]
        public string FirstPageUrl { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }
    }

   

    public class PrimaryImage
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }

   
    public class Founders
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

    public class Person
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("properties")]
        public PersonProperties Properties { get; set; }
    }

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

    public class PersonRelationships
    {

        [JsonProperty("person")]
        public Person Person { get; set; }
    }

    public class Item
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("relationships")]
        public PersonRelationships Relationships { get; set; }
    }

    public class CurrentTeam
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

   

    public class PastTeam
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

    public class JobProperties
    {
        public string permalink { get; set; }
        public string api_path { get; set; }
        public string web_path { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public object also_known_as { get; set; }
        public string bio { get; set; }
        public string profile_image_url { get; set; }
        public object role_investor { get; set; }
        public object born_on { get; set; }
        public object born_on_trust_code { get; set; }
        public object died_on { get; set; }
        public int died_on_trust_code { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }

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

    public class Market
    {
        public string name { get; set; }
        public string web_path { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }


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


    public class Image
    {
        public string asset_path { get; set; }
        public string asset_url { get; set; }
        public string content_type { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int filesize { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }

    public class WebPresence
    {
        public string website_type { get; set; }
        public string website_name { get; set; }
        public string url { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
    }


    public class BoardMembersAndAdvisors
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

   

    public class Investors
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

    public class OwnedBy
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    

    public class SubOrganizations
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

   

    public class Offices
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Address> Items { get; set; }
    }

    public class Headquarters
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }

   

    public class Products
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Product> Items { get; set; }
    }

   

    public class Categories
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Market> Items { get; set; }
    }

    public class Customers
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Competitors
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Memberships
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

   

    public class Members
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Person> Items { get; set; }
    }

   

    public class FundingRounds
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<FundingRound> Items { get; set; }
    }

    public class Investments
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Acquisitions
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }

    public class AcquiredBy
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public class Ipo
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public class Funds
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }

    

    public class Websites
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<WebPresence> Items { get; set; }
    }

    

    public class Images
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Image> Items { get; set; }
    }

    public class Videos
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }

    

    public class News
    {

        [JsonProperty("cardinality")]
        public string Cardinality { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<NewsItem> Items { get; set; }
    }

    public class Relationships
    {

        [JsonProperty("primary_image")]
        public PrimaryImage PrimaryImage { get; set; }

        [JsonProperty("founders")]
        public Founders Founders { get; set; }

        [JsonProperty("current_team")]
        public CurrentTeam CurrentTeam { get; set; }

        [JsonProperty("past_team")]
        public PastTeam PastTeam { get; set; }

        [JsonProperty("board_members_and_advisors")]
        public BoardMembersAndAdvisors BoardMembersAndAdvisors { get; set; }

        [JsonProperty("investors")]
        public Investors Investors { get; set; }

        [JsonProperty("owned_by")]
        public OwnedBy OwnedBy { get; set; }

        [JsonProperty("sub_organizations")]
        public SubOrganizations SubOrganizations { get; set; }

        [JsonProperty("offices")]
        public Offices Offices { get; set; }

        [JsonProperty("headquarters")]
        public Headquarters Headquarters { get; set; }

        [JsonProperty("products")]
        public Products Products { get; set; }

        [JsonProperty("categories")]
        public Categories Categories { get; set; }

        [JsonProperty("customers")]
        public Customers Customers { get; set; }

        [JsonProperty("competitors")]
        public Competitors Competitors { get; set; }

        [JsonProperty("memberships")]
        public Memberships Memberships { get; set; }

        [JsonProperty("members")]
        public Members Members { get; set; }

        [JsonProperty("funding_rounds")]
        public FundingRounds FundingRounds { get; set; }

        [JsonProperty("investments")]
        public Investments Investments { get; set; }

        [JsonProperty("acquisitions")]
        public Acquisitions Acquisitions { get; set; }

        [JsonProperty("acquired_by")]
        public AcquiredBy AcquiredBy { get; set; }

        [JsonProperty("ipo")]
        public Ipo Ipo { get; set; }

        [JsonProperty("funds")]
        public Funds Funds { get; set; }

        [JsonProperty("websites")]
        public Websites Websites { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("videos")]
        public Videos Videos { get; set; }

        [JsonProperty("news")]
        public News News { get; set; }
    }

    public class Data
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

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

    public class OrganizationResponse
    {

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class PersonResponse
    {

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("data")]
        public PersonData Data { get; set; }
    }

}
