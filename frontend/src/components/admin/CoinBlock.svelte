<script lang="ts">
   import CoinBtn from "#components/CoinBtn.svelte";
   import availability from "#data/availability";
   import { setAvailability } from "#requests/availability";
   import { getContext } from "svelte";
   import errorState from "#data/error";

   let key: string = getContext("key");

   export let denomination: number;
   export let state: boolean;
   export let classes: string = "";

   async function changeState() {
      let res = await setAvailability(key, denomination, !state);

      if (res.status == 200) {
         state = !state;

         $availability.set(denomination, state);

         availability.set($availability);
      } else {
         let message =
            res.status == 500
               ? `Ошибка на сервере, повторите попытку позже`
               : `Ошибка ${res.status}: ${(await res.json()).message}`;

         errorState.set({ state: true, message });
      }
   }
</script>

<div class="d-flex align-items-center {classes}">
   <CoinBtn {denomination} classes="me-1" {state} on:click={changeState} />
</div>
