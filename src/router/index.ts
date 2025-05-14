import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import CharacterList from '@/views/CharacterList.vue'
import CharacterDetail from '@/views/CharacterDetail.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/tea-rounds',
      name: 'tea-rounds',
      component: () => import('../components/TeaRoundPickerPage.vue')
    },
    {
      path: '/star-wars',
      name: 'star-wars',
      component: CharacterList
    },
    {
      path: '/character/:name',
      name: 'character-detail',
      component: CharacterDetail,
      props: true
    }
  ]
})

export default router
