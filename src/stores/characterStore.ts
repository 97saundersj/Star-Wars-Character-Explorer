import { defineStore } from 'pinia'
import axios from 'axios'
import type { Character, CharacterReview, CharacterState } from '@/types/starWars'

export const useCharacterStore = defineStore('character', {
  state: (): CharacterState => ({
    characters: [],
    loading: false,
    error: null,
    reviews: []
  }),

  actions: {
    async fetchCharacters() {
      this.loading = true
      try {
        const response = await axios.get('https://swapi.py4e.com/api/people/')
        this.characters = response.data.results.map((char: Character) => ({
          ...char,
          isLiked: false
        }))
        this.error = null
      } catch (error) {
        this.error = 'Failed to fetch characters'
        console.error('Error fetching characters:', error)
      } finally {
        this.loading = false
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
        await axios.post('https://swapi.py4e.com/api/reviews/', review)
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
    }
  }
})
