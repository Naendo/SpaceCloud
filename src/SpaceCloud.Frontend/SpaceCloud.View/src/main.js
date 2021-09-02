import Vue from 'vue';
import store from '@/store/index';
// import store from '@/store/index.js';
import VeeValidate from 'vee-validate';

import upperFirst from 'lodash/upperFirst';
import camelCase from 'lodash/camelCase';
import VueSweetalert2 from 'vue-sweetalert2';
import router from './router';
import App from './App.vue';

// require('./styles/css/mystyles.css');
require('./styles/scss/mystyles.scss');

Vue.config.productionTip = false;

// Plugins
Vue.use(VeeValidate);
Vue.use(VueSweetalert2);

// register global base components (./components/base)
const requireComponent = require.context(
  './components/base',
  // Whether or not to look in subfolders
  false,
  // The regular expression used to match base component filenames
  /Base[A-Z]\w+\.(vue|js)$/,
);

requireComponent.keys().forEach(fileName => {
  // Get component config
  const componentConfig = requireComponent(fileName);

  // Get PascalCase name of component
  const componentName = upperFirst(
    camelCase(
      // Gets the file name regardless of folder depth
      fileName
        .split('/')
        .pop()
        .replace(/\.\w+$/, ''),
    ),
  );

  // Register component globally
  Vue.component(
    componentName,
    // Look for the component options on `.default`, which will
    // exist if the component was exported with `export default`,
    // otherwise fall back to module's root.
    componentConfig.default || componentConfig,
  );
});

/* custom error handler:
  err: complete error trace, contains the message and error stack
    e.g.: TypeError: Cannot read property 'CompanyId' of null
      at Object.getRooms (api.js:43)
      ... 
  vm: Vue component/instance in which error is occurred
  info:  Vue specific error information such as lifecycle hooks, events etc
 */

Vue.config.errorHandler = (err /* , vm, info */) => {
  console.log('custom error Handling called: \n', err);

  if (err.response !== undefined)
    console.log(
      'err.response: ',
      err.response.data.message,
      '\n',
      err.response,
    );

  /* if (store.state.auth.user == null || store.state.auth.jwt === '') {
    console.log('Invalid token');
    store.dispatch('auth/silentRefresh').catch(error => {
      console.log('---', error);
      store.dispatch('auth/logout');
    });
  } */
};

window.onerror = (message, source, lineno, colno, error) => {
  console.log(
    'custom window.onerror exception handling (not handled by vue): ' +
      '\n' +
      'message: ',
    message,
    '\n source: ',
    source,
    '\n lineno: ',
    lineno,
    '\n colno: ',
    colno,
    '\n error: ',
    error,
  );
};

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
