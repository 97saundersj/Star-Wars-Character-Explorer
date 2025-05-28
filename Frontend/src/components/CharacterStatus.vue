<template>
  <div v-if="loading" class="d-flex justify-center align-center my-5">
    <v-progress-circular indeterminate color="primary" size="48"></v-progress-circular>
  </div>

  <v-alert v-else-if="characters.length === 0" type="info" variant="tonal" class="mb-4" prepend-icon="mdi-information">
    {{ languageStore.t('noResults') }}
  </v-alert>
</template>

<script setup lang="ts">
import { useLanguageStore } from '@/stores/languageStore'

defineProps<{
  loading: boolean
  characters: Array<{ name: string }>
}>()

const languageStore = useLanguageStore()
</script>
