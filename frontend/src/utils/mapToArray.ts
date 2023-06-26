export default function mapToArray(map: Map<any, any>): any[][] {
   let arr: any[][] = []

   let keys = map.entries();

   map.forEach((item) => arr.push([keys.next().value[0], item]));

   return arr
}