<template>
  <div class="team-selector">
    <select
      v-model="selectedTeam"
      class="form-select"
      @change="handleChange"
    >
      <option :value="null">Select a team</option>
      <option
        v-for="team in teams"
        :key="team.id"
        :value="team"
      >
        {{ team.name }}
      </option>
    </select>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { Team } from '../types/Types'
import { api } from '../services/api'

const emit = defineEmits<{
  (e: 'team-select', team: Team | null): void
}>()

const teams = ref<Team[]>([])
const selectedTeam = ref<Team | null>(null)

// TODO: Implement actual API call to fetch teams
const fetchTeams = async (): Promise<void> => {
  // This is a placeholder. Replace with actual API call
  teams.value = [
    { id: 1, name: 'Team 1' },
    { id: 2, name: 'Team 2' }
  ]
}

const handleChange = (): void => {
  emit('team-select', selectedTeam.value)
}

// Fetch teams when component is mounted
fetchTeams()
</script>

<style scoped>
.team-selector {
  margin-bottom: 1rem;
}

.form-select {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}
</style> 