<template>
  <v-container class="py-4" data-testid="mock-character-list">
    <div class="mb-5">
      <h1 class="text-h3 font-weight-bold mb-3">{{ languageStore.getLocalizedText('characters') }}</h1>
    </div>

    <CharacterSearch />

    <CharacterPagination />

    <CharacterItems
      :loading="store.loading"
      :characters="store.characters"
      :no-results-text="languageStore.getLocalizedText('noResults')"
    />

    <CharacterPagination />
  </v-container>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { useToast } from 'vue-toastification'
import CharacterPagination from '@/components/CharacterPagination.vue'
import CharacterSearch from '@/components/CharacterSearch.vue'
import CharacterItems from '@/components/CharacterItems.vue'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const toast = useToast()

onMounted(async () => {
  try {
    await store.fetchCharacters()
  } catch {
    toast.error(languageStore.getLocalizedText('errorFetchingCharacters'))
  }
})
</script>
