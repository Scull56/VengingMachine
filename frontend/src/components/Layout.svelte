<script>
   import Footer from "#components/Footer.svelte";
   import Header from "#components/Header.svelte";
</script>

<div class="wrapper">
   <Header />
   <main>
      <slot />
   </main>
</div>
<Footer />

<style>
   :global(#app) {
      display: flex;
      flex-direction: column;
      height: 100%;
   }
   .wrapper {
      flex-grow: 1;
   }
</style>
