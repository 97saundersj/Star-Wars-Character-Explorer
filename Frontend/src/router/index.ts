import { createRouter, createWebHistory } from 'vue-router'
import CharacterListPage from '../views/CharacterListPage.vue'
import CharacterDetailPage from '../views/CharacterDetailPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'star-wars',
      component: CharacterListPage,
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutPage.vue'),
    },
    {
      path: '/character/:name',
      name: 'character-detail',
      component: CharacterDetailPage,
      props: true,
    },
    {
      path: '/:pathMatch(.*)*',
      redirect: { name: 'star-wars' },
    },
  ],
})

export default router
