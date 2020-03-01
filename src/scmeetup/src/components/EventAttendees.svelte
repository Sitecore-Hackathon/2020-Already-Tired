<script>
  import { gql } from "apollo-boost";
  import { getContext } from "svelte";
  import {
    Text,
    RichText,
    DateField,
    getSitecoreContext,
    getGraphQLContext
  } from "jss-svelte";
  import { ROUTE_DATA } from "../constants";

  const routeContext = getContext(ROUTE_DATA);

  const { eventId } = routeContext;

  const sitecoreContext = getSitecoreContext();

  const groupQuery = gql`
    query EventQuery($event_id:ID!) {
      eventAttendees(event_id:$event_id) {
        user_id
      }
    }
  `;

  const graphql = getGraphQLContext();

  const queryVariables = {
    event_id: eventId
  };

  let result;
  result = graphql.runQuery(
    `eventattendees|${eventId}|${new Date().valueOf()}`,
    groupQuery,
    queryVariables,
    queryResult => {
      (result = queryResult)
    }
  );
</script>

{#if result && result.data.eventAttendees.length}
  <div>
    <h2>Attendees</h2>
    <ul>
      {#each result.data.eventAttendees as attendee}
        <li>{attendee.user_id}</li>
      {/each}
    </ul>
  </div>
{/if}
