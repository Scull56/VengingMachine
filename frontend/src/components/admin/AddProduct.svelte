<script lang="ts">
   import { addProduct as addProductQuery } from "#requests/products";
   import {
      title as titleValid,
      count as countValid,
      price as priceValid,
      image as imageValid,
   } from "#validation/product";
   import updateProducts from "#scripts/products";
   import { getContext } from "svelte";
   import errorState from "#data/error";

   let key: string = getContext("key");

   let form;
   let fileInput;

   let titleState = true;
   let priceState = true;
   let countState = true;
   let fileState = true;

   let title = "Напиток";
   let count = 0;
   let price = 1;
   let files;

   async function addProduct(e) {
      e.preventDefault();

      let check = true;

      titleState = titleValid(title);
      priceState = priceValid(price);
      countState = countValid(count);
      fileState = imageValid(files);

      check = titleState && priceState && countState && fileState;

      if (check) {
         let res = await addProductQuery(key, form);

         if (res.status == 200) {
            fileInput.value = "";
            title = "Напиток";
            count = 0;
            price = 1;

            updateProducts();
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

<form action="" bind:this={form}>
   <div class="row g-3 mb-sm-3 mb-3 mb-sm-0">
      <div class="form-floating col-sm">
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
      <div class="form-floating col-sm">
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
      <div class="form-floating col-sm">
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
   <div class="row g-3">
      <div class="col-sm-9 col-lg-10">
         <input
            class="form-control col-sm {!fileState ? 'is-invalid' : ''}"
            type="file"
            id="image"
            name="image"
            accept=".jpg, .jpeg"
            bind:files
            bind:this={fileInput}
         />
         <div class="invalid-feedback">
            Выберите изображение для товара. Формат jpg, размер до 1 мб.
         </div>
      </div>
      <div class="col-sm-3 col-lg-2 d-grid">
         <button
            type="submit"
            class="btn btn-warning"
            on:click={(e) => addProduct(e)}
         >
            Добавить
         </button>
      </div>
   </div>
</form>
