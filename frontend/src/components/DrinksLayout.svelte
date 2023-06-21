<script lang="ts">
   import DrinkCard from "./DrinkCard.svelte";
   import drinks from "#data/drinks";
</script>

<div class="row row-cols-1 row-cols-md-3 g-4">
   {#each $drinks as card (card.id)}
      <div class="col">
         <DrinkCard {card} />
      </div>
   {/each}
</div>
