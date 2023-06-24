<script lang="ts">
   import Header from "#components/Header.svelte";
   import ProductsLayout from "#components/ProductsLayout.svelte";
   import ProductCardEdit from "#components/admin/ProductCardEdit.svelte";
   import CoinBlockBar from "#components/admin/CoinBlockBar.svelte";
   import AddProduct from "#components/admin/AddProduct.svelte";
   import LoadFile from "#components/admin/LoadFile.svelte";
   import Footer from "#components/Footer.svelte";
   import { getParamFromUrl } from "#utils/getParam";
   import { verifiyKey } from "#requests/auth";

   let key = getParamFromUrl("key");

   if (key === undefined) {
      location.replace("/");
   }

   let checkPromise = verifiyKey(key);

   checkPromise.then((res) => {
      if (res.status != 200) {
         location.replace("/");
      }
   });
</script>

<main>
   {#await checkPromise then check}
      {#if check.status == 200}
         <Header />
         <div class="container">
            <div class="row mb-4 g-3">
               <div class="col-md-3">
                  <h4>Блокировка монет:</h4>
                  <CoinBlockBar />
               </div>
               <div class="col-md-9">
                  <h4>Добавить новый товар:</h4>
                  <AddProduct />
               </div>
            </div>
            <h4>Загрузить новые товары:</h4>
            <LoadFile classes="mb-4" />
            <h4>Каталог:</h4>
            <ProductsLayout let:prop={card} classes="mb-4" countInRow="3">
               <ProductCardEdit {card} />
            </ProductsLayout>
         </div>
      {/if}
   {/await}
</main>
<Footer />
