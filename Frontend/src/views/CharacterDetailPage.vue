<template>
  <div v-if="character">
    <v-btn variant="outlined" color="primary" class="mb-4" :to="{ name: 'star-wars' }">
      <v-icon start icon="mdi-arrow-left" class="me-2"></v-icon>
      {{ languageStore.getLocalizedText('backToCharacters') }}
    </v-btn>

    <CharacterInfo
      :name="character.name"
      :description="character.description"
      :image="character.image"
    />

    <ReviewForm @submit="submitReview" />
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { useToast } from 'vue-toastification'
import CharacterInfo from '@/components/CharacterInfo.vue'
import ReviewForm from '@/components/ReviewForm.vue'

const route = useRoute()
const store = useCharacterStore()
const languageStore = useLanguageStore()
const toast = useToast()

const character = computed(() => store.getCharacterByName(route.params.name as string))

onMounted(async () => {
  if (!character.value) {
    await store.searchCharacters(route.params.name as string)
  }
})

const submitReview = async (review: {
  reviewerName: string
  watchDate: string
  reviewDetails: string
  rating: number
}) => {
  try {
    await store.submitReview({
      id: character.value?._id || '',
      characterName: route.params.name as string,
      ...review,
      createdAt: new Date().toISOString(),
    })
    toast.success(languageStore.getLocalizedText('successReview'))
  } catch {
    toast.error(languageStore.getLocalizedText('errorCreatingReview'))
  }
}
</script>
