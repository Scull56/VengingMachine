<script lang="ts">
   import products from "#data/products";

   export let classes = "";
   export let countInRow = "4";
</script>

<div
   class="row row-cols-1 row-cols-md-3 row-cols-lg-{countInRow} g-3 {classes}"
>
   {#each $products as card (card.id)}
      <div class="col">
         <slot prop={card} />
      </div>
   {/each}
</div>
