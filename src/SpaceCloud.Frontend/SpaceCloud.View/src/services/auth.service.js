import axiosModule from 'axios';
import sha256 from 'js-sha256';
import store from '@/store/index';

const API_URL = process.env.VUE_APP_API_URL;
let tenant = '';

if (process.env.NODE_ENV === 'development') {
  tenant = 'dev';
} else {
  tenant = window.location.href.substring(
    window.location.href.indexOf('://') + 3,
    window.location.href.indexOf('.'),
  );
}

const axios = axiosModule.create();

// Request interceptor
axios.interceptors.request.use(
  config => {
    // eslint-disable-next-line no-param-reassign
    config.headers = {
      withCredentials: true,
      Tenant: tenant,
    };
    return config;
  },
  error => {
    Promise.reject(error);
  },
);

class AuthService {
  // eslint-disable-next-line class-methods-use-this
  login(loginData) {
    const data = {
      mail: loginData.mail,
      password: sha256(loginData.password).toString(),
      stayLogged: loginData.stayLogged,
    };

    return axios
      .post(`${API_URL}authorize`, data, { withCredentials: true })
      .then(
        response => {
          return this.parseAuthStateData(response);
        },
        error => {
          throw error;
        },
      );
  }

  // eslint-disable-next-line class-methods-use-this
  register(regData) {
    const data = {
      firstName: regData.firstName,
      lastName: regData.lastName,
      mail: regData.mail,
      password: sha256(regData.password).toString(),
      locationId: regData.locationId,
    };
    return axios.post(`${API_URL}account/register`, data).then(
      () => {
        return store.dispatch('auth/login', regData);
      },
      error => {
        throw error;
      },
    );
  }

  // eslint-disable-next-line class-methods-use-this
  silentRefresh(token) {
    return axios
      .post(`${API_URL}authorize/refresh`, {
        refreshToken: token,
      })
      .then(
        response => {
          return this.parseAuthStateData(response);
        },
        error => {
          throw error;
        },
      );
  }

  parseAuthStateData(response) {
    const d = response.data;
    const tokenParsed = this.parseToken(d.token);

    d.jwt = {
      token: response.data.token,
      exp: tokenParsed.exp,
      iat: tokenParsed.iat,
      nbf: tokenParsed.nbf,
    };
    d.user = {
      CompanyId: tokenParsed.CompanyId,
      LocationId: tokenParsed.LocationId,
      UserId: tokenParsed.UserId,
      role: tokenParsed.role,
    };

    return d;
  }

  // eslint-disable-next-line class-methods-use-this
  parseToken(token) {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch {
      return '';
    }
  }
}

// Axios Interceptors
// axios.interceptors.response.use(
//   response => {
//     return response;
//   },
//   error => {
//     // console.log(error.response.data);
//     // console.log(error.response.status);
//     // console.log(error.response.headers);
//     if (!error.response) {
//       console.log('network error detected! ', error);
//       /* Try Again ? */
//     } else if (error.response.status === 400)
//       console.log('400: ', error);
//     return error;
//   },
// );

export default new AuthService();
