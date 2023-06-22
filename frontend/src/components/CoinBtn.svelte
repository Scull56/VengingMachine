<script lang="ts">
   export let classes: string = "";
   export let denomination: number;
   export let state: boolean = true;
</script>

<button
   type="button"
   class="btn coin {classes} {!state ? 'disabled' : ''}"
   on:click
>
   {denomination}
</button>

<style lang="scss">
   .coin {
      position: relative;
      margin-right: 3px;
      color: var(--bs-gray-900);
      font-weight: 600;
      z-index: 1;

      &::before {
         position: absolute;
         z-index: -1;
         top: 50%;
         left: 50%;
         transform: translate(-50%, -50%);
         content: "";
         width: 30px;
         height: 30px;
         background-color: var(--bs-warning);
         border-radius: 15px;
         transition: all var(--ts);
      }

      &:hover {
         cursor: pointer;

         &::before {
            background-color: #ffcd39;
         }
      }

      &:active {
         border-color: transparent;
      }

      &.disabled {
         pointer-events: all;
         border-color: transparent;
      }
   }
</style>
