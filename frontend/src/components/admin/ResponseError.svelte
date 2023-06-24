<script>
   import { fade } from "svelte/transition";

   export let state = false;

   $: if (state) setTimeout(() => (state = false), 5000);
</script>

{#if state}
   <div class="alert alert-danger" role="alert" transition:fade>
      <slot />
   </div>
{/if}

<style lang="scss">
   .alert {
      position: fixed;
      z-index: 1;
      bottom: 1vh;
      right: 1vw;
      margin: 0;
   }
</style>
