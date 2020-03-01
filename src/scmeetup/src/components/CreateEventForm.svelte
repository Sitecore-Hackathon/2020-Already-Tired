<script>
  import { gql } from "apollo-boost";
  import { getContext } from "svelte";
  import {
    Text,
    getInternationalizationContext,
    getGraphQLContext
  } from "jss-svelte";
  import { Link, navigate } from "svelte-routing";
  import { ROUTE_DATA } from "../constants";

  const i18Context = getInternationalizationContext();
  const t = i18Context.getTranslator();

  const createGroupQuery = gql`
    mutation CreateEvent(
      $name: String!
      $description: String!
      $date: String!
      $location: String!
      $group_id: String!
    ) {
      createEvent(
        name: $name
        description: $description
        date: $date
        location: $location
        group_id: $group_id
      ) {
        id
      }
    }
  `;

  let createdId;

  const handleFormSubmit = event => {
    const routeContext = getContext(ROUTE_DATA);

    const { groupId } = routeContext;

    const variables = {
      name: event.target.name.value,
      date: event.target.date.value,
      location: event.target.location.value,
      description: event.target.description.value,
      group_id: groupId
    };

    const graphql = getGraphQLContext();

    graphql.runMutation(createGroupQuery, variables).then(response => {
      createdId = response.data.createEvent.id;
    });
  };

  const redirect = () => {
    const newUrl = `/events/detail/${createdId}`;
    navigate(newUrl, { replace: true });
  };

  $: createdId && redirect();
</script>

<form on:submit|preventDefault={handleFormSubmit}>
  <div class="form-group">
    <label for="name">Event Name</label>
    <input
      required
      type="text"
      class="form-control"
      placeholder="Enter event name"
      id="name" />
  </div>
  <div class="form-group">
    <label for="date">Event Date</label>
    <input
      required
      type="text"
      class="form-control"
      placeholder="Enter event date"
      id="date" />
  </div>
  <div class="form-group">
    <label for="location">Event Location</label>
    <input
      required
      type="text"
      class="form-control"
      placeholder="Enter event location"
      id="location" />
  </div>
  <div class="form-group">
    <label for="description">Event Description</label>
    <textarea
      required
      class="form-control"
      id="description"
      rows="5"
      placeholder="Enter a description for this event" />
  </div>
  <button class="btn btn-primary">{t('Create Event')}</button>
</form>
