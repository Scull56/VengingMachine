export function getParamFromUrl(param: string) {
   let params = location.search;
   let result: string | undefined

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