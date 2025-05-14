import { createRouter, createWebHistory } from 'vue-router'
import CharacterList from '../views/CharacterList.vue'
import CharacterDetail from '../views/CharacterDetail.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'star-wars',
      component: CharacterList
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/character/:name',
      name: 'character-detail',
      component: CharacterDetail,
      props: true
    },
    {
      path: '/:pathMatch(.*)*',
      redirect: { name: 'star-wars' }
    }
  ]
})

export default router
