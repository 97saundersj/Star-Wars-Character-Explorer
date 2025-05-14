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
          <div class="col-md-6">
            <p><strong>{{ languageStore.t('birthYear') }}:</strong> {{ character.birth_year }}</p>
            <p><strong>{{ languageStore.t('gender') }}:</strong> {{ character.gender }}</p>
            <p><strong>{{ languageStore.t('height') }}:</strong> {{ character.height }} cm</p>
            <p><strong>{{ languageStore.t('mass') }}:</strong> {{ character.mass }} kg</p>
            <p><strong>{{ languageStore.t('hairColor') }}:</strong> {{ character.hair_color }}</p>
            <p><strong>{{ languageStore.t('skinColor') }}:</strong> {{ character.skin_color }}</p>
            <p><strong>{{ languageStore.t('eyeColor') }}:</strong> {{ character.eye_color }}</p>
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
            <label for="reviewDetail" class="form-label">{{ languageStore.t('reviewDetails') }}</label>
            <textarea
              class="form-control bg-dark text-light border-secondary"
              id="reviewDetail"
              v-model="reviewForm.reviewDetail"
              rows="4"
              required
            ></textarea>
          </div>

          <div class="mb-3">
            <label for="rating" class="form-label">{{ languageStore.t('rating') }}</label>
            <select
              class="form-select bg-dark text-light border-secondary"
              id="rating"
              v-model="reviewForm.rating"
              required
            >
              <option v-for="n in 10" :key="n" :value="n">{{ n }}</option>
            </select>
          </div>

          <button
            type="submit"
            class="btn btn-primary"
            :disabled="submitting"
          >
            <span v-if="submitting" class="spinner-border spinner-border-sm me-2" role="status"></span>
            {{ languageStore.t('submitReview') }}
          </button>
        </form>
      </div>
    </div>

    <!-- Existing Reviews -->
    <div v-if="characterReviews.length > 0" class="card bg-dark text-light border-secondary">
      <div class="card-header border-secondary">
        <h3 class="card-title mb-0">{{ languageStore.t('reviews') }}</h3>
      </div>
      <div class="card-body">
        <div class="list-group list-group-flush">
          <div
            v-for="review in characterReviews"
            :key="review.id"
            class="list-group-item bg-dark text-light border-secondary"
          >
            <div class="d-flex justify-content-between align-items-center mb-2">
              <h5 class="mb-0">
                {{ languageStore.t('rating') }}: {{ review.rating }}/10
                <div class="d-inline-block ms-2">
                  <i
                    v-for="n in review.rating"
                    :key="n"
                    class="bi bi-star-fill text-warning"
                  ></i>
                </div>
              </h5>
              <small class="text-muted">
                {{ languageStore.t('watchedOn') }}: {{ new Date(review.watchDate).toLocaleDateString() }}
              </small>
            </div>
            <p class="mb-1">{{ review.reviewDetail }}</p>
            <small class="text-muted">
              {{ languageStore.t('reviewedBy') }} {{ review.reviewerName }} {{ languageStore.t('on') }} {{ new Date(review.createdAt).toLocaleDateString() }}
            </small>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const route = useRoute()
const store = useCharacterStore()
const languageStore = useLanguageStore()

const submitting = ref(false)

const reviewForm = ref({
  reviewerName: '',
  watchDate: '',
  reviewDetail: '',
  rating: 5
})

const character = computed(() => store.getCharacterByName(route.params.name as string))
const characterReviews = computed(() => store.getCharacterReviews(route.params.name as string))

async function submitReview() {
  submitting.value = true
  try {
    await store.submitReview({
      characterName: route.params.name as string,
      ...reviewForm.value
    })
    // Reset form
    reviewForm.value = {
      reviewerName: '',
      watchDate: '',
      reviewDetail: '',
      rating: 5
    }
  } catch (error) {
    // Error is expected as per requirements
    console.log('Review saved locally:', error)
  } finally {
    submitting.value = false
  }
}

onMounted(() => {
  if (!store.characters.length) {
    store.fetchCharacters()
  }
})
</script>

<style scoped>
.form-control:focus,
.form-select:focus {
  background-color: #2b3035;
  color: #fff;
  border-color: #6c757d;
  box-shadow: 0 0 0 0.25rem rgba(108, 117, 125, 0.25);
}

.list-group-item {
  transition: background-color 0.2s;
}

.list-group-item:hover {
  background-color: #2b3035 !important;
}
</style>
