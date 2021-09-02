/* import store from '@/store/index.js';

export default function authHeader() {
  let user = store.state.auth.user;

  if (process.env.NODE_ENV === 'development')
    if (user && store.state.auth.token) {
      return { Authentication: 'bearer ' + user.accessToken };
    } else {
      return {};
    }
} */
