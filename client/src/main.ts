import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import VueDragDrop from 'vue-drag-drop';


Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(VueDragDrop);


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
