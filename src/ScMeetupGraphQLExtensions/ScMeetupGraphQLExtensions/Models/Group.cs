using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions.Models
{
    public class Group
    {
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public DateTime updated_at { get; set; }
    }
}
