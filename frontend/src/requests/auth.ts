export async function verifiyKey(key: string) {
   return await fetch(`/Auth/VerifyKey?key=${key}`)
}