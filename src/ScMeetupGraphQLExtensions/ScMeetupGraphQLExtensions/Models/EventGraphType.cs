using GraphQL.Types;

namespace ScMeetupGraphQLExtensions.Models
{
    public class EventGraphType : ObjectGraphType<Event>
    {
        public EventGraphType()
        {
            Name = "Event";

            Field<NonNullGraphType<IdGraphType>>("id", resolve: context => context.Source.id);
            Field<NonNullGraphType<StringGraphType>>("name", resolve: context => context.Source.name);
            Field<NonNullGraphType<StringGraphType>>("description", resolve: context => context.Source.description);
            Field<NonNullGraphType<StringGraphType>>("location", resolve: context => context.Source.description);
            Field<NonNullGraphType<DateGraphType>>("date", resolve: context => context.Source.description);
        }
    }
}
