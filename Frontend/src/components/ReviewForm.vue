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
              size="small"
              class="px-2"
              @click="form.rating = rating"
            >
              {{ rating }}
            </v-btn>
          </div>
        </div>

        <v-btn type="submit" color="primary">
          {{ languageStore.t('submitReview') }}
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
