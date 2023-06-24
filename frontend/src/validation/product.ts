// 1 MB
const maxFileSize = 1048576;

export function title(title: string) {
   return !(title.length < 2 || title.length > 30)
}

export function price(price: number | null) {
   return !(price === null || price < 1)
}

export function count(count: number | null) {
   return !(count === null || count < 0)
}

export function image(files: FileList | undefined) {
   let supportedFileExtensions = ["image/jpg", "image/jpeg"];

   return !(
      files === undefined ||
      files[0] === undefined ||
      supportedFileExtensions.indexOf(files[0].type) == -1 ||
      files[0].size > maxFileSize
   )
}

export function file(files: FileList | undefined) {
   let supportedFileExtensions = ["application/vnd.ms-excel"];

   return !(
      files === undefined ||
      files[0] === undefined ||
      supportedFileExtensions.indexOf(files[0].type) == -1 ||
      files[0].size > maxFileSize
   )
}