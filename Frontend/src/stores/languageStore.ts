import { defineStore } from 'pinia'

// Types
export type Locale = 'en' | 'au' | 'fr'

interface LocalizationState {
  currentLocale: Locale
}

// Translation Schema
interface TranslationSchema {
  appTitle: string
  characters: string
  backToCharacters: string
  writeReview: string
  yourName: string
  dateWatched: string
  reviewDetails: string
  rating: string
  submitReview: string
  language: string
  searchCharacters: string
  clearSearch: string
  displayLimit: string
  noResults: string
  about: string
  checkGithub: string
  errorFetchingCharacters: string
  errorCreatingReview: string
  successReview: string
}

export type TranslationKey = keyof TranslationSchema
type Translations = Record<Locale, TranslationSchema>

// Translation Data
const translations: Translations = {
  en: {
    appTitle: 'Star Wars Characters',
    characters: 'Characters',
    backToCharacters: 'Back',
    writeReview: 'Write a Character Review',
    yourName: 'Your Name',
    dateWatched: 'When did you last watch this character?',
    reviewDetails: 'Character Review',
    rating: 'Rating',
    submitReview: 'Submit Review',
    language: 'Language',
    searchCharacters: 'Search character names...',
    clearSearch: 'Clear search',
    displayLimit: 'Results',
    noResults: 'No characters found',
    about: 'About',
    checkGithub: 'Check out the project on GitHub',
    errorFetchingCharacters: 'Error fetching characters.\nI have a bad feeling about this...',
    errorCreatingReview: 'Error creating review.\nI have a bad feeling about this...',
    successReview: 'May the Force be review!',
  },
  au: {
    appTitle: 'Star Wars Characters',
    characters: 'Characters',
    backToCharacters: 'Back',
    writeReview: 'Write a Character Review',
    yourName: 'Your Name',
    dateWatched: 'When did you last watch this character?',
    reviewDetails: 'Character Review',
    rating: 'Rating',
    submitReview: 'Submit Review',
    language: 'Language',
    searchCharacters: 'Search character names...',
    clearSearch: 'Clear search',
    displayLimit: 'Results',
    noResults: 'No characters found',
    about: 'About',
    checkGithub: 'Check out the project on GitHub',
    errorFetchingCharacters: 'Error fetching characters.\nI have a bad feeling about this...',
    errorCreatingReview: 'Error creating review.\nI have a bad feeling about this...',
    successReview: 'May the Force be review!',
  },
  fr: {
    appTitle: 'Personnages Star Wars',
    characters: 'Personnages',
    backToCharacters: 'Retour',
    writeReview: 'Écrire une critique du personnage',
    yourName: 'Votre nom',
    dateWatched: 'Quand avez-vous vu ce personnage pour la dernière fois ?',
    reviewDetails: 'Critique du personnage',
    rating: 'Note',
    submitReview: 'Soumettre la critique',
    language: 'Langue',
    searchCharacters: 'Rechercher des noms de personnages...',
    clearSearch: 'Effacer la recherche',
    displayLimit: 'Résultats',
    noResults: 'Aucun personnage trouvé',
    about: 'À propos',
    checkGithub: 'Voir le projet sur GitHub',
    errorFetchingCharacters: 'Erreur lors du chargement des personnages.\nJ\'ai un mauvais pressentiment...',
    errorCreatingReview: 'Erreur lors de la création de la critique.\nJ\'ai un mauvais pressentiment...',
    successReview: 'Que la Force soit avec votre critique !',
  },
}

// Store Definition
export const useLocalizationStore = defineStore('localization', {
  state: (): LocalizationState => ({
    currentLocale: 'en',
  }),

  actions: {
    setLocale(locale: Locale) {
      this.currentLocale = locale
      document.documentElement.setAttribute('data-locale', locale)
    },
  },

  getters: {
    getLocalizedText: (state) => (key: TranslationKey) => {
      return translations[state.currentLocale][key]
    },
  },
})
