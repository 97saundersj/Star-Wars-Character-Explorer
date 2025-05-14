<template>
  <div class="container" :class="{ 'bg-dark text-light': isDarkMode }">
    <h1 class="mb-4">Tea Round Picker</h1>

    <TeamSelector
      @team-select="handleTeamSelect"
    />

    <div v-if="selectedTeamId" class="components-container">
      <ParticipantsList
        :team-id="selectedTeamId"
        :participants="participants"
        :set-participants="setParticipants"
        @participant-added="refetchTeam"
      />

      <TeaWheel
        v-if="participants.length > 0"
        :team-id="selectedTeamId"
        :participants="participants"
        :is-dark-mode="isDarkMode"
        @tea-maker-picked="handleTeaMakerPicked"
      />

      <TeaRoundsTable
        v-if="selectedTeamId"
        ref="teaRoundsTable"
        :team-id="selectedTeamId"
      />
    </div>

    <div v-else class="text-center mt-5">
      <p class="text-muted">Please select a team to get started</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { Team, Participant } from '../types/Types'
import { api } from '../services/api'
import { useToast } from 'vue-toastification'
import TeamSelector from './TeaRoundPicker/TeamSelector.vue'
import ParticipantsList from './TeaRoundPicker/ParticipantsList.vue'
import TeaWheel from './TeaRoundPicker/TeaWheel.vue'
import TeaRoundsTable from './TeaRoundPicker/TeaRoundsTable.vue'

const toast = useToast()
const teaRoundsTable = ref<InstanceType<typeof TeaRoundsTable> | null>(null)
const isDarkMode = ref(true) // Set dark mode as default

const participants = ref<Participant[]>([])
const selectedTeamId = ref<number | null>(null)

onMounted(() => {
  console.log('TeaRoundPickerPage mounted, teaRoundsTable ref will be available after render')
})

const fetchTeamById = async (teamId: number): Promise<void> => {
  try {
    console.log('Fetching team details for ID:', teamId)
    const response = await api.getTeamById(teamId)
    console.log('Team details response:', response)
    participants.value = response.participants || []
    if (response.id) {
      console.log('Setting selectedTeamId to:', response.id)
      selectedTeamId.value = response.id
    }
  } catch (error) {
    console.error('Error fetching team:', error)
    toast.error('Failed to fetch team details. Please try again.')
  }
}

const handleTeamSelect = (team: Team | null): void => {
  console.log('Team selected:', team)
  if (team?.id) {
    console.log('Fetching team with ID:', team.id)
    fetchTeamById(team.id)
  } else {
    console.log('No team selected, clearing state')
    participants.value = []
    selectedTeamId.value = null
  }
}

const handleTeaMakerPicked = (): void => {
  console.log('Tea maker picked event received, attempting to refresh table')
  if (teaRoundsTable.value) {
    console.log('TeaRoundsTable reference exists, calling fetchTeaRounds()')
    teaRoundsTable.value.fetchTeaRounds()
  } else {
    console.error('TeaRoundsTable reference is null, cannot refresh')
  }
}

const refetchTeam = (): void => {
  if (selectedTeamId.value) {
    fetchTeamById(selectedTeamId.value)
  }
}

const setParticipants = (newParticipants: Participant[]): void => {
  participants.value = newParticipants
}
</script>

<style scoped>
.container {
  padding: 1rem;
  min-height: 100vh;
  transition: all 0.3s ease;
}

.mb-4 {
  margin-bottom: 1rem;
}

.bg-dark {
  background-color: #212529;
}

.components-container {
  opacity: 1;
  transition: opacity 0.3s ease;
}

.text-muted {
  opacity: 0.7;
}
</style> 