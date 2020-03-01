using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions.Models
{
    public class InsertEvent
    {
        public int affected_rows { get; set; }
        public List<Event> returning { get; set; }
    }

    public class InsertEventData
    {
        public InsertEvent insert_event { get; set; }
    }
}
