<script lang="ts">
   import CoinDepost from "./CoinDeposit.svelte";
   import Loader from "#components/Loader.svelte";
   import balance from "#data/balance";
   import availability from "#data/availability";
   import mapToArray from "#utils/mapToArray";

   let loaderState: boolean;

   $: loaderState = $availability == undefined ? true : false;
</script>

{#if loaderState}
   <Loader classes="spinner_min" />
{:else}
   <div class="text-end">
      <div class="d-flex">
         {#each mapToArray($balance) as coin (coin[0])}
            <CoinDepost denomination={coin[0]} classes="me-1" />
         {/each}
      </div>
   </div>
{/if}
