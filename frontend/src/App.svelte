<script>
   import Main from "./pages/Main.svelte";
   import Admin from "./pages/Admin.svelte";

   let pages = {
      "/": Main,
      "/admin": Admin,
   };

   let page = location.pathname;
</script>

<svelte:component this={pages[page] ? pages[page] : Main} />
