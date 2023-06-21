import { writable } from 'svelte/store';

let balance

export default balance = writable(
   new Map<number, number>(
      [
         [1, 5],
         [2, 4],
         [5, 3],
         [10, 8]
      ]
   )
)