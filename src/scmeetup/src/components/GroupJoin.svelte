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
  import { ROUTE_DATA, USER_DATA } from "../constants";

  let isUserAttendingValue;
  let isUserAttendingCheckCompleted;
  let buttonText;
  let isChangingAttendingStatus = false;

  const graphql = getGraphQLContext();

  const joinGroupMutation = gql`
    mutation($group_id: ID!) {
      joinGroup(group_id: $group_id)
    }
  `;

  const unjoinGroupMutation = gql`
    mutation($group_id: ID!) {
      unjoinGroup(group_id: $group_id)
    }
  `;

  const isMemberOfGroupQuery = gql`
    query($group_id: ID!) {
      isMemberOfGroup(group_id: $group_id)
    }
  `;

  const routeContext = getContext(ROUTE_DATA);
  const whoAmI = getContext(USER_DATA);

  const { groupId } = routeContext;

  const isUserMember = () => {
    const queryVariables = {
      group_id: groupId
    };

    let result;
    result = graphql.runQuery(
      `isUserMember|${groupId}|${new Date().valueOf()}`,
      isMemberOfGroupQuery,
      queryVariables,
      queryResult => {
        isUserAttendingValue = queryResult.data.isMemberOfGroup;
        isUserAttendingCheckCompleted = true;
      }
    );
  };

  const handleAttendClick = () => {
    const queryToRun = !isUserAttendingValue
      ? joinGroupMutation
      : unjoinGroupMutation;

    const variables = {
      group_id: groupId
    };

    isChangingAttendingStatus = true;
    graphql.runMutation(queryToRun, variables).then(response => {
      isUserAttendingValue = !isUserAttendingValue;
      isChangingAttendingStatus = false;
    });
  };

  $: buttonText = isUserAttendingValue ? "Unjoin" : "Join";

  isUserMember();
</script>

<style>
  div {
    padding-bottom: 20px;
  }
</style>

<div>
  <h4>Join This Group</h4>
  <p>Like what you see? Join this group!</p>
  {#if whoAmI.isAuthenticated}
    {#if isUserAttendingCheckCompleted}
      <button
        class="btn btn-primary"
        on:click={handleAttendClick}
        disabled={isChangingAttendingStatus}>
        {buttonText}
      </button>
    {/if}
  {:else}
    <p>Create an account to join this group.</p>
  {/if}
</div>
