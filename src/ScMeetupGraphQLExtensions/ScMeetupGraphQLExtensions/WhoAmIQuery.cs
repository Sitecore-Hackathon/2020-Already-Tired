using GraphQL.Types;
using ScMeetupGraphQLExtensions.Models;
using Sitecore;
using Sitecore.Security.Accounts;
using Sitecore.Services.GraphQL.Schemas;

namespace ScMeetupGraphQLExtensions
{
    internal class WhoAmIQuery : RootFieldType<UserGraphType, User>
    {
        public WhoAmIQuery() : base(name: "whoAmI", description: "Gets the current user")
        {
        }

        protected override User Resolve(ResolveFieldContext context)
        {
            // this is the object the resolver maps onto the graph type
            // (see UserGraphType below). This is your own domain object, not GraphQL-specific.
            return Context.User;
        }
    }
}
