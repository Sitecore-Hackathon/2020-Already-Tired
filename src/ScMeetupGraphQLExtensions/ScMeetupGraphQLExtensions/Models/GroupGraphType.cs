using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;

namespace ScMeetupGraphQLExtensions.Models
{
    public class GroupGraphType : ObjectGraphType<Group>
    {
        public GroupGraphType()
        {
            Name = "Group";

            Field<NonNullGraphType<IdGraphType>>("id", resolve: context => context.Source.id);
            Field<NonNullGraphType<StringGraphType>>("name", resolve: context => context.Source.name);
            Field<NonNullGraphType<StringGraphType>>("description", resolve: context => context.Source.description);
        }
    }
}
