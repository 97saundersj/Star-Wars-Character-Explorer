<template>
  <div>
    <h1 class="text-light mb-4">{{ languageStore.t('appTitle') }}</h1>

    <div class="mb-4">
      <div class="input-group">
        <span class="input-group-text bg-dark text-light border-secondary">
          <i class="bi bi-search"></i>
        </span>
        <input
          type="text"
          class="form-control bg-dark text-light border-secondary"
          :placeholder="languageStore.t('searchCharacters')"
          :value="store.searchQuery"
          @input="handleSearch"
        >
        <button
          v-if="store.searchQuery"
          class="btn btn-outline-secondary text-light border-secondary"
          type="button"
          @click="clearSearch"
          :title="languageStore.t('clearSearch')"
        >
          <i class="bi bi-x-lg"></i>
        </button>
      </div>
    </div>

    <div v-if="store.loading" class="text-center my-4">
      <div class="spinner-border text-light" role="status">
        <span class="visually-hidden">{{ languageStore.t('loading') }}</span>
      </div>
    </div>

    <div v-else-if="store.error" class="alert alert-danger">
      {{ languageStore.t('error') }}
    </div>

    <div v-else>
      <div v-if="store.characters.length === 0" class="alert alert-info bg-dark text-light border-secondary">
        {{ languageStore.t('noResults') }}
      </div>
      <div v-else class="row g-4 mb-4">
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

      <!-- Bootstrap Pagination -->
      <nav aria-label="Character list pagination">
        <ul class="pagination justify-content-center">
          <li class="page-item" :class="{ disabled: !store.hasPreviousPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.previousPage">
              <i class="bi bi-chevron-left"></i>
            </button>
          </li>
          <li v-for="page in store.totalPages" :key="page" class="page-item" :class="{ active: page === store.currentPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(page)">
              {{ page }}
            </button>
          </li>
          <li class="page-item" :class="{ disabled: !store.hasNextPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.nextPage">
              <i class="bi bi-chevron-right"></i>
            </button>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const searchTimeout = ref<number | null>(null)

const handleSearch = (event: Event) => {
  const target = event.target as HTMLInputElement
  if (searchTimeout.value) {
    clearTimeout(searchTimeout.value)
  }
  searchTimeout.value = window.setTimeout(() => {
    store.searchCharacters(target.value)
  }, 300)
}

const clearSearch = () => {
  store.searchQuery = ''
  store.fetchCharacters(1)
}

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

.page-link {
  transition: all 0.2s;
}

.page-link:hover {
  background-color: #2b3035 !important;
  border-color: #6c757d !important;
}

.page-item.active .page-link {
  background-color: #0d6efd !important;
  border-color: #0d6efd !important;
}

.page-item.disabled .page-link {
  background-color: #212529 !important;
  border-color: #6c757d !important;
  opacity: 0.5;
}

::placeholder {
  color: #6c757d !important;
  opacity: 1;
}

:-ms-input-placeholder {
  color: #6c757d !important;
}

::-ms-input-placeholder {
  color: #6c757d !important;
}
</style>
