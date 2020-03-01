using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;
using System;
using System.Collections.Generic;

namespace ScMeetupGraphQLExtensions
{
    public class EventAttendeesQuery : RootFieldType<ListGraphType<EventAttendeeGraphType>, EventAttendee[]>
    {
        public EventAttendeesQuery() : base("eventAttendee", "Retrieves a single group.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "event_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override EventAttendee[] Resolve(ResolveFieldContext context)
        {
            Guid event_id = context.GetArgument<Guid>("event_id");

            var variables = new
            {
                event_id
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<EventAttendeeList>>("EventAttendees.graphql", variables);

            EventAttendee[] attendees = responseObject.data.event_attendee.ToArray();

            return attendees;
        }
    }
}
