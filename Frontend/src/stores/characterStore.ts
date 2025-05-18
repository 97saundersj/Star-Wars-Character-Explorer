import { defineStore } from 'pinia'
import type { CharacterReview, CharacterState, Character, StarWarsCharacter } from '@/types/starWars'
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
      hasNext: false,
      hasPrevious: false
    }
  }),

  actions: {
    async fetchCharacters(page: number = 1) {
      this.loading = true
      try {
        const search = typeof this.searchQuery === 'string' ? this.searchQuery : '';
        const response = await starwarsApi.getAllCharacters(
          page,
          this.pageSize,
          search
        );

        this.characters = response.data.map((char): Character => ({
          ...char,
          isLiked: false
        }));
        this.info = response.info;

        // Set pagination state
        this.currentPage = this.info.page;
        this.totalPages = Math.ceil(this.info.total / this.info.limit);
        this.hasNextPage = this.info.hasNext;
        this.hasPreviousPage = this.info.hasPrevious;
        this.error = null;
      } catch (error) {
        this.error = 'Failed to fetch characters';
      } finally {
        this.loading = false;
      }
    },

    async searchCharacters(query: string) {
      this.searchQuery = query || '';
      await this.fetchCharacters(1);
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
    }
  }
})
