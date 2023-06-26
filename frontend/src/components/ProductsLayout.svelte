<script lang="ts">
   import products from "#data/products";
   import Loader from "./Loader.svelte";

   export let classes = "";
   export let countInRow = "4";

   let loaderState = true;

   $: loaderState = $products == undefined ? true : false;
</script>

{#if loaderState}
   <div class="wrapper position-relative">
      <Loader classes="position-absolute top-50 start-50 translate-middle" />
   </div>
{:else}
   <div
      class="row row-cols-1 row-cols-md-3 row-cols-lg-{countInRow} g-3 {classes}"
   >
      {#each $products as card (card.id)}
         <div class="col">
            <slot prop={card} />
         </div>
      {/each}
   </div>
{/if}

<style>
   .wrapper {
      height: 200px;
   }
</style>
