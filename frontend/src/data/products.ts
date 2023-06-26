import { writable, type Writable } from 'svelte/store';
import type ProductCard from '#types/ProductCard';
import { getProducts } from '#requests/products';

let products: Writable<ProductCard[]>

async function init() {
   let res = await getProducts()

   if (res.status == 200) {

      products.set(await res.json())
   }
}

init()

export default products = writable(undefined)