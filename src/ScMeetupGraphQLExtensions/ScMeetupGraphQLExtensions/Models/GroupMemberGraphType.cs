using GraphQL.Types;

namespace ScMeetupGraphQLExtensions.Models
{
    public class GroupMemberGraphType : ObjectGraphType<GroupMember>
    {
        public GroupMemberGraphType()
        {
            Name = "GroupMember";

            Field<NonNullGraphType<StringGraphType>>("user_id", resolve: context => context.Source.user_id);
            Field<NonNullGraphType<StringGraphType>>("is_organizer", resolve: context => context.Source.is_organizer);
        }
    }
}
