import { describe, it, expect, vi, beforeEach } from 'vitest'
import { mount } from '@vue/test-utils'
import { createRouter, createMemoryHistory } from 'vue-router'
import { createPinia, setActivePinia } from 'pinia'
import App from '../src/App.vue'
import CharacterList from '../src/views/CharacterList.vue'
import CharacterListPage from '../src/views/CharacterListPage.vue'

// Mock the MainNavbar component
vi.mock('../src/components/MainNavbar.vue', () => ({
  default: {
    name: 'MainNavbar',
    template: '<div data-testid="mock-main-navbar">Main Navbar</div>'
  }
}))

// Mock the stores
vi.mock('../src/stores/characterStore', () => ({
  useCharacterStore: () => ({
    characters: [],
    loading: false,
    fetchCharacters: vi.fn(),
    searchCharacters: vi.fn()
  })
}))

vi.mock('../src/stores/languageStore', () => ({
  useLanguageStore: () => ({
    t: (key: string) => key,
    currentLanguage: 'en'
  })
}))

describe('App Component', () => {
  beforeEach(() => {
    const pinia = createPinia()
    setActivePinia(pinia)
  })

  it('renders without crashing', async () => {
    // Arrange
    const router = createRouter({
      history: createMemoryHistory(),
      routes: [{ path: '/', component: CharacterListPage }]
    })

    // Act
    const wrapper = mount(App, {
      global: {
        plugins: [router, createPinia()]
      }
    })

    // Wait for router to be ready
    await router.isReady()

    // Assert
    expect(wrapper.element).toBeDefined()
  })

  it('renders CharacterListPage component', async () => {
    // Arrange
    const router = createRouter({
      history: createMemoryHistory(),
      routes: [{ path: '/', component: CharacterListPage }]
    })

    // Act
    const wrapper = mount(App, {
      global: {
        plugins: [router, createPinia()]
      }
    })

    // Wait for router to be ready
    await router.isReady()

    const characterList = wrapper.find('[data-testid="mock-character-list"]')

    // Assert
    expect(characterList.exists()).toBe(true)
  })
})
