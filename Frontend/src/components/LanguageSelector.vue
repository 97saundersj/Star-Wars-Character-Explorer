<template>
  <div>
    <div v-if="showTitle" class="mb-2">{{ languageStore.getLocalizedText('language') }}</div>
    <v-btn-group class="w-100">
      <v-btn
        variant="outlined"
        :color="languageStore.currentLanguage === 'en' ? 'primary' : undefined"
        :class="{ 'selected-language': languageStore.currentLanguage === 'en' }"
        @click="languageStore.setLanguage('en')"
        class="flex-grow-1"
      >
        <span :class="{ 'white-text': languageStore.currentLanguage === 'en' }">EN</span>
      </v-btn>
      <v-btn
        variant="outlined"
        :color="languageStore.currentLanguage === 'au' ? 'primary' : undefined"
        :class="{ 'selected-language': languageStore.currentLanguage === 'au' }"
        @click="languageStore.setLanguage('au')"
        class="flex-grow-1"
      >
        <span class="aurebesh" :class="{ 'white-text': languageStore.currentLanguage === 'au' }">AU</span>
      </v-btn>
      <v-btn
        variant="outlined"
        :color="languageStore.currentLanguage === 'fr' ? 'primary' : undefined"
        :class="{ 'selected-language': languageStore.currentLanguage === 'fr' }"
        @click="languageStore.setLanguage('fr')"
        class="flex-grow-1"
      >
        <span class="default-font" :class="{ 'white-text': languageStore.currentLanguage === 'fr' }">FR</span>
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

<style scoped>
.selected-language {
  background-color: rgb(var(--v-theme-primary)) !important;
}

.white-text {
  color: white !important;
}

/* Override the global text color for unselected buttons */
.v-btn:not(.selected-language) {
  color: rgb(var(--v-theme-primary)) !important;
}

.default-font {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif !important;
  font-size: 1em !important;
}
</style>
