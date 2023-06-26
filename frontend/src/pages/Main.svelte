<script lang="ts">
   import Layout from "#components/Layout.svelte";
   import Balance from "#components/main/Balance.svelte";
   import DepositBar from "#components/main/DepositBar.svelte";
   import Deposit from "#components/main/Deposit.svelte";
   import ProductsLayout from "#components/ProductsLayout.svelte";
   import ProductCard from "#components/main/ProductCard.svelte";
   import ResponseError from "#components/ResponseError.svelte";
   import error from "#data/error";

   let errorState;
   let errorMessage;

   $: errorState = $error.state;
   $: errorMessage = $error.message;
</script>

<Layout>
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
</Layout>
<ResponseError />

<style>
   h4 {
      margin-right: 10px;
   }
</style>
