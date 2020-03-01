<script>
  import { Placeholder } from "jss-svelte";

  export let rendering = null;
</script>

<div class="row">
  <div class="col-md-6">
    <Placeholder {rendering} name="left" />
  </div>
  <div class="col-md-6">
    <Placeholder {rendering} name="right" />
  </div>
</div>
