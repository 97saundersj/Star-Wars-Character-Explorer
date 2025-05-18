<template>
  <v-app class="bg-dark">
    <MainNavbar />

    <v-main>
      <v-container class="py-4">
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
import MainNavbar from '@/components/MainNavbar.vue'
import '@/assets/fonts.css'
</script>

<style>
.v-application {
  background: #121212 !important;
}

/* Force Star Wars blue color on all text */
* {
  color: #00bfff !important;
}

/* Apply Aurebesh font when language is set to Aurebesh */
html[data-language='au'] {
  font-family: 'Aurebesh', sans-serif !important;
  font-size: 0.9em !important;
}

html[data-language='au'] * {
  font-family: inherit !important;
}

/* Prevent EN button from changing font */
html[data-language='au'] .v-btn:first-child {
  font-family:
    -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Fira Sans',
    'Droid Sans', 'Helvetica Neue', sans-serif !important;
  font-size: 1em !important;
}
</style>
