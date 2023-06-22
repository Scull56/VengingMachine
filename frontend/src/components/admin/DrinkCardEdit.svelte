<script lang="ts">
   import drinks from "#data/drinks";

   export let card;
</script>

<div class="card h-100">
   <img src={card.imgSrc} class="card-img-top img" alt="напиток" />
   <div class="card-body">
      <div class="d-flex justify-content-between align-items-center">
         <div>
            <div class="form-floating mb-2">
               <input
                  id="title"
                  type="text"
                  class="form-control"
                  value={card.title}
               />
               <label for="title">Название</label>
            </div>
            <div class="form-floating">
               <input
                  id="count"
                  type="text"
                  class="form-control"
                  value={card.count}
               />
               <label for="count">Кол-во в шт.</label>
            </div>
         </div>
         <div>
            <div class="form-floating">
               <input
                  id="count"
                  type="text"
                  class="form-control"
                  value={card.price}
               />
               <label for="count">Цена в руб.</label>
            </div>
         </div>
      </div>
      <div class="d-flex justify-content-end">
         <button type="button" class="btn btn-primary mb-2 me-2">
            Сохранить
         </button>
         <button type="button" class="btn btn-danger mb-2"> Удалить </button>
      </div>
   </div>
</div>

<style lang="scss">
   .img {
      max-height: 250px;
      object-fit: contain;
   }
</style>
