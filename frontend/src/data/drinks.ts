import { writable } from 'svelte/store';
import type DrinkCard from '#types/DrinkCard';

let drinks

export default drinks = writable(
   [
      {
         id: 1,
         imgSrc: "/assets/images/drinks/1.jpg",
         title: "Colaaaaaaaaaaaa aaaaaaaaaaaaaaaaaa aaaaaaaaaaaaaaaaaaaa aaaaaaaaaaaaaaaaa",
         price: 80,
         count: 4,
      },
      {
         id: 2,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 0,
      },
      {
         id: 3,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
      {
         id: 4,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
      {
         id: 5,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
      {
         id: 6,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
      {
         id: 7,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
      {
         id: 8,
         imgSrc: "../../../wwwroot/assets/images/drinks/1.jpg",
         title: "Cola",
         price: 80,
         count: 4,
      },
   ] as DrinkCard[]
)