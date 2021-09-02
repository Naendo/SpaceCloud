/* eslint-disable no-sequences */
import axiosModule from 'axios';
import store from '@/store/index';
// import AuthService from '@/services/auth.service';

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
const axiosFile = axiosModule.create();

// Interceptors
// Request interceptor
axios.interceptors.request.use(
  async config => {
    config.headers = {
      Authorization: await store.getters['auth/tokenBearer'],
      withCredentials: true,
      Tenant: tenant,
    };
    return config;
  },
  error => {
    Promise.reject(error);
  },
);

axiosFile.interceptors.request.use(
  config => {
    config.headers = {
      Authorization: store.getters['auth/tokenBearer'],
      withCredentials: true,
      Tenant: tenant,
      'Content-Type': 'multipart/form-data',
    };
    return config;
  },
  error => {
    Promise.reject(error);
  },
);

// Response interceptor
axios.interceptors.response.use(
  response => {
    // check if response data is string, then convert to json (response caching returns data as string)
    if (typeof response.data === 'string') {
      if (response.data) response.data = JSON.parse(response.data);
    }

    return response;
  },
  error => {
    if (!error.response)
      console.log('network error detected! ', error);
    else if (error.response.status === 401) {
      console.log('not authorized');
    }
    // user not logged in? (vuex state) - needs to be reworked
    else if (
      store.state.auth.user == null ||
      store.state.auth.jwt === ''
    ) {
      console.log('user not logged in');
      store.dispatch('auth/logout');
    }

    // 403 -> valid bearer token but user doesn't have enough permission
    if (error.response.status === 403) {
      console.log('Access denied');
    }

    return Promise.reject(error);
  },
);

// Calls
export default {
  // dashboard
  fetchRecentActions() {
    return axios
      .get(`${API_URL}dashboard/fetch/actions`)
      .then(result => {
        return result.data;
      });
  },
  fetchTickets() {
    return axios
      .get(`${API_URL}dashboard/fetch/tickets`)
      .then(result => {
        return result.data;
      });
  },
  fetchCoworkingUsage() {
    return axios
      .get(`${API_URL}dashboard/fetch/usage`)
      .then(result => {
        return result.data;
      });
  },

  // Product
  getDesks() {
    return axios.get(`${API_URL}product/desk/get`);
  },
  getRooms() {
    return axios.get(`${API_URL}product/room/get`);
  },
  getRoom(pid) {
    return axios.get(`${API_URL}product/room/get/${pid}`);
  },
  getDesk(pid) {
    return axios.get(`${API_URL}product/desk/get/${pid}`);
  },
  createDesk(desk) {
    const d = {
      description: desk.description,
      name: desk.name,
      imageUri: desk.imageUri,
      price: parseInt(desk.price, 10),
      stockAmount: parseInt(desk.stockAmount, 10),
      intervalType: parseInt(desk.intervalType, 10),
    };
    return axios.post(`${API_URL}product/desk/add`, d);
  },
  createRoom(room) {
    return axios.post(`${API_URL}product/room/add`, {
      description: room.description,
      name: room.name,
      imageUri: room.imageUri,
      price: parseInt(room.price, 10),
      capacity: parseInt(room.capacity, 10),
      tags: room.tags,
    });
  },
  deleteProduct(pid) {
    return axios.delete(`${API_URL}product/delete/${pid}`);
  },
  enableProduct(pid) {
    return axios.put(`${API_URL}product/enable/${pid}`);
  },
  disableProduct(pid) {
    return axios.put(`${API_URL}product/disable/${pid}`);
  },
  imgUpload(file) {
    const formData = new FormData();
    formData.append('file', file);
    return axiosFile.post(`${API_URL}file/upload`, formData);
  },

  // Order
  getOrders() {
    if (store.getters['auth/isAdmin'])
      return axios.get(`${API_URL}order/get`);
    return axios.get(`${API_URL}worker/order/get`);
  },
  getOrder(invId) {
    return axios.get(`${API_URL}order/get/${invId}`);
  },
  createOrder(purchase) {
    return axios.post(`${API_URL}order/add`, purchase);

    // return axios.post(`${API_URL}order/add`, {
    //   category: purchase.room.category,
    //   pid: purchase.room.pid,
    //   usage: purchase.usage,
    //   duration: 0,
    //   startDate: purchase.startDate,
    //   endDate: purchase.endDate,
    // });
  },
  acceptOrder(invId) {
    return axios.put(`${API_URL}order/accept/${invId}`);
  },
  denyOrder(invId) {
    return axios.put(`${API_URL}order/deny/${invId}`);
  },
  paidOrder(invId) {
    return axios.put(`${API_URL}order/paid/${invId}`);
  },

  // Users
  async getUsers() {
    const locId = store.state.auth.user.LocationId;
    const response = await axios.get(`${API_URL}users/get`);
    response.data.locationId = locId;
    return response;
  },
  getUser(uid) {
    return axios.get(`${API_URL}users/get/${uid}`);
  },
  getCurrentUser() {
    return axios.get(`${API_URL}user/get`);
  },
  promoteUser(userId) {
    return axios.put(`${API_URL}account/promote/${userId}`);
  },
  demoteUser(userId) {
    return axios.put(`${API_URL}account/demote/${userId}`);
  },

  // Tickets
  createTicket(subject, content) {
    return axios.post(`${API_URL}ticket/add`, {
      subject,
      content,
    });
  },
};
