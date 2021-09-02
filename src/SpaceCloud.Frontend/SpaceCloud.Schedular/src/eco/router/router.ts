import {createRouter, createWebHistory, RouteRecordRaw} from 'vue-router';

import home from "../../views/home.vue"
import booking from "../../views/booking.vue"


const routes: Array<RouteRecordRaw> = [
    {
        path: "/login",
        name: 'login',
        component: () => import("../../views/login.vue")
    },
    {
        path: "/scheduler/:token",
        name: 'scheduler',
        component: home,
        props: true
    },
    {
        path: "/notfound",
        name: 'notfound',
        component: () => import("../../views/errors/error.vue")
    },
    {
        path: "/scheduler/purchase",
        name: 'booking',
        component: booking,
        props: true
    },

]


const router = createRouter({
    history: createWebHistory(),
    routes
});




export default router;





