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

    <!-- Page Size Selector and Top Pagination -->
    <div class="d-flex justify-content-between align-items-center mb-4">
      <div class="d-flex align-items-center">
        <label for="pageSize" class="text-light me-2">{{ languageStore.t('displayLimit') }}:</label>
        <select
          id="pageSize"
          class="form-select bg-dark text-light border-secondary"
          style="width: auto;"
          v-model="store.pageSize"
          @change="handlePageSizeChange"
        >
          <option value="12">12</option>
          <option value="24">24</option>
          <option value="48">48</option>
          <option value="96">96</option>
        </select>
      </div>
    </div>

    <!-- Top Pagination -->
    <nav aria-label="Character list pagination top" class="mb-4">
      <ul class="pagination justify-content-center">
        <li class="page-item" :class="{ disabled: !store.hasPreviousPage }">
          <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(1)" :disabled="!store.hasPreviousPage">
            <i class="bi bi-chevron-double-left"></i>
          </button>
        </li>
        <li class="page-item" :class="{ disabled: !store.hasPreviousPage }">
          <button class="page-link bg-dark text-light border-secondary" @click="store.previousPage" :disabled="!store.hasPreviousPage">
            <i class="bi bi-chevron-left"></i>
          </button>
        </li>
        <template v-for="page in store.totalPages" :key="page">
          <li v-if="shouldShowPage(page)" class="page-item" :class="{ active: page === store.currentPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(page)">
              {{ page }}
            </button>
          </li>
          <li v-else-if="shouldShowEllipsis(page)" class="page-item disabled">
            <span class="page-link bg-dark text-light border-secondary">...</span>
          </li>
        </template>
        <li class="page-item" :class="{ disabled: !store.hasNextPage }">
          <button class="page-link bg-dark text-light border-secondary" @click="store.nextPage" :disabled="!store.hasNextPage">
            <i class="bi bi-chevron-right"></i>
          </button>
        </li>
        <li class="page-item" :class="{ disabled: !store.hasNextPage }">
          <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(store.totalPages)" :disabled="!store.hasNextPage">
            <i class="bi bi-chevron-double-right"></i>
          </button>
        </li>
      </ul>
    </nav>

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
      <div v-else class="row g-3 mb-4">
        <div v-for="character in store.characters" :key="character.name" class="col-6 col-sm-4 col-md-3 col-lg-2">
          <div class="card bg-dark text-light border-secondary h-100">
            <div class="card-header d-flex justify-content-between align-items-center border-secondary py-2">
              <h6 class="card-title mb-0 text-truncate">{{ character.name }}</h6>
              <button
                class="btn btn-link text-danger p-0"
                @click="store.toggleLike(character.name)"
              >
                <i :class="character.isLiked ? 'bi bi-heart-fill' : 'bi bi-heart'"></i>
              </button>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="character-image-container">
                <img
                  :src="character.image"
                  :alt="character.name"
                  class="img-fluid rounded-bottom character-image"
                  loading="lazy"
                >
              </div>
            </div>
            <div class="card-footer border-secondary py-2">
              <RouterLink
                :to="{ name: 'character-detail', params: { name: character.name }}"
                class="btn btn-primary btn-sm w-100"
              >
                {{ languageStore.t('viewDetails') }}
              </RouterLink>
            </div>
          </div>
        </div>
      </div>

      <!-- Bottom Pagination -->
      <nav aria-label="Character list pagination bottom">
        <ul class="pagination justify-content-center">
          <li class="page-item" :class="{ disabled: !store.hasPreviousPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(1)" :disabled="!store.hasPreviousPage">
              <i class="bi bi-chevron-double-left"></i>
            </button>
          </li>
          <li class="page-item" :class="{ disabled: !store.hasPreviousPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.previousPage" :disabled="!store.hasPreviousPage">
              <i class="bi bi-chevron-left"></i>
            </button>
          </li>
          <template v-for="page in store.totalPages" :key="page">
            <li v-if="shouldShowPage(page)" class="page-item" :class="{ active: page === store.currentPage }">
              <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(page)">
                {{ page }}
              </button>
            </li>
            <li v-else-if="shouldShowEllipsis(page)" class="page-item disabled">
              <span class="page-link bg-dark text-light border-secondary">...</span>
            </li>
          </template>
          <li class="page-item" :class="{ disabled: !store.hasNextPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.nextPage" :disabled="!store.hasNextPage">
              <i class="bi bi-chevron-right"></i>
            </button>
          </li>
          <li class="page-item" :class="{ disabled: !store.hasNextPage }">
            <button class="page-link bg-dark text-light border-secondary" @click="store.fetchCharacters(store.totalPages)" :disabled="!store.hasNextPage">
              <i class="bi bi-chevron-double-right"></i>
            </button>
          </li>
        </ul>
      </nav>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const searchTimeout = ref<number | null>(null)

const handleSearch = async (event: Event) => {
  const target = event.target as HTMLInputElement
  if (searchTimeout.value) {
    clearTimeout(searchTimeout.value)
  }
  searchTimeout.value = window.setTimeout(async () => {
    await store.searchCharacters(target.value)
  }, 300)
}

const clearSearch = async () => {
  store.searchQuery = ''
  await store.fetchCharacters(1)
}

const handlePageSizeChange = async () => {
  await store.fetchCharacters(1)
}

const shouldShowPage = (page: number) => {
  const currentPage = store.currentPage
  const totalPages = store.totalPages
  const maxVisiblePages = 5

  if (totalPages <= maxVisiblePages) return true
  if (page === 1 || page === totalPages) return true
  if (page >= currentPage - 1 && page <= currentPage + 1) return true
  return false
}

const shouldShowEllipsis = (page: number) => {
  const currentPage = store.currentPage
  const totalPages = store.totalPages
  const maxVisiblePages = 5

  if (totalPages <= maxVisiblePages) return false
  if (page === 1 || page === totalPages) return false
  if (page === currentPage - 2 || page === currentPage + 2) return true
  return false
}

onMounted(async () => {
  await store.fetchCharacters()
})

// Add watchers for pagination
watch(() => store.currentPage, async (newPage) => {
  await store.fetchCharacters(newPage)
})
</script>

<style scoped>
.card {
  transition: all 0.3s ease;
}

.card:hover {
  box-shadow: 0 0 15px rgba(13, 110, 253, 0.3);
}

.character-image-container {
  width: 100%;
  aspect-ratio: 16/9;
  background-color: #2b3035;
  display: flex;
  align-items: flex-start;
  justify-content: center;
  position: relative;
  overflow: hidden;
}

.character-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
  object-position: top;
  transition: all 0.3s ease;
}

.card:hover .character-image {
  transform: scale(1.05);
  filter: brightness(1.1);
}

.page-link {
  transition: all 0.2s;
  min-width: 40px;
  text-align: center;
  padding: 0.5rem 0.75rem;
}

.page-link:hover:not(:disabled) {
  background-color: #2b3035 !important;
  border-color: #6c757d !important;
  transform: translateY(-1px);
}

.page-link:disabled {
  cursor: not-allowed;
  opacity: 0.5;
}

.page-item.active .page-link {
  background-color: #0d6efd !important;
  border-color: #0d6efd !important;
  box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
}

.page-item.disabled .page-link {
  background-color: #212529 !important;
  border-color: #6c757d !important;
  opacity: 0.5;
}

.pagination {
  margin-bottom: 0;
  gap: 0.25rem;
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

/* Image loading placeholder */
img {
  background-color: #2b3035;
}

.no-image-text {
  color: #6c757d;
  text-align: center;
  padding: 1rem;
}

.image-loading {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
}
</style>
