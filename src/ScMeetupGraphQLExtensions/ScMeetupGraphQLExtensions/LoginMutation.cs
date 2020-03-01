using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;
using Sitecore;
using Sitecore.Data;
using Sitecore.Security.Authentication;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Services.Infrastructure.Security;
using Sitecore.Services.Infrastructure.Web.Http.Security;

namespace ScMeetupGraphQLExtensions
{
    public class LoginMutation : RootFieldType<BooleanGraphType, bool>, IContentSchemaRootFieldType
    {
        public LoginMutation() : base("login", "User login")
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

            Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override bool Resolve(ResolveFieldContext context)
        {
            string username = context.GetArgument<string>("username");
            string password = context.GetArgument<string>("password");

            var userService = new UserService();

            try
            {
                userService.Login("extranet", username, password);
                return true;
            }
            catch (Exception)
            {

            }

            return false;
        }

        public Database Database { get; set; }
    }
}
