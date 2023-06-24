<script lang="ts">
   import products from "#data/products";
   import deposit from "#data/deposit";

   export let card;

   let state: boolean = card.count > 0;

   function payProduct() {
      if (state && $deposit >= card.price) {
         deposit.set($deposit - card.price);

         $products.find((item) => item.id == card.id).count = card.count - 1;
         products.set($products);
      }
   }
</script>

<div class="card h-100">
   <img src={card.imgSrc} class="card-img-top img" alt="напиток" />
   <div class="card-body">
      <div class="row align-items-center">
         <div class="col-8">
            <h5 class="card-title mb-2">{card.title}</h5>
            <button
               type="button"
               class="btn btn-warning mb-2 {!state ? 'disabled' : ''}"
               on:click={payProduct}
            >
               Купить
            </button>
            <p>
               В наличии: {card.count} шт.
            </p>
         </div>
         <div class="col-4">
            <h5 class="text-end">
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
