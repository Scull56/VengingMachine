<script lang="ts">
   import CoinBlock from "./CoinBlock.svelte";
   import availability from "#data/availability";
   import mapToArray from "#utils/mapToArray";
   import Loader from "#components/Loader.svelte";

   export let classes = "";

   let loaderState: boolean;

   $: loaderState = $availability == undefined ? true : false;
</script>

{#if loaderState}
   <Loader classes="spinner_min" />
{:else}
   <div class="text-end {classes}">
      <div class="d-flex">
         {#each mapToArray($availability) as coin (coin[0])}
            <CoinBlock
               bind:denomination={coin[0]}
               bind:state={coin[1]}
               classes="me-1"
            />
         {/each}
      </div>
   </div>
{/if}
