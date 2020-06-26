import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import VueRouter from 'vue-router'

// router setup
//eslint-disable-next-line
import routes from "./router.js";

Vue.config.productionTip = false
Vue.use(VueRouter)
const router = Vue.router;

new Vue({
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app')