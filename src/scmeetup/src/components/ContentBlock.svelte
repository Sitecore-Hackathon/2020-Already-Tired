<script>
  import { Text, RichText } from "jss-svelte";

  export let fields = null;
</script>

<div>
  <h2><Text field={fields.heading} /></h2>
  <RichText field={fields.body} />
</div>
