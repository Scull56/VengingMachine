<script lang="ts">
   import CoinBalance from "./CoinBalance.svelte";
   import balance from "#data/balance";
   import mapToArray from "#utils/mapToArray";
</script>

<div class="text-end">
   <div class="d-flex">
      {#each mapToArray($balance) as coin (coin[0])}
         <CoinBalance denomination={coin[0]} count={coin[1]} classes="me-3" />
      {/each}
   </div>
</div>
