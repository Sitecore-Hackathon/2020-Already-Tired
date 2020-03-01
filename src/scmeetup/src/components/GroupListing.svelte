<script>
  import { gql } from "apollo-boost";
  import { Text, getGraphQLContext } from "jss-svelte";
  import { Link } from "svelte-routing";

  const pageSize = 5;
  let groups = [];
  let offset = 0;
  let result;

  const listGroupsQuery = gql`
    query ListGroups($limit: Int!, $offset: Int!) {
      groups(limit: $limit, offset: $offset) {
        description
        id
        name
      }
    }
  `;

  const fetchGroups = () => {
    const graphql = getGraphQLContext();

    const queryVariables = {
      limit: pageSize,
      offset: offset
    };

    const response = graphql.runQuery(
      `groups|${offset}`,
      listGroupsQuery,
      queryVariables,
      queryResult => (result = queryResult)
    );

    if (response && response.data) {
      result = response;
    }
  };

  const transformGroup = group => {
    const { name, description, id } = group;
    return {
      name,
      description,
      id,
      href: `/groups/detail/${id}`
    };
  };

  const processResults = () => {
    const newItems = [];

    result.data.groups.forEach(group => {
      const transformed = transformGroup(group);
      newItems.push(transformed);
    });

    groups = [...groups, ...newItems];
  };

  const handleShowMoreClick = () => {
    offset += pageSize;
    fetchGroups();
  };

  fetchGroups();

  $: result && processResults();
</script>

<div>
  <h1>Groups</h1>
  <hr />

  <div>
    {#each groups as group}
      <div>
        <Link to={group.href}>
          <h4>{group.name}</h4>
        </Link>
        <p>{group.description}</p>
      </div>
    {/each}
  </div>

  <div>
    <button class="btn btn-primary" on:click={handleShowMoreClick}>
      Show More
    </button>
  </div>
</div>
