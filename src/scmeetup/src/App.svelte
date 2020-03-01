<script>
  import { gql } from "apollo-boost";
  import { Router, Link, Route } from "svelte-routing";
  import {setContext} from 'svelte';

  import RouteComponent from "./RouteComponent.svelte";
  import { setSitecoreContext, SitecoreContext } from "jss-svelte";
  import {
    InternationalizationContext,
    setInternationalizationContext,
    setGraphQLContext
  } from "jss-svelte";
  import { USER_DATA } from "./constants";

  import componentFactory from "./temp/componentFactory.js";

  export let path = null;
  export let routeData = null;
  export let graphQLClient = null;
  export let dictionary = null;

  export const routePatterns = [
    {
      pattern: "/groups/detail/:groupId*",
      variables: { sitecoreRoute: "/groups/detail" }
    },
    {
      pattern: "/events/create/:groupId*",
      variables: { sitecoreRoute: "/events/create" }
    },
    {
      pattern: "/events/detail/:eventId*",
      variables: { sitecoreRoute: "/events/detail" }
    },
    { pattern: "/:lang([a-z]{2}-[A-Z]{2})/:sitecoreRoute*" },
    { pattern: "/:lang([a-z]{2})/:sitecoreRoute*" },
    { pattern: "/:sitecoreRoute*" }
  ];

  const whoAmIQuery = gql`
    {
      whoAmI {
        fullName
        isAuthenticated
      }
    }
  `;

  if (dictionary) {
    const context = new InternationalizationContext(dictionary);
    setInternationalizationContext(context);
  }

  if (graphQLClient) {
    setGraphQLContext(graphQLClient);
  }

  const sitecoreContext = new SitecoreContext();
  sitecoreContext.setComponentFactory(componentFactory);
  setSitecoreContext(sitecoreContext);

  let whoAmIResult;
  whoAmIResult = graphQLClient.runQuery(
    "whoami",
    whoAmIQuery,
    {},
    queryResult => {
      const { whoAmI } = queryResult.data;
      whoAmIResult = whoAmI;
    }
  );

  $: {
    whoAmIResult && setContext(USER_DATA, whoAmIResult);
  }
</script>

<div id="JSS_APP">
  {#if whoAmIResult}
    <Router>
      {#each routePatterns as routePattern}
        <Route path={routePattern.pattern} let:params>
          <RouteComponent
            pathOverride={path}
            {params}
            {routeData}
            {dictionary}
            variables={routePattern.variables} />
        </Route>
      {/each}
    </Router>
  {/if}
</div>
