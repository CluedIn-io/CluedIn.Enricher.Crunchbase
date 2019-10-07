using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.Crunchbase.Model
{
    public class RelationshipResponse
    {
        public List<Relationship> data { get; set; }
        public Paging paging { get; set; }
    }
}