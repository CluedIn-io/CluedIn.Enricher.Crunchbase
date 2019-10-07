using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class PeopleResponsePaging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }
}
