import { writable } from 'svelte/store';

let earnings

export default earnings = writable(
   new Map<number, number>(
      [
         [1, 10],
         [2, 10],
         [5, 10],
         [10, 10]
      ]
   )
)