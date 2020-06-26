/* eslint-disable */
import Vue from 'vue';
import Router from 'vue-router';
import CadastroUsuario from './components/CadastroUsuario';
import Tarefas from './components/Tarefas';
import Login from "./components/Login";
var layoutBlank = "blank";

Vue.router = new Router({
    routes: [{
            path: '/login',
            name: 'login',
            component: Login,
            meta: {
                auth: false,
                layout: layoutBlank
            }
        },
        {
            path: '/cadastro',
            name: '',
            component: CadastroUsuario
        },
        {
            path: '/tarefas',
            name: '',
            component: Tarefas
        }
    ],
    mode: 'history',
    hashbang: false,
    base: __dirname,
    transitionOnLoad: true,
    linkActiveClass: 'active'
});

Vue.router.beforeEach((to, from, next) => {
    if (localStorage.getItem('jAuth') == null &&
        to.fullPath != "/cadastro" &&
        to.fullPath != "/login") {
        location.href = "/login";
    } else {
        next();
    }
});