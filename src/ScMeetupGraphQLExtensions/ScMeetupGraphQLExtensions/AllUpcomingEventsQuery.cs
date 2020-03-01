using System;
using System.Collections.Generic;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class AllUpcomingEventsQuery : RootFieldType<ListGraphType<EventGraphType>, Event[]>
    {
        public AllUpcomingEventsQuery() : base("upcomingEvents", "Retrieves a list of upcoming events for a group.")
        {
            Arguments = new QueryArguments(new List<QueryArgument>());
        }

        protected override Event[] Resolve(ResolveFieldContext context)
        {
            var variables = new
            {
                date = DateTime.Now.ToString("d")
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<EventsList>>("AllUpcomingEvents.graphql", variables);

            Event[] events = responseObject.data.events.ToArray();

            return events;
        }
    }
}
