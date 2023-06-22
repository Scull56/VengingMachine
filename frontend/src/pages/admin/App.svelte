<script lang="ts">
   import Header from "#components/Header.svelte";
   import DrinksLayout from "#components/DrinksLayout.svelte";
   import DrinkCardEdit from "#components/admin/DrinkCardEdit.svelte";
   import CoinBlockBar from "#components/admin/CoinBlockBar.svelte";
   import AddDrink from "#components/admin/AddDrink.svelte";
   import LoadFile from "#components/admin/LoadFile.svelte";
   import Footer from "#components/Footer.svelte";
</script>

<main>
   <Header />
   <div class="container">
      <div class="row mb-4 g-3">
         <div class="col-md-3">
            <h4>Блокировка монет:</h4>
            <CoinBlockBar />
         </div>
         <div class="col-md-9">
            <h4>Добавить новый товар:</h4>
            <AddDrink />
         </div>
      </div>
      <h4>Каталог:</h4>
      <DrinksLayout let:prop={card} classes="mb-4" countInRow="3">
         <DrinkCardEdit {card} />
      </DrinksLayout>
      <h4>Загрузить новые товары:</h4>
      <LoadFile />
   </div>
</main>
<Footer />
