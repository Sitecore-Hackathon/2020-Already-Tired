<script>
  import { gql } from "apollo-boost";
  import { getContext } from "svelte";
  import { Text, RichText, DateField, getGraphQLContext } from "jss-svelte";
  import { ROUTE_DATA } from "../constants";

  const routeContext = getContext(ROUTE_DATA);

  const { eventId } = routeContext;

  const groupQuery = gql`
    query EventQuery($event_id: ID!) {
      event(event_id: $event_id) {
        description
        id
        location
        name
      }
    }
  `;

  const graphql = getGraphQLContext();

  const queryVariables = {
    event_id: eventId
  };

  let result;
  result = graphql.runQuery(
    `event|${eventId}`,
    groupQuery,
    queryVariables,
    queryResult => (result = queryResult)
  );
</script>

{#if result}
  <div>
    <h1>{result.data.event.name}</h1>
    <hr />
    <div>
      <p>{result.data.event.description}</p>
    </div>
  </div>
{/if}
