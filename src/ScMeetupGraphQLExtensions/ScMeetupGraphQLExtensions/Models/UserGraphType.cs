﻿using System;
using System.Web;
using GraphQL.Types;
using Sitecore.Security.Accounts;

namespace ScMeetupGraphQLExtensions.Models
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Name = "SitecorePrincipal";

            Field<NonNullGraphType<StringGraphType>>("name", resolve: context => context.Source.Name);
            Field<NonNullGraphType<StringGraphType>>("fullName", resolve: context => string.IsNullOrWhiteSpace(context.Source.Profile.FullName) ? context.Source.Name : context.Source.Profile.FullName);
            Field<NonNullGraphType<StringGraphType>>("icon", resolve: context => $"{HttpContext.Current?.Request.Url.GetLeftPart(UriPartial.Authority)}/-/icon/{context.Source.Profile.Portrait}");
            Field<NonNullGraphType<BooleanGraphType>>("isAuthenticated", resolve: context => context.Source.IsAuthenticated);
            Field<NonNullGraphType<BooleanGraphType>>("isAdministrator", resolve: context => context.Source.IsAdministrator);
        }
    }
}
