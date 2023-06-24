<script lang="ts">
   import ResponseError from "./ResponseError.svelte";
   import {
      title as titleValid,
      price as priceValid,
      count as countValid,
   } from "#validation/product";
   import {
      updateProduct as updateProductQuery,
      deleteProduct as deleteProductQuery,
   } from "#requests/products";
   import { getContext } from "svelte";
   import updateProducts from "#scripts/products";

   export let card;

   let key: string = getContext("key");

   let form;

   let titleState = true;
   let priceState = true;
   let countState = true;
   let errorState = false;
   let message = "";

   let title = card.title;
   let count = card.count;
   let price = card.price;

   async function updateProduct(e) {
      e.preventDefault();

      titleState = titleValid(title);
      countState = countValid(count);
      priceState = priceValid(price);

      if (titleState && countState && priceState) {
         let res = await updateProductQuery(key, form);

         if (res.status != 200) {
            let body = res.json();

            errorState = true;
            message = body["message"];
         } else {
            await updateProducts();
         }
      }
   }

   async function deleteProduct(e) {
      e.preventDefault();

      let res = await deleteProductQuery(key, card.id);

      if (res.status != 200) {
         let body = res.json();

         errorState = true;
         message = body["message"];
      } else {
         await updateProducts();
      }
   }
</script>

<div class="card h-100">
   <img src={card.imgSrc} class="card-img-top img" alt="напиток" />
   <div class="card-body">
      <form action="" bind:this={form}>
         <input type="hidden" name="id" value={card.id} />
         <div class="row row-cols-sm-2 g-2 mb-2">
            <div class="form-floating">
               <input
                  id="title"
                  name="title"
                  type="text"
                  class="form-control {!titleState ? 'is-invalid' : ''}"
                  minlength="2"
                  bind:value={title}
               />
               <label for="title">Название</label>
               <div class="invalid-feedback">
                  Допустимый размер названия от 2 до 30 символов.
               </div>
            </div>
            <div class="form-floating">
               <input
                  id="price"
                  name="price"
                  type="number"
                  class="form-control {!priceState ? 'is-invalid' : ''}"
                  min="1"
                  bind:value={price}
               />
               <label for="price">Цена в руб.</label>
               <div class="invalid-feedback">Минимальная цена - 1 руб.</div>
            </div>
            <div class="form-floating">
               <input
                  id="count"
                  name="count"
                  type="number"
                  class="form-control {!countState ? 'is-invalid' : ''}"
                  min="0"
                  bind:value={count}
               />
               <label for="count">Кол-во в шт.</label>
               <div class="invalid-feedback">
                  Необходимо указать положительное число.
               </div>
            </div>
         </div>
         <div class="d-flex justify-content-end">
            <button
               type="button"
               class="btn btn-warning mb-2 me-2"
               on:click={(e) => updateProduct(e)}
            >
               Сохранить
            </button>
            <button
               type="button"
               class="btn btn-secondary mb-2"
               on:click={(e) => deleteProduct(e)}
            >
               Удалить
            </button>
         </div>
      </form>
   </div>
</div>
<ResponseError bind:state={errorState}>
   {message}
</ResponseError>

<style lang="scss">
   .img {
      max-height: 250px;
      object-fit: contain;
   }
</style>
