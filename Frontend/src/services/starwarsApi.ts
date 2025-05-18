import type { StarWarsCharacter, Info, CharacterReview } from '@/types/starWars'
import axios from 'axios'

const BASE_URL = import.meta.env.VITE_API_URL

export const starwarsApi = {
  async getAllCharacters(
    page: number = 1,
    limit?: number,
    search?: string,
  ): Promise<{ info: Info; data: StarWarsCharacter[] }> {
    try {
      const searchParam = search ? `&search=${encodeURIComponent(search)}` : ''
      const limitParam = limit && limit > 0 ? `&limit=${limit}` : ''
      const url = `${BASE_URL}/characters?page=${page}${limitParam}${searchParam}`

      const response = await axios.get(url)
      return response.data
    } catch {
      throw new Error()
    }
  },

  async getCharacterById(id: string): Promise<StarWarsCharacter> {
    try {
      const response = await axios.get(`${BASE_URL}/characters/${id}`)
      return response.data
    } catch {
      throw new Error()
    }
  },

  async submitReview(review: CharacterReview): Promise<void> {
    try {
      await axios.post(`${BASE_URL}/reviews`, review, {
        headers: {
          'Content-Type': 'application/json',
        },
      })
    } catch {
      throw new Error()
    }
  },
}
