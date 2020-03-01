<script>
  import { getContext } from "svelte";
  import { Text, getFieldValue } from "jss-svelte";
  import { Link } from "svelte-routing";
  import { ROUTE_DATA, USER_DATA } from "../constants";

  export let fields = null;

  const buttonText = getFieldValue(fields, "buttonText") || "Create Event";

  const routeContext = getContext(ROUTE_DATA);
  const whoAmI = getContext(USER_DATA);

  const { groupId } = routeContext;

  const url = `/events/create/${groupId}`;
</script>

<style>
  div {
    padding-bottom: 20px;
  }
</style>

<div>
  <h4>Create Event</h4>
  {#if whoAmI.isAuthenticated}
    <p>Create an account for for this group.</p>
    <Link to={url} className="btn btn-primary">{buttonText}</Link>
  {:else}
    <p>Create an account to create an event.</p>
  {/if}
</div>
