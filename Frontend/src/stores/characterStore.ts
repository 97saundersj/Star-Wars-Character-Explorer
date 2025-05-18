import { defineStore } from 'pinia'
import type { CharacterReview, CharacterState, Character } from '@/types/starWars'
import { starwarsApi } from '@/services/starwarsApi'

export const useCharacterStore = defineStore('character', {
  state: (): CharacterState => ({
    characters: [],
    loading: false,
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
      hasNext: false,
      hasPrevious: false,
    },
    likedCharacterIds: JSON.parse(localStorage.getItem('likedCharacters') || '[]'),
  }),

  actions: {
    async fetchCharacters(page: number = 1) {
      this.loading = true
      try {
        const search = typeof this.searchQuery === 'string' ? this.searchQuery : ''
        const response = await starwarsApi.getAllCharacters(page, this.pageSize, search)

        this.characters = response.data.map(
          (char): Character => ({
            ...char,
            isLiked: this.likedCharacterIds.includes(char._id),
          }),
        )
        this.info = response.info

        // Set pagination state
        this.currentPage = this.info.page
        this.totalPages = Math.ceil(this.info.total / this.info.limit)
        this.hasNextPage = this.info.hasNext
        this.hasPreviousPage = this.info.hasPrevious
      } catch {
        throw new Error()
      } finally {
        this.loading = false
      }
    },

    async searchCharacters(query: string) {
      this.searchQuery = query || ''
      await this.fetchCharacters(1)
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

    toggleLike(characterId: string) {
      const character = this.characters.find((char) => char._id === characterId)
      if (character) {
        character.isLiked = !character.isLiked

        if (character.isLiked) {
          if (!this.likedCharacterIds.includes(characterId)) {
            this.likedCharacterIds.push(characterId)
          }
        } else {
          this.likedCharacterIds = this.likedCharacterIds.filter(id => id !== characterId)
        }

        // Save to localStorage
        localStorage.setItem('likedCharacters', JSON.stringify(this.likedCharacterIds))
      }
    },

    async submitReview(review: CharacterReview) {
      try {
        await starwarsApi.submitReview(review)
      } catch {
        throw new Error()
      }
    },
  },

  getters: {
    getCharacterByName: (state) => (name: string) => {
      return state.characters.find((char) => char.name === name)
    },
    getLikedCharacters: (state) => {
      return state.characters.filter((char) => char.isLiked)
    },
  },
})
