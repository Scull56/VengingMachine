<script lang="ts">
   import deposit from "#data/deposit";
   import earnings from "#data/earnings";
   import balance from "#data/balance";
   import mapToArray from "#utils/mapToArray";

   let state: boolean = $deposit > 0;

   $: state = $deposit > 0;

   function getReminder() {
      if (state) {
         let reminder = $deposit;
         let earningsArray = mapToArray($earnings).sort((a, b) => b[0] - a[0]);
         let index = 0;

         console.log(earningsArray);

         while (reminder > 0) {
            if (earningsArray[index][1] > 0) {
               if (reminder - earningsArray[index][0]) {
                  --earningsArray[index][1];
                  $earnings.set(
                     earningsArray[index][0],
                     earningsArray[index][1]
                  );
                  reminder -= earningsArray[index][0];
               }
            } else {
               ++index;
            }
         }

         earnings.set($earnings);
         balance.set($balance);
         deposit.set(0);
      }
   }
</script>

<div class="d-flex align-items-center">
   <h3 class="me-5">{$deposit} руб.</h3>
   <button
      type="button"
      class="btn btn-outline-secondary {!state ? 'disabled' : ''}"
      on:click={getReminder}
   >
      Получить сдачу
   </button>
</div>
