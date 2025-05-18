<template>
  <v-card class="mb-4" variant="outlined">
    <v-card-title class="text-h5">
      {{ languageStore.t('writeReview') }}
    </v-card-title>

    <v-card-text>
      <v-form @submit.prevent="handleSubmit">
        <v-text-field
          v-model="form.reviewerName"
          :label="languageStore.t('yourName')"
          required
          variant="outlined"
          class="mb-3"
        />

        <v-text-field
          v-model="form.watchDate"
          :label="languageStore.t('dateWatched')"
          type="date"
          required
          variant="outlined"
          class="mb-3"
        />

        <v-textarea
          v-model="form.reviewDetails"
          :label="languageStore.t('reviewDetails')"
          rows="3"
          required
          variant="outlined"
          class="mb-4"
        />

        <div class="mb-4">
          <div class="text-subtitle-1 mb-2">{{ languageStore.t('rating') }}</div>
          <div class="d-flex flex-wrap gap-2">
            <v-btn
              v-for="rating in 10"
              :key="rating"
              :color="form.rating === rating ? 'primary' : undefined"
              :variant="form.rating === rating ? 'flat' : 'outlined'"
              :class="{ 'selected-btn': form.rating === rating }"
              size="small"
              class="px-2"
              @click="form.rating = rating"
            >
              <span :class="{ 'white-text': form.rating === rating }">{{ rating }}</span>
            </v-btn>
          </div>
        </div>

        <v-btn type="submit" color="primary" class="submit-btn">
          <span class="white-text">{{ languageStore.t('submitReview') }}</span>
        </v-btn>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useLanguageStore } from '@/stores/languageStore'

const languageStore = useLanguageStore()

const form = ref({
  reviewerName: '',
  watchDate: '',
  reviewDetails: '',
  rating: 0,
})

const emit = defineEmits<{
  (
    e: 'submit',
    review: {
      reviewerName: string
      watchDate: string
      reviewDetails: string
      rating: number
    },
  ): void
}>()

const handleSubmit = () => {
  emit('submit', { ...form.value })
}
</script>

<style scoped>
.submit-btn, .selected-btn {
  box-shadow: inset 0px 0px 5px rgba(0, 0, 0, 0.5) !important;
  transform: scale(0.98) !important;
  opacity: 0.9 !important;
  background-color: #0098cc !important;
}

.white-text {
  color: white !important;
}

/* Target all elements inside submit-btn and selected-btn */
.submit-btn *, .selected-btn * {
  color: white !important;
}
</style>
