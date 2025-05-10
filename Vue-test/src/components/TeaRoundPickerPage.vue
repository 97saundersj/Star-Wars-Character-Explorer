<template>
  <div class="container">
    <h1 class="mb-4">Tea Round Picker</h1>

    <TeamSelector
      @team-select="handleTeamSelect"
    />

    <ParticipantsList
      :team-id="selectedTeamId"
      :participants="participants"
      @update:participants="setParticipants"
      @participant-added="refetchTeam"
    />

    <TeaWheel
      :team-id="selectedTeamId"
      :participants="participants"
      @tea-maker-picked="pickedTeaMaker"
    />

    <TeaRoundsTable
      :team-id="selectedTeamId"
      :refresh="refetchPreviousSelections"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { Team, Participant } from '../types/Types'
import { api } from '../services/api'
import { useToast } from 'vue-toastification'

const toast = useToast()

const participants = ref<Participant[]>([])
const selectedTeamId = ref<number | null>(null)
const refetchPreviousSelections = ref<boolean>(false)

const fetchTeamById = async (teamId: number): Promise<void> => {
  try {
    const response = await api.getTeamById(teamId)
    participants.value = response.participants || []
    if (response.id) {
      selectedTeamId.value = response.id
    }
  } catch (error) {
    console.error('Error fetching team:', error)
    toast.error('Failed to fetch team details. Please try again.')
  }
}

const handleTeamSelect = (team: Team | null): void => {
  if (team?.id) {
    fetchTeamById(team.id)
  } else {
    participants.value = []
    selectedTeamId.value = null
  }
}

const pickedTeaMaker = (): void => {
  refetchPreviousSelections.value = !refetchPreviousSelections.value
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
}

.mb-4 {
  margin-bottom: 1rem;
}
</style> 