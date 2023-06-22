import { writable } from 'svelte/store';

let availability

export default availability = writable(
   new Map<number, boolean>(
      [
         [1, true],
         [2, true],
         [5, true],
         [10, true]
      ]
   )
)