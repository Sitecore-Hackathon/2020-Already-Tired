using System;
using System.Collections.Generic;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class UpcomingEventsQuery : RootFieldType<ListGraphType<EventGraphType>, Event[]>
    {
        public UpcomingEventsQuery() : base("upcomingEvents", "Retrieves a list of upcoming events for a group.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "group_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Event[] Resolve(ResolveFieldContext context)
        {
            Guid group_id = context.GetArgument<Guid>("group_id");

            var variables = new
            {
                group_id,
                date = DateTime.Now.ToString("d")
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<EventsList>>("UpcomingEventsQuery.graphql", variables);

            Event[] events = responseObject.data.events.ToArray();

            return events;
        }
    }
}
