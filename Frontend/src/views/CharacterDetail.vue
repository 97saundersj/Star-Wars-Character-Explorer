<template>
  <div v-if="character">
    <BButton
      variant="outline-light"
      class="mb-4"
      :to="{ name: 'star-wars' }"
    >
      <i class="bi bi-arrow-left me-2"></i>
      {{ languageStore.t('backToCharacters') }}
    </BButton>

    <BCard
      bg-variant="dark"
      text-variant="light"
      border-variant="secondary"
      class="mb-4"
    >
      <template #header>
        <h2 class="card-title mb-0">{{ character.name }}</h2>
      </template>
      <BRow>
        <BCol md="4" class="mb-3 mb-md-0">
          <div class="character-image-container">
            <div v-if="!imageLoaded" class="loading-overlay">
              <BSpinner class="text-light" style="width: 3rem; height: 3rem;" />
            </div>
            <BImg
              :src="character.image"
              :alt="character.name"
              fluid
              class="character-image rounded"
              @load="imageLoaded = true"
              @error="imageLoaded = true"
            />
          </div>
        </BCol>
        <BCol md="8">
          <p class="mb-4">{{ character.description }}</p>
        </BCol>
      </BRow>
    </BCard>

    <!-- Review Form -->
    <BCard
      bg-variant="dark"
      text-variant="light"
      border-variant="secondary"
      class="mb-4"
    >
      <template #header>
        <h3 class="card-title mb-0">{{ languageStore.t('writeReview') }}</h3>
      </template>
      <BForm @submit.prevent="submitReview">
        <BFormGroup
          :label="languageStore.t('yourName')"
          label-for="reviewerName"
        >
          <BFormInput
            id="reviewerName"
            v-model="reviewForm.reviewerName"
            required
            bg-variant="dark"
            text-variant="light"
            border-variant="secondary"
          />
        </BFormGroup>
        <BFormGroup
          :label="languageStore.t('dateWatched')"
          label-for="watchDate"
        >
          <BFormInput
            id="watchDate"
            type="date"
            v-model="reviewForm.watchDate"
            required
            bg-variant="dark"
            text-variant="light"
            border-variant="secondary"
          />
        </BFormGroup>
        <BFormGroup
          :label="languageStore.t('reviewDetails')"
          label-for="reviewDetails"
        >
          <BFormTextarea
            id="reviewDetails"
            v-model="reviewForm.reviewDetails"
            rows="3"
            required
            bg-variant="dark"
            text-variant="light"
            border-variant="secondary"
          />
        </BFormGroup>
        <BFormGroup :label="languageStore.t('rating')" class="mb-4">
          <div class="rating-buttons">
            <BButton
              v-for="rating in 10"
              :key="rating"
              :variant="reviewForm.rating === rating ? 'primary' : 'outline-primary'"
              @click="reviewForm.rating = rating"
              size="sm"
              class="rating-button"
            >
              {{ rating }}
            </BButton>
          </div>
        </BFormGroup>
        <BButton type="submit" variant="primary">
          {{ languageStore.t('submitReview') }}
        </BButton>
      </BForm>
    </BCard>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { useToast, POSITION } from 'vue-toastification'
import {
  BButton,
  BCard,
  BRow,
  BCol,
  BImg,
  BForm,
  BFormGroup,
  BFormInput,
  BFormTextarea,
  BSpinner
} from 'bootstrap-vue-next'

const route = useRoute()
const store = useCharacterStore()
const languageStore = useLanguageStore()
const toast = useToast()
const imageLoaded = ref(false)

const character = computed(() => store.getCharacterByName(route.params.name as string))

onMounted(async () => {
  if (!character.value) {
    await store.searchCharacters(route.params.name as string)
  }
})

const reviewForm = ref({
  reviewerName: '',
  watchDate: '',
  reviewDetails: '',
  rating: 0
})

const submitReview = async () => {
  try {
    await store.submitReview({
      characterName: route.params.name as string,
      reviewerName: reviewForm.value.reviewerName,
      watchDate: reviewForm.value.watchDate,
      reviewDetails: reviewForm.value.reviewDetails,
      rating: reviewForm.value.rating
    })
    // Reset form
    reviewForm.value = {
      reviewerName: '',
      watchDate: '',
      reviewDetails: '',
      rating: 0
    }
    toast.success("May the Force be review!")
  } catch {
    toast.error("Error creating review.\nI have a bad feeling about this...")
  }
}
</script>

<style scoped>
.btn-outline-primary {
  color: #0d6efd;
  border-color: #0d6efd;
}

.btn-outline-primary:hover {
  color: #fff;
  background-color: #0d6efd;
  border-color: #0d6efd;
}

.character-image-container {
  width: 100%;
  background-color: #2b3035;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  overflow: hidden;
  border-radius: 0.375rem;
}

.character-image {
  width: 100%;
  height: auto;
  max-height: 600px;
  object-fit: contain;
  transition: all 0.3s ease;
}

.character-image:hover {
  transform: scale(1.05);
  filter: brightness(1.1);
}

/* Form control styling */
:deep(.form-control) {
  background-color: #2b3035 !important;
  border-color: #6c757d !important;
  color: #fff !important;
}

:deep(.form-control:focus) {
  background-color: #2b3035 !important;
  border-color: #0d6efd !important;
  color: #fff !important;
  box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25) !important;
}

:deep(.form-control::placeholder) {
  color: #6c757d !important;
}

:deep(.form-label) {
  color: #fff !important;
}

:deep(.form-control:disabled) {
  background-color: #1a1d20 !important;
  color: #6c757d !important;
}

:deep(.form-control[readonly]) {
  background-color: #2b3035 !important;
  color: #fff !important;
}

:deep(.form-select) {
  background-color: #2b3035 !important;
  border-color: #6c757d !important;
  color: #fff !important;
}

:deep(.form-select:focus) {
  background-color: #2b3035 !important;
  border-color: #0d6efd !important;
  color: #fff !important;
  box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25) !important;
}

:deep(.form-select option) {
  background-color: #2b3035 !important;
  color: #fff !important;
}

:deep(.form-select:disabled) {
  background-color: #1a1d20 !important;
  color: #6c757d !important;
}

:deep(.form-select[readonly]) {
  background-color: #2b3035 !important;
  color: #fff !important;
}

.rating-buttons {
  display: flex;
  flex-wrap: wrap;
  gap: 0.25rem;
}

.rating-button {
  min-width: 2.5rem;
  padding: 0.25rem 0.5rem;
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
  border-radius: 0.375rem;
}
</style>

