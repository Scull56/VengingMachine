import products from "#data/products";
import { getProducts } from "#requests/products";
import type ProductCard from "#types/ProductCard";

export default async function updateProducts() {
   let res = await getProducts()

   if (res.status == 200) {

      let cards: ProductCard[] = await res.json()

      products.set(cards)
   }

   return res.status == 200
}