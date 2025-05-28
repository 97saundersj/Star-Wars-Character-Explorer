<template>
  <RouterLink
    :to="{ name: 'character-detail', params: { name: character.name } }"
    class="text-decoration-none d-block"
  >
    <v-card class="h-100 position-relative" variant="outlined">
      <div style="position: absolute; top: 5px; right: 5px; z-index: 10">
        <v-btn
          icon
          variant="text"
          color="error"
          density="compact"
          @click.prevent="store.toggleLike(character._id)"
        >
          <v-icon
            :icon="character.isLiked ? 'mdi-heart' : 'mdi-heart-outline'"
            size="large"
          ></v-icon>
        </v-btn>
      </div>

      <v-card-title class="text-subtitle-1 pe-8">
        <span class="text-truncate">{{ character.name }}</span>
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
    _id: string
  }
}>()

const store = useCharacterStore()
const imageLoaded = ref(false)
</script>
