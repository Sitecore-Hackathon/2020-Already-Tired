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
  import { Link } from "svelte-routing";

  const routeContext = getContext(ROUTE_DATA);

  const { groupId } = routeContext;

  const sitecoreContext = getSitecoreContext();

  const groupQuery = gql`
    query GroupUpcomingEvents($group_id:ID!){
      events: upcomingEvents(group_id:$group_id) {
        description
        id
        location
        name
      }
    }
  `;

  const graphql = getGraphQLContext();

  const queryVariables = {
    group_id: groupId
  };

  let result;
  result = graphql.runQuery(
    `upcomingevents|${groupId}`,
    groupQuery,
    queryVariables,
    queryResult => (result = queryResult)
  );
</script>

{#if result && result.data.events.length}
  <div>
    <h2>Upcoming Events</h2>
    <ul>
      {#each result.data.events as event}
        <li>
          <Link to={"/events/detail/" + event.id}>{event.name}</Link>
        </li>
      {/each}
    </ul>
  </div>
{/if}
