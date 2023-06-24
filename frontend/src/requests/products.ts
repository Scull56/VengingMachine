export async function getProducts() {
   return await fetch('/Products/GetProducts')
}

export async function addProduct(key: string, form: HTMLFormElement) {
   return await fetch(`/Products/Add?key=${key}`, {
      method: "POST",
      body: new FormData(form),
   })
}

export async function addProducts(key: string, form: HTMLFormElement) {
   return await fetch(`/Products/AddProducts?key=${key}`, {
      method: "POST",
      body: new FormData(form),
   })
}

export async function updateProduct(key: string, form: HTMLFormElement) {
   return await fetch(`/Products/Update?key=${key}`, {
      method: "PUT",
      body: new FormData(form)
   })
}

export async function deleteProduct(key: string, id: number) {
   return await fetch(`/Products/Delete?key=${key}&id=${id}`, {
      method: "DELETE"
   })
}