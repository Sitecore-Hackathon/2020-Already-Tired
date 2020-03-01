<script>
  import { getContext } from "svelte";
  import { Text, getFieldValue } from "jss-svelte";
  import { Link } from "svelte-routing";
  import { ROUTE_DATA, USER_DATA } from "../constants";

  export let fields = null;

  const whoAmI = getContext(USER_DATA);

  const buttonText = getFieldValue(fields, "buttonText") || "Create a group";
</script>

<style>
  div {
    padding-bottom: 20px;
  }
</style>

<div>
  <h4>Create a Group</h4>
  <p>Start something new and create a group from scratch.</p>
  {#if whoAmI.isAuthenticated}
    <Link to="/creategroup" className="btn btn-primary">{buttonText}</Link>
  {:else}
    <p>Create an account to create a group.</p>
  {/if}
</div>
