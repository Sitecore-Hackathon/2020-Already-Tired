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

  const attendEventMutation = gql`
    mutation AttendEventMutation($event_id: ID!) {
      attendEvent(event_id: $event_id)
    }
  `;

  const removeAttendEventMutation = gql`
    mutation RemoveAttendingQuery($event_id:ID!) {
      removeAttendEvent(event_id:$event_id)
    }
  `;

  const isAttendingEventQuery = gql`
    query IsAttendingQuery($event_id: ID!) {
      isAttendingEvent(event_id: $event_id)
    }
  `;

  const routeContext = getContext(ROUTE_DATA);
  const whoAmI = getContext(USER_DATA);

  const { eventId } = routeContext;

  const isUserAttending = () => {
    const queryVariables = {
      event_id: eventId
    };

    let result;
    result = graphql.runQuery(
      `isUserAttending|${eventId}|${new Date().valueOf()}`,
      isAttendingEventQuery,
      queryVariables,
      queryResult => {
        isUserAttendingValue = queryResult.data.isAttendingEvent;
        isUserAttendingCheckCompleted = true;
      }
    );
  };

  const handleAttendClick = () => {
    const queryToRun = !isUserAttendingValue ? attendEventMutation : removeAttendEventMutation;

    const variables = {
      event_id: eventId
    };

    isChangingAttendingStatus = true;
    graphql.runMutation(queryToRun, variables).then(response => {
      isUserAttendingValue = !isUserAttendingValue;
      isChangingAttendingStatus = false;
    });
  };

  $: buttonText = isUserAttendingValue ? 'Remove Attendance' : 'Attend';

  isUserAttending();
</script>

<style>
  div {
    padding-bottom: 20px;
  }
</style>

<div>
  <h4>Attend this Event</h4>
  <p>Does this event look interesting? Join them!</p>
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
    <p>Create an account to join this event.</p>
  {/if}
</div>
