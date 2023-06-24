export function getParamFromUrl(param: string) {
   let params = location.search;
   let result: string | undefined

   if (params.length == 0) {
      return result
   }

   params = params.slice(1);

   params
      .split("&")
      .find(
         (item) => {
            let [name, value] = item.split('=');

            if (name == param) {

               result = value

               return true
            }

            return false
         }
      );

   return result;
}