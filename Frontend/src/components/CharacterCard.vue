<template>
  <RouterLink
    :to="{ name: 'character-detail', params: { name: character.name } }"
    class="text-decoration-none d-block"
  >
    <v-card class="h-100" variant="outlined">
      <v-card-title class="d-flex justify-space-between align-center">
        <span class="text-truncate">{{ character.name }}</span>
        <v-btn
          variant="text"
          color="error"
          class="p-0"
          @click.prevent="store.toggleLike(character.name)"
        >
          <v-icon
            :icon="character.isLiked ? 'mdi-heart' : 'mdi-heart-outline'"
            size="small"
          ></v-icon>
        </v-btn>
      </v-card-title>

      <div class="position-relative" style="aspect-ratio: 16/9">
        <div
          v-if="!imageLoaded"
          class="position-absolute top-0 start-0 w-100 h-100 d-flex align-center justify-center bg-dark bg-opacity-50"
        >
          <v-progress-circular indeterminate color="primary"></v-progress-circular>
        </div>

        <v-img
          :src="character.image"
          :alt="character.name"
          loading="lazy"
          cover
          position="top"
          height="100%"
          @load="imageLoaded = true"
          @error="imageLoaded = true"
        />
      </div>
    </v-card>
  </RouterLink>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'

defineProps<{
  character: {
    name: string
    image: string
    isLiked: boolean
  }
}>()

const store = useCharacterStore()
const imageLoaded = ref(false)
</script>
