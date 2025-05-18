<template>
  <v-form class="mb-5">
    <v-text-field
      v-model="store.searchQuery"
      :placeholder="languageStore.t('searchCharacters')"
      variant="outlined"
      density="comfortable"
      hide-details
      @input="handleSearch"
    >
      <template v-slot:prepend>
        <v-icon icon="mdi-magnify"></v-icon>
      </template>
      <template v-slot:append v-if="store.searchQuery">
        <v-btn
          variant="text"
          icon="mdi-close"
          size="small"
          @click="clearSearch"
          :title="languageStore.t('clearSearch')"
        ></v-btn>
      </template>
    </v-text-field>
  </v-form>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

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

<style scoped></style>
