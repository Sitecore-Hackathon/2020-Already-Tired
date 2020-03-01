<script>
  import { Link } from "svelte-routing";
  import { getInternationalizationContext } from 'jss-svelte';
  import {getContext} from 'svelte';
  import { USER_DATA } from "./constants";

  const i18Context = getInternationalizationContext();
  const t = i18Context.getTranslator();

  const whoAmI = getContext(USER_DATA);
</script>

<div
  class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3
  bg-white border-bottom">
  <h5 class="my-0 mr-md-auto font-weight-normal">
    <Link to="/" className="text-dark active" aria-current="page">{t('ScMeetup')}</Link>
  </h5>
  <nav class="my-2 my-md-0 mr-md-3">
    <Link to="/events" className="p-2 text-dark"> {t('Events')} </Link>
    <Link to="/groups" className="p-2 text-dark"> {t('Groups')} </Link>
    {#if !whoAmI.isAuthenticated}
      <Link to="/login" className="p-2 text-dark"> {t('Login')} </Link>
      <Link to="/signup" className="p-2 text-dark"> {t('Sign up')} </Link>
    {/if}
  </nav>
</div>
