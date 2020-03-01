using System;
using System.Collections.Generic;
using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class IsAttendingEventQuery : RootFieldType<BooleanGraphType, bool>, IContentSchemaRootFieldType
    {
        public IsAttendingEventQuery() : base("isAttendingEvent", "Is the user attending an event")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "event_id",
                Description = "The name of the event to attend"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        public Database Database { get; set; }

        protected override bool Resolve(ResolveFieldContext context)
        {
            Guid event_id = context.GetArgument<Guid>("event_id");
            string user_id = Sitecore.Context.User.Name;

            var variables = new
            {
                event_id,
                user_id
            };

            var graphQLClient = new GraphQLClient();

            dynamic isAttendingResult = graphQLClient.Execute<dynamic>("IsAttendingQuery.graphql", variables);

            bool isAttending = isAttendingResult["data"]["event_attendee"].Count > 0;

            return isAttending;
        }
    }
}
