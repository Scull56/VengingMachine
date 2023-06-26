import { writable, type Writable } from 'svelte/store';
import { getAvailability } from '#requests/availability';

let availability: Writable<Map<number, boolean> | undefined>

async function init() {
   let res = await getAvailability()

   if (res.status == 200) {

      availability.set(new Map<number, boolean>(
         await res.json()
      ))
   }
}

init()

export default availability = writable(undefined)