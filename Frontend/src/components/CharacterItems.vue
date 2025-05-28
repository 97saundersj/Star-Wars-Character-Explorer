<template>
  <div>
    <div v-if="loading" class="text-center">
      <v-progress-circular indeterminate color="primary" />
    </div>

    <div v-else-if="characters.length === 0" class="text-center">
      <v-alert type="info" variant="tonal" class="mb-4" prepend-icon="mdi-information">
        {{ noResultsText }}
      </v-alert>
    </div>

    <v-row v-else class="g-4">
      <v-col v-for="character in characters" :key="character.name" cols="6" md="4" lg="3">
        <CharacterCard :character="character" />
      </v-col>
    </v-row>
  </div>
</template>

<script setup lang="ts">
import CharacterCard from './CharacterCard.vue'
import type { Character } from '@/types/starWars'

defineProps<{
  loading: boolean
  characters: Character[]
  noResultsText: string
}>()
</script>
