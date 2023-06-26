import { defineConfig } from 'vite'
import { tsconfigPaths } from 'vite-plugin-lib'
import { svelte } from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/
export default defineConfig({
   plugins: [tsconfigPaths(), svelte()],
   build: {
      outDir: '../wwwroot'
   }
})
