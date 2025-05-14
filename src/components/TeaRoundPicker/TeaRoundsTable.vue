<template>
  <div v-if="teamId" class="card bg-dark text-light m-2">
    <div class="card-body">
      <h5 class="card-title mb-4">Previous Tea Rounds</h5>
      <div v-if="error" class="alert alert-danger">
        {{ error }}
      </div>
      <div v-else-if="!teaRounds.length" class="text-center text-muted">
        No tea rounds yet
      </div>
      <div v-else class="accordion" id="teaRoundAccordion">
        <div
          v-for="(round, index) in teaRounds"
          :key="round.id"
          class="accordion-item bg-dark text-light"
        >
          <h2 class="accordion-header" :id="`heading${index}`">
            <button
              class="accordion-button collapsed bg-dark text-light"
              type="button"
              data-bs-toggle="collapse"
              :data-bs-target="`#collapse${index}`"
              aria-expanded="false"
              :aria-controls="`collapse${index}`"
            >
              <b>
                {{ formatDateTime(round.date) }} - {{ round.chosenParticipant.name }} made tea
              </b>
            </button>
          </h2>
          <div
            :id="`collapse${index}`"
            class="accordion-collapse collapse bg-dark"
            :aria-labelledby="`heading${index}`"
          >
            <div class="accordion-body">
              <table class="table table-dark table-hover">
                <thead>
                  <tr>
                    <th>Participant</th>
                    <th>Tea Order</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="order in round.teaOrders" :key="order.id">
                    <td>{{ order.participant.name }}</td>
                    <td>
                      <template v-if="order.requestedTeaOrder">
                        {{ order.requestedTeaOrder }}
                      </template>
                      <small v-else class="text-muted fst-italic">None Specified</small>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted, watchEffect } from 'vue'
import type { TeaRound } from '../../types/Types'
import { api } from '../../services/api'

const props = defineProps<{
  teamId: number | null
}>()

const teaRounds = ref<TeaRound[]>([])
const error = ref<string | null>(null)

const fetchTeaRounds = async (): Promise<void> => {
  if (!props.teamId) return
  
  try {
    console.log('Fetching tea rounds for team:', props.teamId)
    const response = await api.getTeaRounds(props.teamId)
    console.log('Received tea rounds:', response)
    teaRounds.value = response
    error.value = null
  } catch (err) {
    console.error('Error fetching tea rounds:', err)
    error.value = 'Error fetching previous selections.'
    teaRounds.value = []
  }
}

// Watch for teamId changes
watch(() => props.teamId, (newTeamId) => {
  console.log('Team ID changed to:', newTeamId)
  if (newTeamId) {
    fetchTeaRounds()
  } else {
    teaRounds.value = []
  }
}, { immediate: true })

// Initial fetch when component is mounted
onMounted(() => {
  console.log('TeaRoundsTable component mounted, teamId:', props.teamId)
  if (props.teamId) {
    fetchTeaRounds()
  }
})

const formatDateTime = (date: string): string => {
  return new Intl.DateTimeFormat('en-UK', {
    dateStyle: 'medium',
    timeStyle: 'short'
  }).format(new Date(date))
}

defineExpose({
  fetchTeaRounds
})
</script>

<style scoped>
.accordion-button::after {
  filter: invert(1);
}
</style> 