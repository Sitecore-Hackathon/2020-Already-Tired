﻿using System;
using System.Collections.Generic;
using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    public class JoinGroupMutation : RootFieldType<IntGraphType, int>, IContentSchemaRootFieldType
    {
        public JoinGroupMutation() : base("joinGroup", "Join a group")
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

            dynamic joinResult = graphQLClient.Execute<dynamic>("JoinGroupMutation.graphql", variables);

            Console.WriteLine(joinResult);

            return 1;
        }
    }
}
