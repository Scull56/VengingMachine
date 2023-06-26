<script lang="ts">
   import {
      table as tableValid,
      images as imagesValid,
   } from "#validation/product";
   import { addProducts } from "#requests/products";
   import updateProducts from "#scripts/products";
   import { getContext } from "svelte";
   import errorState from "#data/error";

   export let classes = "";

   let key: string = getContext("key");

   let form;

   let table;
   let tableInput;
   let tableState = true;

   let images;
   let imagesInput;
   let imagesState = true;

   async function sendQuery(e) {
      e.preventDefault();

      tableState = tableValid(table);
      imagesState = imagesValid(images);

      if (tableState && imagesState) {
         let res = await addProducts(key, form);

         if (res.status == 200) {
            tableInput.value = "";
            imagesInput.value = "";

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
   <div class="row g-3 justify-content-end {classes}">
      <div class="col-sm-6">
         <label for="formFile" class="form-label">Таблица данных</label>
         <input
            class="form-control col-sm {!tableState ? 'is-invalid' : ''}"
            type="file"
            id="formFile"
            name="sheet"
            accept=".xls, .xlsx"
            bind:this={tableInput}
            bind:files={table}
         />
         <div class="invalid-feedback">
            Скачайте шаблон, заполните его данными и выберите его для отправки.
            Размер файла не должен превышать 1 мб.
         </div>
      </div>
      <div class="col-sm-6">
         <label for="images" class="form-label">Изображения продукторв</label>
         <input
            class="form-control col-sm {!imagesState ? 'is-invalid' : ''}"
            type="file"
            id="images"
            name="images"
            accept=".jpeg, .jpg"
            bind:this={imagesInput}
            bind:files={images}
            multiple
         />
         <div class="invalid-feedback">
            Загрузите изображения в формате .jpg или .jpeg, размером не более 1
            МБ каждая
         </div>
      </div>
      <div class="col-sm-4 col-md-3 col-lg-2 d-grid">
         <button
            type="button"
            class="btn btn-warning"
            on:click={(e) => sendQuery(e)}
         >
            Загрузить
         </button>
      </div>
      <div class="col-sm-4 col-md-3 col-lg-2 d-grid">
         <a
            class="d-grid text-decoration-none"
            href="/assets/files/template.xlsx"
            download="шаблон.xlsx"
         >
            <button type="button" class="btn btn-secondary">
               Скачать шаблон
            </button>
         </a>
      </div>
   </div>
</form>
