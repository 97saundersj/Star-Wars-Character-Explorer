export interface Info {
  total: number
  page: number
  limit: number
  hasNext: boolean
  hasPrevious: boolean
}

export interface StarWarsCharacter {
  _id: string
  name: string
  description: string
  image: string
  __v: number
}

export interface Character extends StarWarsCharacter {
  isLiked: boolean
}

export interface CharacterReview {
  id: string
  characterName: string
  reviewerName: string
  watchDate: string
  reviewDetails: string
  rating: number
  createdAt: string
}

export interface CharacterState {
  characters: Character[]
  loading: boolean
  reviews: CharacterReview[]
  currentPage: number
  totalPages: number
  hasNextPage: boolean
  hasPreviousPage: boolean
  searchQuery: string
  pageSize: number
  info: Info
  likedCharacterIds: string[]
}
