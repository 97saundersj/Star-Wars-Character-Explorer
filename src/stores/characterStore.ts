import { defineStore } from 'pinia'
import type { CharacterReview, CharacterState, Character } from '@/types/starWars'
import { starwarsApi } from '@/services/starwarsApi'

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
    searchQuery: '',
    pageSize: 24,
    info: {
      total: 0,
      page: 1,
      limit: 24,
      next: null,
      prev: null
    }
  }),

  actions: {
    // Helper function to normalize strings for search
    normalizeSearchString(str: string): string {
      return str
        .toLowerCase()
        .replace(/[^a-z0-9]/g, '') // Remove all non-alphanumeric characters
    },

    async fetchCharacters(page: number = 1) {
      this.loading = true
      try {
        if (this.searchQuery) {
          // If we're searching, fetch all characters and filter
          const response = await starwarsApi.getAllCharacters(1, 1000)
          const normalizedQuery = this.normalizeSearchString(this.searchQuery)
          const filteredCharacters = response.data.filter(char =>
            this.normalizeSearchString(char.name).includes(normalizedQuery)
          )

          // Calculate pagination
          const startIndex = (page - 1) * this.pageSize
          const endIndex = startIndex + this.pageSize

          this.characters = filteredCharacters
            .slice(startIndex, endIndex)
            .map((char): Character => ({
              ...char,
              isLiked: false
            }))

          this.info = {
            total: filteredCharacters.length,
            page: page,
            limit: this.pageSize,
            next: endIndex < filteredCharacters.length ? String(page + 1) : null,
            prev: page > 1 ? String(page - 1) : null
          }
        } else {
          // Normal fetch without search
          const response = await starwarsApi.getAllCharacters(page, this.pageSize)
          this.characters = response.data.map((char): Character => ({
            ...char,
            isLiked: false
          }))
          this.info = response.info
        }

        this.currentPage = this.info.page
        this.totalPages = Math.ceil(this.info.total / this.info.limit)
        this.hasNextPage = !!this.info.next
        this.hasPreviousPage = !!this.info.prev
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
        // Fetch all characters first
        const response = await starwarsApi.getAllCharacters(1, 1000)
        const normalizedQuery = this.normalizeSearchString(query)
        const filteredCharacters = response.data.filter(char =>
          this.normalizeSearchString(char.name).includes(normalizedQuery)
        )

        // Update pagination info
        this.info = {
          total: filteredCharacters.length,
          page: 1,
          limit: this.pageSize,
          next: null,
          prev: null
        }

        // Get the first page of filtered results
        const startIndex = 0
        const endIndex = this.pageSize
        this.characters = filteredCharacters
          .slice(startIndex, endIndex)
          .map((char): Character => ({
            ...char,
            isLiked: false
          }))

        this.currentPage = 1
        this.totalPages = Math.ceil(filteredCharacters.length / this.pageSize)
        this.hasNextPage = filteredCharacters.length > this.pageSize
        this.hasPreviousPage = false
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
        await fetch('https://akabab.github.io/starwars-api/api/reviews/', {
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
      const normalizedQuery = state.searchQuery.toLowerCase().replace(/[^a-z0-9]/g, '')
      return state.characters.filter(char =>
        char.name.toLowerCase().replace(/[^a-z0-9]/g, '').includes(normalizedQuery)
      )
    }
  }
})
