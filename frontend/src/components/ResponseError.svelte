<script>
   import { fade } from "svelte/transition";
   import errorState from "#data/error";

   let state = $errorState.state;

   $: {
      state = $errorState.state;

      if (state)
         setTimeout(() => errorState.set({ state: false, message: "" }), 5000);
   }
</script>

{#if state}
   <div class="alert alert-danger" role="alert" transition:fade>
      {$errorState.message}
   </div>
{/if}

<style lang="scss">
   .alert {
      position: fixed;
      z-index: 10;
      bottom: 5px;
      right: 5px;
      margin: 0;
   }
</style>
