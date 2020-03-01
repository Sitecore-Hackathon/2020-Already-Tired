using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions.Models
{
    public class Event
    {
        public string group_id { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public DateTime updated_at { get; set; }
        public string id { get; set; }
        public DateTime date { get; set; }
    }
}
