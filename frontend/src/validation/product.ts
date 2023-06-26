import conf from "#data/config";

const maxFileSize = conf.fileSizeLimit;

let supportedImagesExtensions = ["image/jpg", "image/jpeg"];

export function title(title: string) {
   return title.length > 2 && title.length < 30
}

export function price(price: number | null) {
   return price != null && price > 0
}

export function count(count: number | null) {
   return count != null && count >= 0
}

export function image(files: FileList | undefined) {

   return (
      files != undefined &&
      files[0] != undefined &&
      supportedImagesExtensions.indexOf(files[0].type) > -1 &&
      files[0].size <= maxFileSize
   )
}

export function images(files: FileList | undefined) {
   let check = true;

   check = files != undefined && files.length > 0

   if (check) {

      for (let i = 0; i < files.length; i++) {
         let image = files[i];

         if (supportedImagesExtensions.indexOf(image.type) == -1 || image.size > maxFileSize) {
            check = false

            break
         }
      }
   }

   return check
}

export function table(files: FileList | undefined) {
   let supportedFileExtensions = ["application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"];

   return (
      files != undefined &&
      files[0] != undefined &&
      supportedFileExtensions.indexOf(files[0].type) > -1 &&
      files[0].size <= maxFileSize
   )
}