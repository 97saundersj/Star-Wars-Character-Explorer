<template>
  <v-card class="mb-4" variant="outlined">
    <v-card-title class="text-h5">
      {{ name }}
    </v-card-title>

    <v-card-text>
      <v-row>
        <v-col cols="12" md="4" class="mb-3 mb-md-0">
          <div class="position-relative bg-dark rounded overflow-hidden">
            <div
              v-if="!imageLoaded"
              class="position-absolute top-0 start-0 w-100 h-100 d-flex align-center justify-center bg-dark bg-opacity-50"
            >
              <v-progress-circular indeterminate color="primary"></v-progress-circular>
            </div>

            <v-img
              :src="image"
              :alt="name"
              class="w-100 rounded character-image"
              @load="imageLoaded = true"
              @error="imageLoaded = true"
            />
          </div>
        </v-col>

        <v-col cols="12" md="8">
          <p class="mb-4">{{ description }}</p>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { ref } from 'vue'

defineProps<{
  name: string
  description: string
  image: string
}>()

const imageLoaded = ref(false)
</script>

<style scoped>
.character-image {
  max-height: 600px;
  object-fit: contain;
  transition: transform 0.3s ease;
}

.character-image:hover {
  transform: scale(1.05);
  filter: brightness(1.1);
}
</style>
