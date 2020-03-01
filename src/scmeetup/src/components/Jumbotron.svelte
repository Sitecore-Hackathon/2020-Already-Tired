<script>
  import { Text } from "jss-svelte";
  import { Link } from "svelte-routing";

  export let fields = null;
</script>

<section class="jumbotron text-center">
  <div class="container">
    <h1 class="jumbotron-heading">
      <Text field={fields.heading} />
    </h1>
    <p class="lead text-muted">
      <Text field={fields.text} />
    </p>
    <p>
      <Link to="/events" className="btn btn-primary my-2">Explore Events</Link>
    </p>
  </div>
</section>
