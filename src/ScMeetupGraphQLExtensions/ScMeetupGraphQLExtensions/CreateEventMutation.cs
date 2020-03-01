using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Content.GraphTypes;
using Sitecore.Services.GraphQL.Content.Mutations;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class CreateEventMutation : RootFieldType<EventGraphType, Event>, IContentSchemaRootFieldType
    {
        private const string TemplateName = "{04A97059-0CA1-5BF8-BEE5-568DAE687346}";

        public CreateEventMutation() : base("createEvent", "Creates a new event.")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "name",
                Description = "The name of the event to create"
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "description",
                Description = "The description of the event."
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "date",
                Description = "The date of the event."
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "location",
                Description = "The location of the event."
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "group_id",
                Description = "The group this event belongs to."
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Event Resolve(ResolveFieldContext context)
        {
            string name = context.GetArgument<string>("name");
            string description = context.GetArgument<string>("description");
            string date = context.GetArgument<string>("date");
            string location = context.GetArgument<string>("location");
            Guid group_id = context.GetArgument<Guid>("group_id");

            name = ItemUtil.ProposeValidItemName(name);

            var variables = new
            {
                date,
                description,
                location,
                name,
                group_id
            };

            var graphQLClient = new GraphQLClient();

            DataObject<InsertEventData> responseObject = graphQLClient.Execute<DataObject<InsertEventData>>("CreateEvent.graphql", variables);

            Event eventResult = responseObject.data.insert_event.returning.FirstOrDefault();

            return eventResult;
        }

        public Database Database { get; set; }
    }
}
