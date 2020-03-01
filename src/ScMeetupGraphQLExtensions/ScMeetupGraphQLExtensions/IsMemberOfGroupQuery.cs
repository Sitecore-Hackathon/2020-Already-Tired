using System;
using System.Collections.Generic;
using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class IsMemberOfGroupQuery : RootFieldType<BooleanGraphType, bool>, IContentSchemaRootFieldType
    {
        public IsMemberOfGroupQuery() : base("isMemberOfGroup", "Is the user a member of the group.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "group_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        public Database Database { get; set; }

        protected override bool Resolve(ResolveFieldContext context)
        {
            Guid group_id = context.GetArgument<Guid>("group_id");
            string user_id = Sitecore.Context.User.Name;

            var variables = new
            {
                group_id,
                user_id
            };

            var graphQLClient = new GraphQLClient();

            dynamic isAttendingResult = graphQLClient.Execute<dynamic>("IsMemberOfGroupQuery.graphql", variables);

            bool isAttending = isAttendingResult["data"]["group_membership"].Count > 0;

            return isAttending;
        }
    }
}
