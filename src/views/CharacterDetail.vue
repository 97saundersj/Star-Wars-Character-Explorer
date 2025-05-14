<template>
  <div v-if="character">
    <RouterLink
      :to="{ name: 'star-wars' }"
      class="btn btn-outline-light mb-4"
    >
      <i class="bi bi-arrow-left me-2"></i>
      {{ languageStore.t('backToCharacters') }}
    </RouterLink>

    <div class="card bg-dark text-light border-secondary mb-4">
      <div class="card-header border-secondary">
        <h2 class="card-title mb-0">{{ character.name }}</h2>
      </div>
      <div class="card-body">
        <div class="row">
          <div class="col-md-4 mb-3 mb-md-0">
            <img :src="character.image" :alt="character.name" class="img-fluid rounded" style="max-height: 400px; object-fit: cover;">
          </div>
          <div class="col-md-8">
            <p class="mb-4">{{ character.description }}</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Review Form -->
    <div class="card bg-dark text-light border-secondary mb-4">
      <div class="card-header border-secondary">
        <h3 class="card-title mb-0">{{ languageStore.t('writeReview') }}</h3>
      </div>
      <div class="card-body">
        <form @submit.prevent="submitReview">
          <div class="mb-3">
            <label for="reviewerName" class="form-label">{{ languageStore.t('yourName') }}</label>
            <input
              type="text"
              class="form-control bg-dark text-light border-secondary"
              id="reviewerName"
              v-model="reviewForm.reviewerName"
              required
            >
          </div>
          <div class="mb-3">
            <label for="watchDate" class="form-label">{{ languageStore.t('dateWatched') }}</label>
            <input
              type="date"
              class="form-control bg-dark text-light border-secondary"
              id="watchDate"
              v-model="reviewForm.watchDate"
              required
            >
          </div>
          <div class="mb-3">
            <label for="reviewDetails" class="form-label">{{ languageStore.t('reviewDetails') }}</label>
            <textarea
              class="form-control bg-dark text-light border-secondary"
              id="reviewDetails"
              v-model="reviewForm.reviewDetails"
              rows="3"
              required
            ></textarea>
          </div>
          <div class="mb-3">
            <label class="form-label">{{ languageStore.t('rating') }}</label>
            <div class="d-flex gap-2">
              <button
                v-for="rating in 10"
                :key="rating"
                type="button"
                class="btn"
                :class="reviewForm.rating === rating ? 'btn-primary' : 'btn-outline-primary'"
                @click="reviewForm.rating = rating"
              >
                {{ rating }}
              </button>
            </div>
          </div>
          <button type="submit" class="btn btn-primary">{{ languageStore.t('submitReview') }}</button>
        </form>
      </div>
    </div>

    <!-- Reviews List -->
    <div class="card bg-dark text-light border-secondary">
      <div class="card-header border-secondary">
        <h3 class="card-title mb-0">{{ languageStore.t('reviews') }}</h3>
      </div>
      <div class="card-body">
        <div v-if="reviews.length === 0" class="text-muted">
          {{ languageStore.t('noReviews') }}
        </div>
        <div v-else class="list-group">
          <div v-for="review in reviews" :key="review.id" class="list-group-item bg-dark text-light border-secondary">
            <div class="d-flex justify-content-between align-items-center mb-2">
              <h5 class="mb-0">{{ review.reviewerName }}</h5>
              <span class="badge bg-primary">{{ review.rating }}/10</span>
            </div>
            <p class="mb-1">{{ review.reviewDetails }}</p>
            <small class="text-muted">
              {{ languageStore.t('watchedOn') }} {{ new Date(review.watchDate).toLocaleDateString() }}
              {{ languageStore.t('on') }} {{ new Date(review.createdAt).toLocaleDateString() }}
            </small>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const route = useRoute()
const store = useCharacterStore()
const languageStore = useLanguageStore()

const character = computed(() => store.getCharacterByName(route.params.name as string))
const reviews = computed(() => store.getCharacterReviews(route.params.name as string))

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
  } catch {
    // Error is expected as per requirements
    console.log('Review saved locally')
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

.list-group-item {
  transition: all 0.3s ease;
}

.list-group-item:hover {
  background-color: #2b3035 !important;
}
</style>

