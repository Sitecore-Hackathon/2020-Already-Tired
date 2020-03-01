using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions.Models
{
    public class InsertGroup
    {
        public int affected_rows { get; set; }
        public List<Group> returning { get; set; }
    }

    public class InsertGroupData
    {
        public InsertGroup insert_group { get; set; }
    }
}
