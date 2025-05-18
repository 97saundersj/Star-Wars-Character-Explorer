<template>
  <BCard bg-variant="dark" text-variant="light" border-variant="secondary" class="mb-4">
    <template #header>
      <h2 class="card-title mb-0">{{ name }}</h2>
    </template>

    <BRow>
      <BCol md="4" class="mb-3 mb-md-0">
        <div class="position-relative bg-dark rounded overflow-hidden">
          <div
            v-if="!imageLoaded"
            class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center bg-dark bg-opacity-50"
          >
            <BSpinner class="text-light" style="width: 3rem; height: 3rem" />
          </div>

          <BImg
            :src="image"
            :alt="name"
            fluid
            class="w-100 rounded character-image"
            @load="imageLoaded = true"
            @error="imageLoaded = true"
          />
        </div>
      </BCol>

      <BCol md="8">
        <p class="mb-4">{{ description }}</p>
      </BCol>
    </BRow>
  </BCard>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { BCard, BRow, BCol, BImg, BSpinner } from 'bootstrap-vue-next'

defineProps<{
  name: string
  description: string
  image: string
}>()

const imageLoaded = ref(false)
</script>
