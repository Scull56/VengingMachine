<script lang="ts">
   import CoinDepost from "./CoinDeposit.svelte";
   import balance from "#data/balance";
   import mapToArray from "#utils/mapToArray";
</script>

<div class="text-end">
   <div class="d-flex">
      {#each mapToArray($balance) as coin}
         <CoinDepost denomination={coin[0]} classes="me-1" />
      {/each}
   </div>
</div>
