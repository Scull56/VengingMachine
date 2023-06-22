<script lang="ts">
   import drinks from "#data/drinks";

   export let classes = "";
   export let countInRow = "4";
</script>

<div class="row row-cols-1 row-cols-md-{countInRow} g-3 {classes}">
   {#each $drinks as card (card.id)}
      <div class="col">
         <slot prop={card} />
      </div>
   {/each}
</div>
