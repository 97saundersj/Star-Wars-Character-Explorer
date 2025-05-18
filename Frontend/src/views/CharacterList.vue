<template>
  <v-container class="py-4" data-testid="mock-character-list">
    <div class="mb-5">
      <h1 class="text-h3 font-weight-bold mb-3">{{ languageStore.t('characters') }}</h1>
    </div>

    <CharacterSearch />

    <CharacterPagination />

    <CharacterStatus :loading="store.loading" :characters="store.characters" />

    <v-row v-if="!store.loading && store.characters.length > 0" class="g-4">
      <v-col v-for="character in store.characters" :key="character.name" cols="6" md="4" lg="3">
        <CharacterCard :character="character" />
      </v-col>
    </v-row>

    <CharacterPagination :is-bottom="true" />
  </v-container>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { useToast } from 'vue-toastification'
import CharacterPagination from '@/components/CharacterPagination.vue'
import CharacterSearch from '@/components/CharacterSearch.vue'
import CharacterCard from '@/components/CharacterCard.vue'
import CharacterStatus from '@/components/CharacterStatus.vue'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const toast = useToast()

onMounted(async () => {
  try {
    await store.fetchCharacters()
  } catch {
    toast.error('Error fetching characters.\nI have a bad feeling about this...')
  }
})
</script>
