import AuthService from '@/services/auth.service';
import router from '../router';

export const auth = {
  namespaced: true,
  state: {
    user: null,
    jwt: {},
    refresh: {},
    loggedIn: false,
  },
  actions: {
    login(context, data) {
      return AuthService.login(data).then(
        response => {
          context.commit('AUTH_SUCCESS', response);
          context.dispatch('loadCart', {}, { root: true });
          return Promise.resolve(response);
        },
        error => {
          // passing authorization if backend server is offline
          // for dev purposes only -> REMOVE!
          // if (process.env.NODE_ENV === 'development') {
          //   console.error('AUTHORIZATION DISABLED!');
          //   console.warn('Passing Authorization..');
          //   return true;
          // }
          context.commit('LOGOUT');
          return Promise.reject(error);
        },
      );
    },
    register(context, data) {
      return AuthService.register(data).then(
        response => {
          context.dispatch('clearCart', {}, { root: true });
          return Promise.resolve(response);
        },
        error => {
          context.commit('LOGOUT');
          return Promise.reject(error);
        },
      );
    },
    logout(context) {
      context.dispatch('clearCart', {}, { root: true });
      context.commit('LOGOUT');
      router.push({ name: 'Login' });
    },
    // not in use (yet?)
    // updateJWT(context, data) {
    //   const d = {
    //     token: data,
    //     exp: AuthService.parseToken(data).Exp,
    //   };
    //   context.commit('SET_JWT', d);
    // },
    silentRefresh(context) {
      const refreshToken = JSON.parse(
        localStorage.getItem('refreshToken'),
      ).token;
      if (!refreshToken)
        throw new Error('no refresh token available');

      return AuthService.silentRefresh(refreshToken).then(
        response => {
          context.commit('AUTH_SUCCESS', response);
        },
        error => {
          console.log(error);
          context.commit('LOGOUT');
        },
      );
    },
  },
  mutations: {
    AUTH_SUCCESS(state, data) {
      state.jwt = data.jwt;
      state.refresh = {
        token: data.refreshToken,
        exp: data.refreshExp,
      };
      state.user = data.user;
      state.loggedIn = true;
      localStorage.setItem('loggedIn', 'true');
      localStorage.setItem(
        'refreshToken',
        JSON.stringify(state.refresh),
      );
    },
    LOGOUT(state) {
      state.loggedIn = false;
      localStorage.setItem('loggedIn', 'false');
      localStorage.removeItem('cart');
      localStorage.removeItem('refreshToken');
      state.user = null;
      state.jwt = '';
    },
    SET_JWT(state, data) {
      // setting loggedIn nessecary?
      state.loggedIn = true;
      localStorage.setItem('loggedIn', 'true');
      state.jwt = data;
    },
  },
  getters: {
    tokenBearer: state => {
      if (state.jwt == null || state.jwt === undefined) return null;
      return `bearer ${state.jwt.token}`;
    },
    isAdmin: state => {
      if (state.user === null || state.user === undefined)
        return false;
      if (
        state.user.role === 'Administrator' ||
        state.user.role === 'Owner'
      )
        return true;
      return false;
    },
  },
};

export default auth;
