using System;
using System.Collections.Generic;
using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class UnjoinGroupMutation : RootFieldType<IntGraphType, int>, IContentSchemaRootFieldType
    {
        public UnjoinGroupMutation() : base("unjoinGroup", "Unjoin a group")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "group_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        public Database Database { get; set; }

        protected override int Resolve(ResolveFieldContext context)
        {
            Guid group_id = context.GetArgument<Guid>("group_id");
            string user_id = Sitecore.Context.User.Name;

            var variables = new
            {
                group_id,
                user_id
            };

            var graphQLClient = new GraphQLClient();

            graphQLClient.Execute<dynamic>("UnjoinGroupMutation.graphql", variables);

            return 1;
        }
    }
}
