<script lang="ts">
   import Header from "#components/Header.svelte";
   import Footer from "#components/Footer.svelte";
   import Balance from "#components/main/Balance.svelte";
   import DepositBar from "#components/main/DepositBar.svelte";
   import Deposit from "#components/main/Deposit.svelte";
   import ProductsLayout from "#components/ProductsLayout.svelte";
   import ProductCard from "#components/main/ProductCard.svelte";
</script>

<main>
   <Header />
   <div class="container">
      <div class="row mb-4 g-4">
         <div class="col-lg-4">
            <h4>Баланс:</h4>
            <Balance />
         </div>
         <div class="col-lg-4">
            <h4>Внести деньги:</h4>
            <DepositBar />
         </div>
         <div class="col-lg-4">
            <h4>Депозит:</h4>
            <Deposit />
         </div>
      </div>
      <h4>Выберите товар:</h4>
      <ProductsLayout let:prop={card}>
         <ProductCard {card} />
      </ProductsLayout>
   </div>
</main>
<Footer />

<style>
   h4 {
      margin-right: 10px;
   }
</style>
