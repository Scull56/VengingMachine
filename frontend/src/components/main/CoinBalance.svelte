<script lang="ts">
   import CoinBtn from "#components/CoinBtn.svelte";
   import balance from "#data/balance";

   export let denomination: number;
   export let count: number;
   export let classes: string = "";

   if (count != $balance.get(denomination)) {
      count = $balance.get(denomination);
   }

   function updateCount() {
      $balance.set(denomination, ++count);
      balance.set($balance);
   }
</script>

<div class="d-flex align-items-center {classes}">
   <CoinBtn {denomination} classes="me-1" on:click={updateCount} />

   <span>x {count}</span>
</div>
