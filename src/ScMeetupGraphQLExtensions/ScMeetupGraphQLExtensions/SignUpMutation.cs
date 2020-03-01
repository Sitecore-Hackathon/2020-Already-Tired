using System;
using System.Collections.Generic;
using System.Web.Security;
using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Services.Infrastructure.Security;

namespace ScMeetupGraphQLExtensions
{
    public class SignupMutation : RootFieldType<BooleanGraphType, bool>, IContentSchemaRootFieldType
    {
        public SignupMutation() : base("signup", "Sign Up")
        {
            var queryArgumentArray = new List<QueryArgument>();

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "username"
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "password"
            });

            queryArgumentArray.Add(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "email"
            });

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override bool Resolve(ResolveFieldContext context)
        {
            string username = context.GetArgument<string>("username");
            string password = context.GetArgument<string>("password");
            string email = context.GetArgument<string>("email");

            var userService = new UserService();
 
            try
            {
                string usernameWithDomain = string.Format("extranet\\{0}", username);
                Membership.CreateUser(usernameWithDomain, password, email);
                userService.Login("extranet", username, password);
                return true;
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp);
            }

            return false;
        }

        public Database Database { get; set; }
    }
}
