<script>
  import { gql } from "apollo-boost";
  import { Text, getInternationalizationContext, getGraphQLContext } from "jss-svelte";
  import queryString from "query-string";
  import { Link, navigate } from "svelte-routing";
  import config from "../temp/config";

  const i18Context = getInternationalizationContext();
  const t = i18Context.getTranslator();

  let invalidUsernameOrPassword = false;

  const loginGraphqlQuery = gql`
    mutation($username: String!, $password: String!) {
      login(username: $username, password: $password)
    }
  `;

  const handleLoginSubmit = event => {
    const variables = {
      username: event.target.username.value,
      password: event.target.password.value
    };

    const graphql = getGraphQLContext();

    graphql.runMutation(loginGraphqlQuery, variables).then(response => {
      if (response.data.login) {
        window.location = '/events';
      }
    });
  };
</script>

<form on:submit|preventDefault={handleLoginSubmit}>
  {#if invalidUsernameOrPassword}
    <div class="alert alert-warning" role="alert">
      {t('Invalid Username or Password')}
    </div>
  {/if}

  <div class="form-group">
    <label for="username">Username</label>
    <input
      type="text"
      class="form-control"
      id="username"
      placeholder="Enter username" />
  </div>
  <div class="form-group">
    <label for="password">Password</label>
    <input
      type="password"
      class="form-control"
      id="password"
      placeholder="Password" />
  </div>
  <button class="btn btn-primary">{t('Login')}</button>
</form>
