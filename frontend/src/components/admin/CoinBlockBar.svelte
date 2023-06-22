<script lang="ts">
   import CoinBlock from "./CoinBlock.svelte";
   import availability from "#data/availability";
   import mapToArray from "#utils/mapToArray";

   export let classes = "";
</script>

<div class="text-end {classes}">
   <div class="d-flex">
      {#each mapToArray($availability) as coin (coin[0])}
         <CoinBlock denomination={coin[0]} state={coin[1]} classes="me-1" />
      {/each}
   </div>
</div>
