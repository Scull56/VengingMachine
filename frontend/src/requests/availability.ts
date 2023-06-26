export async function getAvailability() {
   return await fetch('/Availability/Get')
}

export async function setAvailability(key: string, denomination: number, state: boolean) {
   return await fetch(`/Availability/Set?key=${key}&denomination=${denomination}&state=${state}`, {
      method: 'PUT'
   })
}