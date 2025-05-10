<template>
  <div class="team-selector card m-2" data-testid="team-selector">
    <div class="card-body">
      <h5 class="card-title">Team</h5>
      <Multiselect
        v-model="selectedTeam"
        :options="teams"
        option-label="name"
        track-by="value"
        :searchable="true"
        :multiple="false"
        :close-on-select="true"
        :clear-on-select="false"
        :preserve-search="true"
        placeholder="Select a team..."
        data-testid="team-selector-input"
        @search-change="handleSearch"
        @open="fetchTeams"
        @change="handleTeamChange"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import type { Team } from '@/types/Types'
import { api } from '@/services/api'
import { useToast } from 'vue-toastification'
import Multiselect from '@vueform/multiselect'
import '@vueform/multiselect/themes/default.css'

interface TeamOption {
  name: string;
  value: number;
}

const toast = useToast()

const emit = defineEmits<{
  (e: 'team-select', team: Team | null): void
}>()

const teams = ref<TeamOption[]>([])
const selectedTeam = ref<TeamOption | null>(null)

const fetchTeams = async (): Promise<void> => {
  try {
    const response = await api.getTeams()
    console.log('Fetched teams:', JSON.stringify(response, null, 2))
    teams.value = response.map(team => {
      const mapped = {
        name: team.label,
        value: team.id ?? 0,
        label: team.label,
        id: team.id,
        participants: team.participants
      }
      console.log('Mapped team:', JSON.stringify(mapped, null, 2))
      return mapped
    })
  } catch (error) {
    console.error('Error fetching teams:', error)
    toast.error('Failed to fetch teams. Please try again.')
  }
}

const handleCreate = async (newValue: string) => {
  if (!newValue.trim()) {
    toast.error('Team name cannot be empty.')
    return
  }

  if (teams.value.some(t => t.name.toLowerCase() === newValue.toLowerCase())) {
    toast.error('This team already exists.')
    return
  }

  const newTeam: Omit<Team, 'id'> = { label: newValue, participants: [] }
  try {
    console.log('Creating team:', newTeam)
    const created = await api.createTeam(newTeam as Team)
    console.log('Created team response:', created)
    if (!created.id) throw new Error('No ID on created team')

    await fetchTeams()
    const justCreated = teams.value.find(t => t.value === created.id)
    console.log('Found created team in list:', justCreated)
    selectedTeam.value = justCreated ?? { name: created.label, value: created.id, ...created }
  } catch (error) {
    console.error('Error creating team:', error)
    toast.error('Error creating team. Please try again.')
  }
}

const handleSearch = (search: string) => {
  if (
    search &&
    !teams.value.some(t => t.name.toLowerCase() === search.toLowerCase())
  ) {
    handleCreate(search)
  }
}

const handleTeamChange = (teamId: number) => {
  console.log('Selected team:', JSON.stringify(teamId, null, 2))
  if (teamId) {
    const originalTeam: Team = {
      id: teamId,
      label: "",
      //participants: team.participants
    }
    console.log('Emitting team:', JSON.stringify(originalTeam, null, 2))
    emit('team-select', originalTeam)
  } else {
    console.log('Emitting null team')
    emit('team-select', null)
  }
}

onMounted(fetchTeams)
</script>

<style scoped>
.team-selector {
  margin-bottom: 1rem;
}
.card {
  border: 1px solid #ddd;
  border-radius: 4px;
}
.card-body {
  padding: 1rem;
}
.card-title {
  margin-bottom: 1rem;
  font-size: 1.1rem;
  font-weight: 500;
}
:deep(.multiselect) {
  min-height: 40px;
}
:deep(.multiselect-dropdown) {
  border: 1px solid #ced4da;
  border-radius: 4px;
}
:deep(.multiselect-option) {
  padding: 0.5rem;
}
:deep(.multiselect-search) {
  padding: 0.5rem;
}
:deep(.multiselect-single-label) {
  padding: 0.5rem;
}
</style>
