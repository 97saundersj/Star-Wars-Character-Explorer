<template>
  <BFormGroup class="mb-5">
    <BInputGroup size="lg">
      <BInputGroupText class="bg-dark text-light border-secondary">
        <i class="bi bi-search"></i>
      </BInputGroupText>
      <BFormInput
        type="text"
        class="bg-dark text-light border-secondary"
        :placeholder="languageStore.t('searchCharacters')"
        v-model="store.searchQuery"
        @input="handleSearch"
      />
      <BButton
        v-if="store.searchQuery"
        variant="outline-secondary"
        class="text-light border-secondary"
        @click="clearSearch"
        :title="languageStore.t('clearSearch')"
      >
        <i class="bi bi-x-lg"></i>
      </BButton>
    </BInputGroup>
  </BFormGroup>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { BFormGroup, BInputGroup, BInputGroupText, BFormInput, BButton } from 'bootstrap-vue-next'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const searchTimeout = ref<number | null>(null)

const handleSearch = async (event: Event) => {
  const target = event.target as HTMLInputElement
  if (searchTimeout.value) {
    clearTimeout(searchTimeout.value)
  }
  searchTimeout.value = window.setTimeout(async () => {
    await store.searchCharacters(target.value)
  }, 300)
}

const clearSearch = async () => {
  store.searchQuery = ''
  await store.fetchCharacters(1)
}
</script>

<style scoped>
</style>
