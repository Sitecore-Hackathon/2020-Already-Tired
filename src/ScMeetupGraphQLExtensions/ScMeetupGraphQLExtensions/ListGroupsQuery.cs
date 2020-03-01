using System.Collections.Generic;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class ListGroupsQuery : RootFieldType<ListGraphType<GroupGraphType>, Group[]>
    {
        public ListGroupsQuery() : base("listGroups", "Retrieves a list of groups.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IntGraphType>>
            {
                Name = "limit",
                Description = "How many groups to return"
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IntGraphType>>
            {
                Name = "offset",
                Description = "Amount of groups to skip before returning results."
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Group[] Resolve(ResolveFieldContext context)
        {
            int limit = context.GetArgument<int>("limit");
            int offset = context.GetArgument<int>("offset");

            var variables = new
            {
                limit,
                offset
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<GroupList>>("ListGroups.graphql", variables);

            Group[] group = responseObject.data.group.ToArray();

            return group;
        }
    }
}
