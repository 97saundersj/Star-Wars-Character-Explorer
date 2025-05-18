import { defineStore } from 'pinia'

export type Language = 'en' | 'au'

interface LanguageState {
  currentLanguage: Language
}

const translations = {
  en: {
    appTitle: 'Star Wars Characters',
    characters: 'Characters',
    birthYear: 'Birth Year',
    gender: 'Gender',
    height: 'Height',
    mass: 'Mass',
    hairColor: 'Hair Color',
    skinColor: 'Skin Color',
    eyeColor: 'Eye Color',
    species: 'Species',
    homeworld: 'Homeworld',
    bornLocation: 'Born Location',
    died: 'Died',
    diedLocation: 'Died Location',
    cybernetics: 'Cybernetics',
    affiliations: 'Affiliations',
    masters: 'Masters',
    apprentices: 'Apprentices',
    formerAffiliations: 'Former Affiliations',
    wikiLink: 'Wiki Page',
    viewDetails: 'View Details',
    writeReview: 'Write a Character Review',
    yourName: 'Your Name',
    dateWatched: 'When did you last watch this character?',
    reviewDetails: 'Character Review',
    rating: 'Rating',
    submitReview: 'Submit Review',
    reviews: 'Reviews',
    watchedOn: 'Watched On',
    reviewedBy: 'Reviewed By',
    backToCharacters: 'Back',
    loading: 'Loading...',
    error: 'Failed to fetch characters',
    on: 'on',
    previous: 'Previous',
    next: 'Next',
    page: 'Page',
    of: 'of',
    searchCharacters: 'Search character names...',
    searchLabel: 'Character Search',
    clearSearch: 'Clear search',
    noResults: 'No characters found',
    itemsPerPage: 'Items per page',
    noReviews: 'No reviews yet',
    displayLimit: 'Results',
    showAll: 'All',
    results: 'results',
    language: 'Language',
  },
  au: {
    appTitle: 'Star Wars Characters',
    characters: 'Characters',
    birthYear: 'Birth Year',
    gender: 'Gender',
    height: 'Height',
    mass: 'Mass',
    hairColor: 'Hair Color',
    skinColor: 'Skin Color',
    eyeColor: 'Eye Color',
    species: 'Species',
    homeworld: 'Homeworld',
    bornLocation: 'Born Location',
    died: 'Died',
    diedLocation: 'Died Location',
    cybernetics: 'Cybernetics',
    affiliations: 'Affiliations',
    masters: 'Masters',
    apprentices: 'Apprentices',
    formerAffiliations: 'Former Affiliations',
    wikiLink: 'Wiki Page',
    viewDetails: 'View Details',
    writeReview: 'Write a Character Review',
    yourName: 'Your Name',
    dateWatched: 'When did you last watch this character?',
    reviewDetails: 'Character Review',
    rating: 'Rating',
    submitReview: 'Submit Review',
    reviews: 'Reviews',
    watchedOn: 'Watched On',
    reviewedBy: 'Reviewed By',
    backToCharacters: 'Back',
    loading: 'Loading...',
    error: 'Failed to fetch characters',
    on: 'on',
    previous: 'Previous',
    next: 'Next',
    page: 'Page',
    of: 'of',
    searchCharacters: 'Search character names...',
    searchLabel: 'Character Search',
    clearSearch: 'Clear search',
    noResults: 'No characters found',
    itemsPerPage: 'Items per page',
    noReviews: 'No reviews yet',
    displayLimit: 'Results',
    showAll: 'All',
    results: 'results',
    language: 'Language',
  },
}

export const useLanguageStore = defineStore('language', {
  state: (): LanguageState => ({
    currentLanguage: 'en',
  }),

  actions: {
    setLanguage(language: Language) {
      this.currentLanguage = language
      document.documentElement.setAttribute('data-language', language)
    },
  },

  getters: {
    t: (state) => (key: keyof typeof translations.en) => {
      return translations[state.currentLanguage][key]
    },
  },
})
