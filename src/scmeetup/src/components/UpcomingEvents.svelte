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

  const sitecoreContext = getSitecoreContext();

  const groupQuery = gql`
    query UpcomingEvents {
      events: allUpcomingEvents {
        description
        id
        location
        name
      }
    }
  `;

  const graphql = getGraphQLContext();

  let result;
  result = graphql.runQuery(
    `allupcomingevents`,
    groupQuery,
    {},
    queryResult => {
      result = queryResult;
    }
  );
</script>

{#if result && result.data.events.length}
  <div>
    <h2>Upcoming Events</h2>
    <hr />

    {#each result.data.events as event}
      <div>
        <Link to={'/events/detail/' + event.id}>
          <h4>{event.name}</h4>
        </Link>
        <div>
          <p>{event.description}</p>
        </div>
      </div>
    {/each}
  </div>
{/if}
