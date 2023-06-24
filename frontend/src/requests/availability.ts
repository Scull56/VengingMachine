export async function getAvailability() {
   return await fetch('/Availability/GetAvailability')
}

export async function setAvailability(key: string, denomination: number, state: boolean) {
    return await fetch(`/Availability/SetAvailability?key=${key}&denomination=${denomination}&state=${state}`, {
      method: 'PUT'
   })
}