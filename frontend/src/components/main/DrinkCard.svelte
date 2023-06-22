<script lang="ts">
   import drinks from "#data/drinks";
   import deposit from "#data/deposit";

   export let card;

   let state: boolean = card.count > 0;

   function payDrink() {
      if (state && $deposit >= card.price) {
         deposit.set($deposit - card.price);

         $drinks.find((item) => item.id == card.id).count = card.count - 1;
         drinks.set($drinks);
      }
   }
</script>

<div class="card h-100">
   <img src={card.imgSrc} class="card-img-top img" alt="напиток" />
   <div class="card-body">
      <div class="d-flex justify-content-between align-items-center">
         <div>
            <h5 class="card-title mb-2">{card.title}</h5>
            <button
               type="button"
               class="btn btn-dark mb-2 {!state ? 'disabled' : ''}"
               on:click={payDrink}
            >
               Купить
            </button>
            <p>
               В наличии: {card.count} шт.
            </p>
         </div>
         <div>
            <h5>
               {card.price} руб.
            </h5>
         </div>
      </div>
   </div>
</div>

<style lang="scss">
   .img {
      max-height: 250px;
      object-fit: contain;
   }

   .card {
      overflow: hidden;
   }
</style>
