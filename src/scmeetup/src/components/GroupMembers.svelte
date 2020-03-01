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

  const { groupId } = routeContext;

  const sitecoreContext = getSitecoreContext();

  const groupQuery = gql`
    query GroupMemberQuery($group_id: ID!) {
      groupMembers(group_id: $group_id) {
        user_id
        is_organizer
      }
    }
  `;

  const graphql = getGraphQLContext();

  const queryVariables = {
    group_id: groupId
  };

  let result;
  result = graphql.runQuery(
    `groupmembers|${groupId}`,
    groupQuery,
    queryVariables,
    queryResult => (result = queryResult)
  );
</script>

{#if result}
  <div>
    <h2>Members</h2>
    <ul>
      {#each result.data.groupMembers as member}
        <li>{member.user_id}</li>
      {/each}
    </ul>
  </div>
{/if}
