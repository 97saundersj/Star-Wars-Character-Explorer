<template>
  <BContainer class="py-4">
    <div class="mb-5">
      <h1 class="text-light display-4 fw-bold mb-3">{{ languageStore.t('appTitle') }}</h1>
    </div>

    <BFormGroup class="mb-5">
      <BInputGroup size="lg">
        <BInputGroupText class="bg-dark text-light border-secondary">
          <i class="bi bi-search"></i>
        </BInputGroupText>
        <BFormInput
          type="text"
          class="bg-dark text-light border-secondary"
          :placeholder="languageStore.t('searchCharacters')"
          v-model="store.searchQuery"
          @input="handleSearch"
        />
        <BButton
          v-if="store.searchQuery"
          variant="outline-secondary"
          class="text-light border-secondary"
          @click="clearSearch"
          :title="languageStore.t('clearSearch')"
        >
          <i class="bi bi-x-lg"></i>
        </BButton>
      </BInputGroup>
    </BFormGroup>

    <CharacterPagination />

    <div v-if="store.loading" class="d-flex justify-content-center align-items-center my-5">
      <BSpinner class="text-light" style="width: 3rem; height: 3rem;" />
    </div>

    <BAlert v-else-if="store.error" variant="danger" show class="mb-4">
      <i class="bi bi-exclamation-triangle-fill me-2"></i>
      {{ languageStore.t('error') }}
    </BAlert>

    <div v-else>
      <BAlert v-if="store.characters.length === 0" variant="dark" show class="mb-4">
        <i class="bi bi-info-circle-fill me-2"></i>
        {{ languageStore.t('noResults') }}
      </BAlert>
      <BRow v-else class="g-4">
        <BCol v-for="character in store.characters" :key="character.name" cols="6" md="4" lg="3">
          <RouterLink
            :to="{ name: 'character-detail', params: { name: character.name }}"
            class="text-decoration-none"
          >
            <BCard
              class="bg-dark text-light border-secondary h-100"
            >
              <template #header>
                <div class="d-flex justify-content-between align-items-center">
                  <h6 class="card-title mb-0 text-truncate fw-medium">{{ character.name }}</h6>
                  <BButton
                    variant="link"
                    class="text-danger p-0"
                    @click.prevent="store.toggleLike(character.name)"
                  >
                    <i :class="character.isLiked ? 'bi bi-heart-fill' : 'bi bi-heart'" class="fs-5"></i>
                  </BButton>
                </div>
              </template>
              <div class="image-container position-relative">
                <div v-if="!imageLoaded[character.name]" class="loading-overlay">
                  <BSpinner class="text-light" />
                </div>
                <img
                  :src="character.image"
                  :alt="character.name"
                  class="card-img"
                  loading="lazy"
                  @load="imageLoaded[character.name] = true"
                  @error="imageLoaded[character.name] = true"
                >
              </div>
            </BCard>
          </RouterLink>
        </BCol>
      </BRow>
    </div>

    <CharacterPagination :is-bottom="true" />
  </BContainer>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import CharacterPagination from '@/components/CharacterPagination.vue'
import {
  BContainer,
  BFormGroup,
  BInputGroup,
  BInputGroupText,
  BFormInput,
  BButton,
  BRow,
  BCol,
  BSpinner,
  BAlert,
  BCard
} from 'bootstrap-vue-next'

const store = useCharacterStore()
const languageStore = useLanguageStore()
const searchTimeout = ref<number | null>(null)
const imageLoaded = ref<Record<string, boolean>>({})

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

onMounted(async () => {
  await store.fetchCharacters()
})
</script>

<style scoped>
:deep(.card) {
  display: flex;
  flex-direction: column;
  transition: transform 0.3s ease;
}

:deep(.card-header) {
  background-color: transparent !important;
  border-bottom: 1px solid #6c757d !important;
  padding: 0.75rem;
}

:deep(.card-header .btn-link) {
  border: none !important;
  outline: none !important;
  box-shadow: none !important;
}

:deep(.card-header .btn-link:hover) {
  border: none !important;
  outline: none !important;
  box-shadow: none !important;
}

:deep(.card:hover) {
  transform: translateY(-2px);
}

:deep(.card-body) {
  padding: 0;
  flex: 1;
  display: flex;
  overflow: hidden;
}

:deep(.card-img) {
  width: 100%;
  aspect-ratio: 16/9;
  object-fit: cover;
  object-position: top;
  transition: transform 0.3s ease;
  margin: 0;
  padding: 0;
}

:deep(.card:hover .card-img) {
  transform: scale(1.05);
}

:deep(a) {
  outline: none !important;
  text-decoration: none !important;
  padding: 0 !important;
}

:deep(a:focus) {
  outline: none !important;
}

:deep(a:focus-visible) {
  outline: none !important;
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

/* Custom pagination styling */
:deep(.page-link) {
  background-color: #212529;
  border-color: #6c757d;
  color: #fff;
  transition: all 0.2s ease;
}

:deep(.page-link:hover) {
  background-color: #2b3035;
  border-color: #0d6efd;
  transform: translateY(-1px);
}

:deep(.page-item.active .page-link) {
  background-color: #0d6efd;
  border-color: #0d6efd;
}

:deep(.page-item.disabled .page-link) {
  background-color: #212529;
  border-color: #6c757d;
  opacity: 0.5;
}

.image-container {
  width: 100%;
  aspect-ratio: 16/9;
  background-color: #2b3035;
  overflow: hidden;
}

.loading-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 1;
}
</style>
