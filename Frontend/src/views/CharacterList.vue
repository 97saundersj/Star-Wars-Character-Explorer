<template>
  <BContainer class="py-4">
    <div class="mb-5">
      <h1 class="text-light display-4 fw-bold mb-3">{{ languageStore.t('appTitle') }}</h1>
    </div>

    <CharacterSearch />

    <CharacterPagination />

    <CharacterStatus :loading="store.loading" :error="store.error" :characters="store.characters" />

    <BRow v-if="!store.loading && !store.error && store.characters.length > 0" class="g-4">
      <BCol v-for="character in store.characters" :key="character.name" cols="6" md="4" lg="3">
        <CharacterCard :character="character" />
      </BCol>
    </BRow>

    <CharacterPagination :is-bottom="true" />
  </BContainer>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import CharacterPagination from '@/components/CharacterPagination.vue'
import CharacterSearch from '@/components/CharacterSearch.vue'
import CharacterCard from '@/components/CharacterCard.vue'
import CharacterStatus from '@/components/CharacterStatus.vue'
import { BContainer, BRow, BCol } from 'bootstrap-vue-next'

const store = useCharacterStore()
const languageStore = useLanguageStore()

onMounted(async () => {
  await store.fetchCharacters()
})
</script>
