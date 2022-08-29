import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  base: '/dist/',
  build: {
    emptyOutDir: true,
    manifest: true
  },
  server: {
    strictPort: true,
    proxy: {
      '/api': { target: 'https://localhost:7242', secure: false }
    },
    hmr: {
      protocol: 'ws'
    }
  },
  plugins: [vue()]
})
