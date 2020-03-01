using System;
using System.Collections.Generic;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class EventQuery : RootFieldType<EventGraphType, Event>
    {
        public EventQuery() : base("event", "Retrieves a single event.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<IdGraphType>>
            {
                Name = "event_id"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Event Resolve(ResolveFieldContext context)
        {
            Guid event_id = context.GetArgument<Guid>("event_id");

            var variables = new
            {
                event_id
            };

            var graphQLClient = new GraphQLClient();

            var responseObject = graphQLClient.Execute<DataObject<EventById>>("EventQuery.graphql", variables);

            Event group = responseObject.data.event_by_pk;

            return group;
        }
    } 
}
