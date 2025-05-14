<template>
  <div>
    <h1 class="text-light mb-4">{{ languageStore.t('appTitle') }}</h1>

    <div v-if="store.loading" class="text-center">
      <div class="spinner-border text-light" role="status">
        <span class="visually-hidden">{{ languageStore.t('loading') }}</span>
      </div>
    </div>

    <div v-else-if="store.error" class="alert alert-danger">
      {{ languageStore.t('error') }}
    </div>

    <div v-else class="row g-4">
      <div v-for="character in store.characters" :key="character.name" class="col-12 col-sm-6 col-md-4">
        <div class="card bg-dark text-light border-secondary h-100">
          <div class="card-header d-flex justify-content-between align-items-center border-secondary">
            <h5 class="card-title mb-0">{{ character.name }}</h5>
            <button
              class="btn btn-link text-danger p-0"
              @click="store.toggleLike(character.name)"
            >
              <i :class="character.isLiked ? 'bi bi-heart-fill' : 'bi bi-heart'"></i>
            </button>
          </div>
          <div class="card-body">
            <p class="card-text"><strong>{{ languageStore.t('birthYear') }}:</strong> {{ character.birth_year }}</p>
            <p class="card-text"><strong>{{ languageStore.t('gender') }}:</strong> {{ character.gender }}</p>
          </div>
          <div class="card-footer border-secondary">
            <RouterLink
              :to="{ name: 'character-detail', params: { name: character.name }}"
              class="btn btn-primary"
            >
              {{ languageStore.t('viewDetails') }}
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const store = useCharacterStore()
const languageStore = useLanguageStore()

onMounted(() => {
  store.fetchCharacters()
})
</script>

<style scoped>
.card {
  transition: transform 0.2s;
}

.card:hover {
  transform: translateY(-5px);
}
</style>
