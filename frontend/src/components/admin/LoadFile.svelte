<script lang="ts">
   import { file as fileValid } from "#validation/product";
   import { addProducts } from "#requests/products";
   import ResponseError from "./ResponseError.svelte";
   import updateProducts from "#scripts/products";
   import { getContext } from "svelte";

   let key: string = getContext("key");

   let form;
   let files;
   let fileInput;
   let fileState = true;
   let errorState = false;
   let message = "";

   async function sendQuery(e) {
      e.preventDefault();

      fileState = fileValid(files);

      if (fileState) {
         let res = await addProducts(key, form);

         if (res.status == 200) {
            fileInput.value = "";

            updateProducts();
         } else {
            message = `Ошибка ${res.status}: ${(await res.json()).message}`;

            errorState = true;
         }
      }
   }

   export let classes = "";
</script>

<form action="" bind:this={form}>
   <div class="row g-3 {classes}">
      <div class="col-sm-8">
         <input
            class="form-control col-sm {!fileState ? 'is-invalid' : ''}"
            type="file"
            id="formFile"
            accept=".xls, .xlsx"
            bind:this={fileInput}
            bind:files
         />
         <div class="invalid-feedback">
            Скачайте шаблон, заполните его данными и выберите его для отправки.
            Размер файла не должен превышать 1 мб.
         </div>
      </div>
      <div class="col-sm-2 d-grid">
         <button
            type="button"
            class="btn btn-warning"
            on:click={(e) => sendQuery(e)}
         >
            Загрузить
         </button>
      </div>
      <div class="col-sm-2 d-grid">
         <a
            class="d-grid"
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
<ResponseError bind:state={errorState}>
   {message}
</ResponseError>
