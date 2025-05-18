<template>
  <div>
    <div v-if="showTitle" class="mb-2">{{ languageStore.t('language') }}</div>
    <v-btn-group class="w-100">
      <v-btn
        variant="outlined"
        :color="languageStore.currentLanguage === 'en' ? 'primary' : undefined"
        @click="languageStore.setLanguage('en')"
        class="flex-grow-1"
      >
        EN
      </v-btn>
      <v-btn
        variant="outlined"
        :color="languageStore.currentLanguage === 'au' ? 'primary' : undefined"
        @click="languageStore.setLanguage('au')"
        class="flex-grow-1"
      >
        <span class="aurebesh">AU</span>
      </v-btn>
    </v-btn-group>
  </div>
</template>

<script setup lang="ts">
import { useLanguageStore } from '@/stores/languageStore'

defineProps<{
  showTitle?: boolean
}>()

const languageStore = useLanguageStore()
</script>
