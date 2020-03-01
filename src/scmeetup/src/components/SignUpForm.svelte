<script>
  import { gql } from "apollo-boost";
  import { Text, getInternationalizationContext, getGraphQLContext } from "jss-svelte";
  import queryString from "query-string";
  import { Link, navigate } from "svelte-routing";
  import config from "../temp/config";

  const i18Context = getInternationalizationContext();
  const t = i18Context.getTranslator();

  let invalidUsernameOrPassword = false;

  const signupGraphqlQuery = gql`
    mutation($username: String!, $password: String!, $email: String!) {
      signup(username: $username, password: $password, email: $email)
    }
  `;

  const handleSignupSubmit = event => {
    const variables = {
      username: event.target.username.value,
      password: event.target.password.value,
      email: event.target.email.value
    };

    const graphql = getGraphQLContext();

    graphql.runMutation(signupGraphqlQuery, variables).then(response => {
      if (response.data.signup) {
        window.location = '/events';
      }
    });
  };
</script>

<form on:submit|preventDefault={handleSignupSubmit}>
  <div class="form-group">
    <label for="email">Email address</label>
    <input
      type="email"
      class="form-control"
      id="email"
      placeholder="Enter email" />
  </div>

  <div class="form-group">
    <label for="username">Username</label>
    <input
      type="text"
      class="form-control"
      id="username"
      placeholder="Enter Username" />
  </div>

  <div class="form-group">
    <label for="password">Password</label>
    <input
      type="password"
      class="form-control"
      id="password"
      placeholder="Password" />
  </div>
  <button class="btn btn-primary">{t('Sign Up')}</button>
</form>
