<template>
  <div class="tea-rounds-table">
    <h2>Previous Tea Rounds</h2>
    <table v-if="teaRounds.length" class="table">
      <thead>
        <tr>
          <th>Date</th>
          <th>Tea Maker</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="round in teaRounds"
          :key="round.id"
        >
          <td>{{ formatDate(round.date) }}</td>
          <td>{{ round.teaMaker }}</td>
        </tr>
      </tbody>
    </table>
    <div v-else class="no-rounds">
      No previous tea rounds recorded
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { Team, Participant } from '../../types/Types'

interface TeaRound {
  id: number
  date: Date
  teaMaker: string
}

const props = defineProps<{
  teamId: number | null
  refresh: boolean
}>()

const teaRounds = ref<TeaRound[]>([])

// Watch for changes in refresh prop to refetch data
watch(() => props.refresh, () => {
  if (props.teamId) {
    fetchTeaRounds()
  }
})

// Watch for changes in teamId to fetch data
watch(() => props.teamId, (newTeamId) => {
  if (newTeamId) {
    fetchTeaRounds()
  } else {
    teaRounds.value = []
  }
})

const fetchTeaRounds = async (): Promise<void> => {
  // TODO: Implement actual API call
  // This is placeholder data
  teaRounds.value = [
    {
      id: 1,
      date: new Date('2024-03-15'),
      teaMaker: 'John Doe'
    },
    {
      id: 2,
      date: new Date('2024-03-14'),
      teaMaker: 'Jane Smith'
    }
  ]
}

const formatDate = (date: Date): string => {
  return new Intl.DateTimeFormat('en-GB', {
    day: 'numeric',
    month: 'short',
    year: 'numeric'
  }).format(date)
}
</script>

<style scoped>
.tea-rounds-table {
  margin-bottom: 1rem;
}

.table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

.table th,
.table td {
  padding: 0.5rem;
  text-align: left;
  border-bottom: 1px solid #eee;
}

.table th {
  font-weight: bold;
  background-color: #f5f5f5;
}

.no-rounds {
  color: #666;
  font-style: italic;
  padding: 1rem;
}
</style> 