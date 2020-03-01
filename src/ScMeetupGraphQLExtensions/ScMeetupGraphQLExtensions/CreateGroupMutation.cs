using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;


namespace ScMeetupGraphQLExtensions
{
    public class CreateGroupMutation : RootFieldType<GroupGraphType, Group>, IContentSchemaRootFieldType
    {
        private const string TemplateName = "{98F51803-F753-56DF-8F62-BD23144A22B3}";

        public CreateGroupMutation() : base("createGroup", "Creates a new group.")
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

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override Group Resolve(ResolveFieldContext context)
        {
            string name = context.GetArgument<string>("name");
            string description = context.GetArgument<string>("description");

            var variables = new
            {
                groupName = name,
                description
            };

            var graphQLClient = new GraphQLClient();

            DataObject<InsertGroupData> responseObject = graphQLClient.Execute<DataObject<InsertGroupData>>("AddGroup.graphql", variables);

            Group group = responseObject.data.insert_group.returning.FirstOrDefault();

            return group;
        }

        public Database Database { get; set; }
    }
}
