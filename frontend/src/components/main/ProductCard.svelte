<script lang="ts">
   import products from "#data/products";
   import deposit from "#data/deposit";
   import conf from "#data/config";
   import { buyProduct } from "#requests/products";
   import errorState from "#data/error";

   export let card;

   let state: boolean = card.count > 0;

   $: state = card.count > 0;

   async function payProduct() {
      if (state && $deposit >= card.price) {
         let res = await buyProduct(card.id, 1);

         if (res.status == 200) {
            deposit.set($deposit - card.price);

            $products.find((item) => item.id == card.id).count = card.count - 1;
            products.set($products);
         } else {
            let message =
               res.status == 500
                  ? `Ошибка на сервере, повторите попытку позже`
                  : `Ошибка ${res.status}: ${(await res.json()).message}`;

            errorState.set({ state: true, message });
         }
      }
   }
</script>

<div class="card h-100">
   <img
      src={conf.imagesPath + card.image}
      class="card-img-top img"
      alt="напиток"
   />
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
               {#if card.count > 0}
                  В наличии: {card.count} шт.
               {:else}
                  Нет в наличии
               {/if}
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
