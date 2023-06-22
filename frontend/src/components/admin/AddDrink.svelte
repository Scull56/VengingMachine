<script>
   let titleState = true;
   let priceState = true;
   let countState = true;
   let fileState = true;

   let title = "Напиток";
   let count = 0;
   let price = 1;
   let files;
   let fileValue;

   function validateForm(e) {
      e.preventDefault();

      let check = true;
      let supportedFileExtensions = ["application/vnd.ms-excel"];

      if (title.length < 2 && title.length > 30) {
         titleState = check = false;
      }

      if (count < 0) {
         countState = check = false;
      }

      if (price < 1) {
         priceState = check = false;
      }

      if (
         supportedFileExtensions.indexOf(files[0].type) == -1 ||
         files[0].size > 1048576
      ) {
         fileState = check = false;
      }
   }

   async function sendQuery(e) {
      validateForm(e);
   }
</script>

<form action="">
   <div class="row g-3 mb-sm-3 mb-3 mb-sm-0">
      <div class="form-floating col-sm">
         <input
            id="title"
            type="text"
            class="form-control {!titleState ? 'is-invalid' : ''}"
            minlength="2"
            bind:value={title}
         />
         <label for="title">Название</label>
         <div class="invalid-feedback">
            Название должно быть длиннее 2х символов
         </div>
      </div>
      <div class="form-floating col-sm">
         <input
            id="price"
            type="number"
            class="form-control {!priceState ? 'is-invalid' : ''}"
            min="1"
            bind:value={price}
         />
         <label for="price">Цена в руб.</label>
         <div class="invalid-feedback">
            Название должно быть длиннее 2х символов
         </div>
      </div>
      <div class="form-floating col-sm">
         <input
            id="count"
            type="number"
            class="form-control {!countState ? 'is-invalid' : ''}"
            min="0"
            bind:value={count}
         />
         <label for="count">Кол-во в шт.</label>
         <div class="invalid-feedback">
            Название должно быть длиннее 2х символов
         </div>
      </div>
   </div>
   <div class="row g-3">
      <div class="col-sm-9 col-lg-10">
         <input
            class="form-control col-sm {!fileState ? 'is-invalid' : ''}"
            type="file"
            id="formFile"
            accept=".xls, xlsx"
            bind:value={fileValue}
            bind:files
         />
      </div>
      <div class="col-sm-3 col-lg-2 d-grid">
         <button
            type="button"
            class="btn btn-primary"
            on:click={(e) => sendQuery(e)}
         >
            Добавить
         </button>
      </div>
   </div>
</form>
