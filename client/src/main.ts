import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import VueDragDrop from 'vue-drag-drop';
import 'font-awesome/css/font-awesome.min.css'
import Toasted from 'vue-toasted';


Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(VueDragDrop);
Vue.use(Toasted, {iconPack: 'fontawesome', duration: 2000});


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
