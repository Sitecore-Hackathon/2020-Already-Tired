using GraphQL.Types;

namespace ScMeetupGraphQLExtensions.Models
{
    public class EventAttendeeGraphType : ObjectGraphType<EventAttendee>
    {
        public EventAttendeeGraphType()
        {
            Name = "EventAttendee";

            Field<NonNullGraphType<StringGraphType>>("user_id", resolve: context => context.Source.user_id);
        }
    }
}
