using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScMeetupGraphQLExtensions
{
    public class GroupMembersQuery : RootFieldType<ListGraphType<GroupMemberGraphType>, GroupMember[]>
    {
        public GroupMembersQuery() : base("groupMembers", "Retrieves a list of members in the group.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "group_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override GroupMember[] Resolve(ResolveFieldContext context)
        {
            Guid group_id = context.GetArgument<Guid>("group_id");

            var variables = new
            {
                group_id
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<GroupMemberList>>("GroupMembers.graphql", variables);

            GroupMember[] members = responseObject.data.members.ToArray();

            return members;
        }
    }
}
