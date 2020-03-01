using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions.Models
{
    public class GroupMember
    {
        public string user_id { get; set; }

        public bool is_organizer { get; set; }
    }
}
