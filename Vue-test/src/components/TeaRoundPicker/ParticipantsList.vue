<template>
  <div class="participants-list">
    <h2>Participants</h2>
    <ul class="list">
      <li
        v-for="participant in participants"
        :key="participant.id"
        class="list-item"
      >
        {{ participant.name }}
      </li>
    </ul>
    <div v-if="!participants.length" class="no-participants">
      No participants in this team
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Participant } from '../types/Types'

defineProps<{
  teamId: number | null
  participants: Participant[]
}>()

defineEmits<{
  (e: 'update:participants', participants: Participant[]): void
  (e: 'participant-added'): void
}>()
</script>

<style scoped>
.participants-list {
  margin-bottom: 1rem;
}

.list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.list-item {
  padding: 0.5rem;
  border-bottom: 1px solid #eee;
}

.no-participants {
  color: #666;
  font-style: italic;
  padding: 0.5rem;
}
</style> 