/* eslint-disable no-plusplus */
import Vue from 'vue';
import Vuex from 'vuex';
// eslint-disable-next-line import/named
import api from '@/services/api';
import { auth } from './auth.module';
// import router from '../router';

Vue.use(Vuex);

function getCartIndex(context, productId) {
  if (typeof productId !== 'number') productId = productId.productId;
  return context.state.cart.findIndex(i => i.productId === productId);
}

function saveCart(cart) {
  window.localStorage.setItem('cart', JSON.stringify(cart));
}

export default new Vuex.Store({
  modules: {
    auth,
  },
  state: {
    rooms: [],
    desks: [],
    cart: [],
  },
  actions: {
    fetchRooms(context) {
      api.getRooms().then(response => {
        context.commit('SET_ROOMS', response.data.reverse());
        return response.data;
      });
    },
    fetchDesks(context) {
      api.getDesks().then(response => {
        context.commit('SET_DESKS', response.data.reverse());
      });
    },

    // Cart
    loadCart(context) {
      context.commit('LOAD_CART_FROM_LS');
    },
    clearCart(context) {
      context.commit('CLEAR_CART');
    },
    addCartItem(context, item) {
      // Cart Item: { usage, startDate, endDate, productId }
      item.quantity = 1;
      context.commit('CART_ITEM_ADD', item);
    },
    removeCartItem(context, productId) {
      const index = getCartIndex(context, productId);
      if (index !== -1) context.commit('CART_ITEM_REMOVE', index);
      else
        console.error(
          'something went wrong while removing a cart item: no fitting product could be found',
        );
    },
    cartItemSetQuantity(context, { productId, quantity }) {
      const index = getCartIndex(context, productId);
      if (index !== -1)
        context.commit('CART_ITEM_SET_QUANTITY', {
          index,
          quantity,
        });
      else
        console.error(
          'something went wrong while editing a cart item: no fitting product could be found',
        );
    },
  },
  mutations: {
    SET_ROOMS(state, rooms) {
      Vue.set(state, 'rooms', rooms);
    },

    SET_DESKS(state, desks) {
      state.desks = desks;
    },

    // Cart
    LOAD_CART_FROM_LS(state) {
      if (state.cart.length > 0)
        console.log(
          'Cart is not empty and will be overwritten! old cart: ',
          state.cart,
        );
      const cart = JSON.parse(window.localStorage.getItem('cart'));
      console.log('LOAD_CART_FROM_LS: ', cart);
      if (cart !== null) if (cart.length > 1) state.cart = cart;
    },
    CART_ITEM_ADD(state, item) {
      state.cart.push(item);
      saveCart(state.cart);
    },
    CART_ITEM_REMOVE(state, index) {
      state.cart.splice(index, 1);
      saveCart(state.cart);
    },
    CART_ITEM_SET_QUANTITY(state, { index, quantity }) {
      Vue.set(state.cart[index], 'quantity', quantity);
      saveCart(state.cart);
    },
    CLEAR_CART(state) {
      state.cart = [];
      window.localStorage.removeItem('cart');
    },
  },
});
