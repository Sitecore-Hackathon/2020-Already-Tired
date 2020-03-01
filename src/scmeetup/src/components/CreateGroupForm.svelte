<script>
  import { gql } from "apollo-boost";
  import {
    Text,
    getInternationalizationContext,
    getGraphQLContext
  } from "jss-svelte";
  import { navigate } from "svelte-routing";

  const i18Context = getInternationalizationContext();
  const t = i18Context.getTranslator();

  const createGroupQuery = gql`
    mutation($name: String!, $description: String!) {
      createGroup(name: $name, description: $description) {
        id
        name
        description
      }
    }
  `;

  let createdGroupId;

  const handleFormSubmit = event => {
    const variables = {
      name: event.target.name.value,
      description: event.target.description.value
    };

    const graphql = getGraphQLContext();

    graphql.runMutation(
      createGroupQuery,
      variables
    ).then((response) => {
      createdGroupId = response.data.createGroup.id;
    })
  };

  const redirect = () => {
    const newUrl = `/groups/detail/${createdGroupId}`;
    navigate(newUrl, { replace: true });
  };

  $: createdGroupId && redirect();
</script>

<form on:submit|preventDefault={handleFormSubmit}>
  <div class="form-group">
    <label for="name">{t('Group Name')}</label>
    <input
      required
      type="text"
      class="form-control"
      placeholder="Enter group name"
      id="name" />
  </div>
  <div class="form-group">
    <label for="description">{t('Group Description')}</label>
    <textarea
      required
      class="form-control"
      id="description"
      rows="5"
      placeholder="Enter a description for this group" />
  </div>
  <button class="btn btn-primary">{t('Create Group')}</button>
</form>
