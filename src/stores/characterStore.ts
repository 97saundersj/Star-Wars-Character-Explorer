import { defineStore } from 'pinia'
import axios from 'axios'
import type { CharacterReview, CharacterState, Character } from '@/types/starWars'
import type { IPeople } from 'swapi-ts'

// Configure SWAPI to use HTTPS endpoint
const SWAPI_BASE_URL = 'https://swapi.py4e.com/api'

interface SWAPIResponse {
  count: number
  next: string | null
  previous: string | null
  results: IPeople[]
}

export const useCharacterStore = defineStore('character', {
  state: (): CharacterState => ({
    characters: [],
    loading: false,
    error: null,
    reviews: [],
    currentPage: 1,
    totalPages: 1,
    hasNextPage: false,
    hasPreviousPage: false,
    searchQuery: ''
  }),

  actions: {
    async fetchCharacters(page: number = 1) {
      this.loading = true
      try {
        const response = await axios.get<SWAPIResponse>(`${SWAPI_BASE_URL}/people/?page=${page}`)
        this.characters = response.data.results.map((char: IPeople): Character => ({
          ...char,
          isLiked: false
        }))
        this.currentPage = page
        this.totalPages = Math.ceil(response.data.count / 10)
        this.hasNextPage = !!response.data.next
        this.hasPreviousPage = !!response.data.previous
        this.error = null
      } catch (error) {
        this.error = 'Failed to fetch characters'
        console.error('Error fetching characters:', error)
      } finally {
        this.loading = false
      }
    },

    async searchCharacters(query: string) {
      this.loading = true
      this.searchQuery = query
      try {
        const response = await axios.get<SWAPIResponse>(`${SWAPI_BASE_URL}/people/?search=${encodeURIComponent(query)}`)
        this.characters = response.data.results.map((char: IPeople): Character => ({
          ...char,
          isLiked: false
        }))
        this.currentPage = 1
        this.totalPages = Math.ceil(response.data.count / 10)
        this.hasNextPage = !!response.data.next
        this.hasPreviousPage = !!response.data.previous
        this.error = null
      } catch (error) {
        this.error = 'Failed to search characters'
        console.error('Error searching characters:', error)
      } finally {
        this.loading = false
      }
    },

    nextPage() {
      if (this.hasNextPage) {
        this.fetchCharacters(this.currentPage + 1)
      }
    },

    previousPage() {
      if (this.hasPreviousPage) {
        this.fetchCharacters(this.currentPage - 1)
      }
    },

    toggleLike(characterName: string) {
      const character = this.characters.find(char => char.name === characterName)
      if (character) {
        character.isLiked = !character.isLiked
      }
    },

    async submitReview(review: Omit<CharacterReview, 'id' | 'createdAt'>) {
      try {
        // This will fail as specified in the requirements
        await fetch(`${SWAPI_BASE_URL}/reviews/`, {
          method: 'POST',
          body: JSON.stringify(review)
        })
      } catch {
        // Store the review locally since the API call will fail
        const newReview: CharacterReview = {
          ...review,
          id: crypto.randomUUID(),
          createdAt: new Date().toISOString()
        }
        this.reviews.push(newReview)
        throw new Error('Failed to submit review to API, but saved locally')
      }
    }
  },

  getters: {
    getCharacterByName: (state) => (name: string) => {
      return state.characters.find(char => char.name === name)
    },
    getLikedCharacters: (state) => {
      return state.characters.filter(char => char.isLiked)
    },
    getCharacterReviews: (state) => (characterName: string) => {
      return state.reviews.filter(review => review.characterName === characterName)
    },
    filteredCharacters: (state) => {
      if (!state.searchQuery) return state.characters
      const query = state.searchQuery.toLowerCase()
      return state.characters.filter(char =>
        char.name.toLowerCase().includes(query)
      )
    }
  }
})
