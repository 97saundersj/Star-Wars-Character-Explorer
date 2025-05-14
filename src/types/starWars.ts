import type { IPeople } from 'swapi-ts'

export type Character = IPeople & {
  isLiked?: boolean
}

export interface CharacterReview {
  id: string
  characterName: string
  reviewerName: string
  watchDate: string
  reviewDetail: string
  rating: number
  createdAt: string
}

export interface CharacterState {
  characters: Character[]
  loading: boolean
  error: string | null
  reviews: CharacterReview[]
  currentPage: number
  totalPages: number
  hasNextPage: boolean
  hasPreviousPage: boolean
  searchQuery: string
}
