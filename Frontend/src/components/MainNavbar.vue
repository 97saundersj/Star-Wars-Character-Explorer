<template>
  <v-app-bar color="dark" class="border-b border-secondary">
    <v-container class="d-flex align-center">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <router-link to="/" class="text-decoration-none">
        <v-app-bar-title class="text-primary font-weight-bold">
          <v-icon icon="mdi-star" class="me-2"></v-icon>{{ languageStore.t('appTitle') }}
        </v-app-bar-title>
      </router-link>
    </v-container>
  </v-app-bar>

  <v-navigation-drawer v-model="drawer">
    <v-list>
      <template v-for="item in navItems" :key="item.to">
        <v-list-item :to="item.to" :prepend-icon="item.icon" :title="item.title"></v-list-item>
      </template>
    </v-list>

    <template v-slot:append>
      <div class="pa-2">
        <LanguageSelector />
      </div>
    </template>
  </v-navigation-drawer>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useLanguageStore } from '@/stores/languageStore'
import LanguageSelector from './LanguageSelector.vue'

defineOptions({
  name: 'MainNavbar',
})

const languageStore = useLanguageStore()
const drawer = ref(false)

const navItems = [
  {
    title: 'Characters',
    to: '/',
    icon: 'mdi-account-card',
  },
  {
    title: 'About',
    to: '/about',
    icon: 'mdi-information',
  },
]
</script>
