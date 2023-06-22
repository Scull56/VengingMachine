<script lang="ts">
   import CoinBtn from "#components/CoinBtn.svelte";
   import availability from "#data/availability";

   export let denomination: number;
   export let state: boolean;
   export let classes: string = "";

   function changeState() {
      state = !state;

      $availability.set(denomination, state);

      availability.set($availability);

      console.log($availability.get(denomination));
   }
</script>

<div class="d-flex align-items-center {classes}">
   <CoinBtn {denomination} classes="me-1" {state} on:click={changeState} />
</div>
