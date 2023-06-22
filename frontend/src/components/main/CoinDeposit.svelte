<script lang="ts">
   import CoinBtn from "#components/CoinBtn.svelte";
   import availability from "#data/availability";
   import balance from "#data/balance";
   import deposit from "#data/deposit";
   import earnings from "#data/earnings";

   export let denomination: number;
   export let classes: string = "";

   let state: boolean;

   $: state = $balance.get(denomination) > 0 && $availability.get(denomination);

   function incrementDeposit() {
      if (state) {
         $balance.set(denomination, $balance.get(denomination) - 1);

         $earnings.set(denomination, $earnings.get(denomination) + 1);

         deposit.set($deposit + denomination);

         balance.set($balance);

         earnings.set($earnings);
      }
   }
</script>

<div class="d-flex align-items-center {classes}">
   <CoinBtn {denomination} classes="me-1" {state} on:click={incrementDeposit} />
</div>
