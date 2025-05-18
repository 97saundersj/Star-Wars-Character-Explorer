<template>
  <BCard bg-variant="dark" border-variant="secondary" class="mb-4">
    <template #header>
      <h3 class="card-title mb-0">{{ languageStore.t('writeReview') }}</h3>
    </template>
    <BForm @submit.prevent="handleSubmit">
      <BFormGroup :label="languageStore.t('yourName')" label-for="reviewerName" class="mb-3">
        <BFormInput
          id="reviewerName"
          v-model="form.reviewerName"
          required
          bg-variant="dark"
          border-variant="secondary"
          class="text-primary text-bg-dark"
        />
      </BFormGroup>

      <BFormGroup :label="languageStore.t('dateWatched')" label-for="watchDate" class="mb-3">
        <BFormInput
          id="watchDate"
          type="date"
          v-model="form.watchDate"
          required
          bg-variant="dark"
          border-variant="secondary"
          class="text-primary text-bg-dark"
        />
      </BFormGroup>

      <BFormGroup :label="languageStore.t('reviewDetails')" label-for="reviewDetails" class="mb-3">
        <BFormTextarea
          id="reviewDetails"
          v-model="form.reviewDetails"
          rows="3"
          required
          bg-variant="dark"
          border-variant="secondary"
          class="text-primary text-bg-dark"
        />
      </BFormGroup>

      <BFormGroup :label="languageStore.t('rating')" class="mb-4">
        <div class="d-flex flex-wrap gap-2">
          <BButton
            v-for="rating in 10"
            :key="rating"
            :variant="form.rating === rating ? 'primary' : 'outline-primary'"
            @click="form.rating = rating"
            size="sm"
            class="px-2"
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
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useLanguageStore } from '@/stores/languageStore'
import { BCard, BForm, BFormGroup, BFormInput, BFormTextarea, BButton } from 'bootstrap-vue-next'

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
