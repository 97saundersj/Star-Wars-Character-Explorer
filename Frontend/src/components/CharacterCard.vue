<template>
  <RouterLink
    :to="{ name: 'character-detail', params: { name: character.name }}"
    class="text-decoration-none d-block"
  >
    <BCard class="bg-dark text-light border-secondary h-100 transition-transform">

      <template #header>
        <div class="d-flex justify-content-between align-items-center">
          <h6 class="card-title mb-0 text-truncate fw-medium">{{ character.name }}</h6>

          <BButton
            variant="link"
            class="text-danger p-0 border-0 shadow-none"
            @click.prevent="store.toggleLike(character.name)"
          >
            <i :class="character.isLiked ? 'bi bi-heart-fill' : 'bi bi-heart'" class="fs-5"></i>
          </BButton>
        </div>
      </template>

      <div class="position-relative" style="aspect-ratio: 16/9;">
        <div v-if="!imageLoaded" class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center bg-dark bg-opacity-50">
          <BSpinner class="text-light" />
        </div>

        <img
          :src="character.image"
          :alt="character.name"
          class="w-100 h-100 object-fit-cover"
          loading="lazy"
          @load="imageLoaded = true"
          @error="imageLoaded = true"
        >
      </div>
    </BCard>

  </RouterLink>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { BCard, BButton, BSpinner } from 'bootstrap-vue-next'

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

<style scoped>
:deep(.card-body) {
  padding: 0;
}

.transition-transform {
  transition: transform 0.3s ease;
}

.transition-transform:hover {
  transform: translateY(-2px);
}

.transition-transform:hover img {
  transform: scale(1.05);
}

img {
  transition: transform 0.3s ease;
}
</style>
