using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class Android
    {
        public string app_name { get; set; }
        public string package { get; set; }
        public string url { get; set; }
    }

    public class Io
    {
        public string app_name { get; set; }
        public string app_store_id { get; set; }
        public string url { get; set; }
    }

    public class AppLinks
    {
        public List<Android> android { get; set; }
        public List<Io> ios { get; set; }
    }

    public class CategoryList
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Summary
    {
        public string social_sentence { get; set; }
        public int total_count { get; set; }
    }

    public class FriendsWhoLike
    {
        public List<object> data { get; set; }
        public Summary summary { get; set; }
    }

    public class Context
    {
        public FriendsWhoLike friends_who_like { get; set; }
        public string id { get; set; }
    }

    public class Cover
    {
        public string cover_id { get; set; }
        public int offset_x { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
        public string id { get; set; }
    }

    public class Engagement
    {
        public int count { get; set; }
        public string social_sentence { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public string country { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
    }

    public class Parking
    {
        public int lot { get; set; }
        public int street { get; set; }
        public int valet { get; set; }
    }

    public class VoipInfo
    {
        public bool has_permission { get; set; }
        public bool has_mobile_app { get; set; }
        public bool is_pushable { get; set; }
        public bool is_callable { get; set; }
        public bool is_callable_webrtc { get; set; }
        public int reason_code { get; set; }
        public string reason_description { get; set; }
    }

    public class FacebookResponse
    {
        public string about { get; set; }
        public string name { get; set; }
        public AppLinks app_links { get; set; }
        public string category { get; set; }
        public List<CategoryList> category_list { get; set; }
        public string company_overview { get; set; }
        public Context context { get; set; }
        public Cover cover { get; set; }
        public string description { get; set; }
        public string description_html { get; set; }
        public string display_subtext { get; set; }
        public List<string> emails { get; set; }
        public Engagement engagement { get; set; }
        public int fan_count { get; set; }
        public string founded { get; set; }
        public string global_brand_page_name { get; set; }
        public string impressum { get; set; }
        public bool is_always_open { get; set; }
        public bool is_community_page { get; set; }
        public bool is_permanently_closed { get; set; }
        public string link { get; set; }
        public Location location { get; set; }
        public string mission { get; set; }
        public double overall_star_rating { get; set; }
        public Parking parking { get; set; }
        public string phone { get; set; }
        public string place_type { get; set; }
        public string products { get; set; }
        public string username { get; set; }
        public VoipInfo voip_info { get; set; }
        public string website { get; set; }
        public string id { get; set; }
    }

    public class OgObject
    {
        public string id { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string updated_time { get; set; }
    }

    public class Share
    {
        public int comment_count { get; set; }
        public int share_count { get; set; }
    }

    public class Website
    {
        public OgObject og_object { get; set; }
        public Share share { get; set; }
        public string id { get; set; }
    }

    public class Relationship
    {
        public string about { get; set; }
        public string bio { get; set; }
        public List<string> emails { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string single_line_address { get; set; }
        public string website { get; set; }
        public string personal_info { get; set; }
        public string id { get; set; }
        public string founded { get; set; }
        public string mission { get; set; }
        public string company_overview { get; set; }
        public string birthday { get; set; }
        public string phone { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class RelationshipResponse
    {
        public List<Relationship> data { get; set; }
        public Paging paging { get; set; }
    }

    public class FacebookLogo
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }

    public class FacebookLogoResponse
    {
        public FacebookLogo data { get; set; }
    }
    
}
