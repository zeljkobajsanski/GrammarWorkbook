import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/home',
      name: 'home',
      component: Home
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    },
    {path: '/exercise/:id', name: 'exercise', component: () => import('./views/Exercise.vue')},
    {path: '/units/:unitId/topics/:id?', name: 'topic-edit', component: () => import('./views/TopicEdit.vue')},
    {path: '/', redirect: '/units/2CCB48AD-1627-4349-1717-08D667F7E9FF/topics'},
  ]
})
