import {createApp} from 'vue'
import App from './App.vue'
import router from './eco/router/router'

const app = createApp(App)
    .use(router)
    .directive('focus', {
        mounted: function (el) {
            el.focus()
        }
    })
    .mount('#app')


