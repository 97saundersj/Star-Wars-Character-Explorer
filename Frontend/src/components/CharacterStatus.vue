<template>
  <div v-if="loading" class="d-flex justify-content-center align-items-center my-5">
    <BSpinner class="text-light" style="width: 3rem; height: 3rem" />
  </div>

  <BAlert v-else-if="characters.length === 0" variant="dark" show class="mb-4">
    <i class="bi bi-info-circle-fill me-2"></i>
    {{ languageStore.t('noResults') }}
  </BAlert>
</template>

<script setup lang="ts">
import { useLanguageStore } from '@/stores/languageStore'
import { BSpinner, BAlert } from 'bootstrap-vue-next'

defineProps<{
  loading: boolean
  characters: Array<{ name: string }>
}>()

const languageStore = useLanguageStore()
</script>
