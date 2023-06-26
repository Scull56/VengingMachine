<script>
   export let classes = "";
</script>

<div class="spinner {classes}">
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
   <div />
</div>

<style lang="scss">
   @import "../styles/variables";

   .spinner {
      color: var(--bs-gray);
      display: inline-block;
      position: relative;
      width: 80px;
      height: 80px;
   }
   .spinner div {
      transform-origin: 40px 40px;
      animation: spinner 1.2s linear infinite;
   }
   .spinner div:after {
      content: " ";
      display: block;
      position: absolute;
      top: 3px;
      left: 37px;
      width: 6px;
      height: 18px;
      border-radius: 20%;
      background: var(--bs-gray);
   }

   .spinner_min {
      width: 38px;
      height: 30px;

      div {
         transform-origin: 20px 15px;

         &:after {
            top: 0px;
            left: 19px;
            width: 2px;
            height: 8px;
         }
      }
   }
   .spinner div:nth-child(1) {
      transform: rotate(0deg);
      animation-delay: -1.1s;
   }
   .spinner div:nth-child(2) {
      transform: rotate(30deg);
      animation-delay: -1s;
   }
   .spinner div:nth-child(3) {
      transform: rotate(60deg);
      animation-delay: -0.9s;
   }
   .spinner div:nth-child(4) {
      transform: rotate(90deg);
      animation-delay: -0.8s;
   }
   .spinner div:nth-child(5) {
      transform: rotate(120deg);
      animation-delay: -0.7s;
   }
   .spinner div:nth-child(6) {
      transform: rotate(150deg);
      animation-delay: -0.6s;
   }
   .spinner div:nth-child(7) {
      transform: rotate(180deg);
      animation-delay: -0.5s;
   }
   .spinner div:nth-child(8) {
      transform: rotate(210deg);
      animation-delay: -0.4s;
   }
   .spinner div:nth-child(9) {
      transform: rotate(240deg);
      animation-delay: -0.3s;
   }
   .spinner div:nth-child(10) {
      transform: rotate(270deg);
      animation-delay: -0.2s;
   }
   .spinner div:nth-child(11) {
      transform: rotate(300deg);
      animation-delay: -0.1s;
   }
   .spinner div:nth-child(12) {
      transform: rotate(330deg);
      animation-delay: 0s;
   }
   @keyframes spinner {
      0% {
         opacity: 1;
      }
      100% {
         opacity: 0;
      }
   }
</style>
