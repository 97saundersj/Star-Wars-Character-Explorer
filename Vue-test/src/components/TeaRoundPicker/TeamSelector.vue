<template>
  <div class="team-selector card m-2 bg-dark text-light" data-testid="team-selector">
    <div class="card-body">
      <h5 class="card-title">Team</h5>
      <Multiselect
        v-model="selectedTeam"
        :options="teams"
        option-label="label"
        track-by="value"
        :searchable="true"
        placeholder="Select a team..."
        data-testid="team-selector-input"
        @search-change="handleSearch"
        @open="fetchTeams"
        @change="handleTeamChange"
        class="form-control bg-dark text-light"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { api } from '@/services/api'
import { useToast } from 'vue-toastification'
import Multiselect from '@vueform/multiselect'
import '@vueform/multiselect/themes/default.css'
import type { Team, Participant } from '../../types/Types'

const toast = useToast()
const teams = ref<Team[]>([])
const selectedTeam = ref<{ name: string; value: number } | null>(null)

const fetchTeams = async () => {
  try {
    const response = await api.getTeams()
    teams.value = response.map(team => ({
      label: team.label,
      value: team.id ?? 0
    }))
  } catch (error) {
    toast.error('Failed to fetch teams. Please try again.')
  }
}

const handleCreate = async (newValue: string) => {
  if (!newValue.trim()) {
    toast.error('Team name cannot be empty.')
    return
  }

  if (teams.value.some(t => t.label.toLowerCase() === newValue.toLowerCase())) {
    toast.error('This team already exists.')
    return
  }

  try {
    const created = await api.createTeam({ label: newValue, participants: [] })
    await fetchTeams()
    const createdTeam = { name: created.label, value: created.id ?? 0 }
    teams.value.push(created)
    selectedTeam.value = createdTeam
  } catch (error) {
    toast.error('Error creating team. Please try again.')
  }
}

const handleSearch = (search: string) => {
  if (search && !teams.value.some(t => t.label.toLowerCase() === search.toLowerCase())) {
    handleCreate(search)
  }
}

const emit = defineEmits<{
  (e: 'team-select', team: { id: number; label: string } | null): void
}>()

const handleTeamChange = (teamId: number | null) => {
  emit('team-select', teamId ? { id: teamId, label: "" } : null)
}

onMounted(fetchTeams)
</script>

<style scoped>
/* Custom styles for dark theme using :deep */
:deep(.multiselect) {
  background-color: #343a40; /* Dark background */
  color: #ffffff; /* Light text */
}

:deep(.multiselect-dropdown) {
  background-color: #343a40; /* Dark dropdown background */
  color: #ffffff; /* Light dropdown text */
}

:deep(.multiselect-option) {
  color: #ffffff; /* Light text for options */
}

:deep(.multiselect-search) {
  background-color: #495057; /* Darker background for search input */
  color: #ffffff; /* Light text for search input */
}
</style>
