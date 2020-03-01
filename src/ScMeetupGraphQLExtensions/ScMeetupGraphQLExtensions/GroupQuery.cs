using System;
using System.Collections.Generic;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class GroupQuery : RootFieldType<GroupGraphType, Group>
    {
        public GroupQuery() : base("group", "Retrieves a single group.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "group_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Group Resolve(ResolveFieldContext context)
        {
            Guid group_id = context.GetArgument<Guid>("group_id");

            var variables = new
            {
                group_id
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<GroupById>>("GroupQuery.graphql", variables);

            Group group = responseObject.data.group_by_pk;

            return group;
        }
    }
}
