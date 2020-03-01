<script>
  import { Placeholder } from "jss-svelte";

  export let rendering = null;
</script>

<div class="row">
  <div class="col-md-9">
    <Placeholder {rendering} name="left" />
  </div>
  <div class="col-md-3">
    <Placeholder {rendering} name="right" />
  </div>
</div>
