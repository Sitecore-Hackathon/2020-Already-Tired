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
    query GroupQuery($group_id:ID!){
      group(group_id:$group_id) {
        description
        id
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
    `group|${groupId}`,
    groupQuery,
    queryVariables,
    queryResult => (result = queryResult)
  );
</script>

{#if result}
  <div>
    <h1>{result.data.group.name}</h1>
    <hr />
    <p>{result.data.group.description}</p>
  </div>
{/if}
