<template>
  <v-container class="py-4">
    <div class="mb-5">
      <h1 class="text-h3 font-weight-bold mb-3">{{ languageStore.getLocalizedText('about') }}</h1>
    </div>

    <div class="text-center">
      <v-btn
        href="https://github.com/97saundersj/Star-Wars-Character-Explorer"
        target="_blank"
        rel="noopener noreferrer"
        variant="outlined"
        color="primary"
        size="large"
      >
        <v-icon start icon="mdi-github" class="me-2"></v-icon>
        {{ languageStore.getLocalizedText('checkGithub') }}
      </v-btn>
    </div>
  </v-container>
</template>

<script setup lang="ts">
import { useLanguageStore } from '@/stores/languageStore'

const languageStore = useLanguageStore()
</script>
