import Vue from 'vue';
import VueRouter from 'vue-router';

const Login = () => import('@/views/Login');

// Webpack Chunks: Tool, Products, NewProduct, EditProduct, Users, Cart, Calednar, Orders, Settings, Scheduler

const Tool = () =>
  import(/* webpackChunkName: "Tool" */ '../views/Tool.vue');
// const Error404 = () =>
//   import(/* webpackChunkName: "Tool" */ '../views/Error404.vue');
const Dashboard = () =>
  import(/* webpackChunkName: "Tool" */ '../views/Dashboard.vue');

// Products
const Products = () =>
  import(
    /* webpackChunkName: "Products" */ '../views/products/Products.vue'
  );
const NewProduct = () =>
  import(
    /* webpackChunkName: "NewProduct" */ '../views/products/NewProduct.vue'
  );
const EditProduct = () =>
  import(
    /* webpackChunkName: "EditProduct" */ '../views/products/EditProduct.vue'
  );
const RoomDetail = () =>
  import(
    /* webpackChunkName: "Products" */ '../views/products/RoomDetail.vue'
  );
const DeskDetail = () =>
  import(
    /* webpackChunkName: "Products" */ '../views/products/DeskDetail.vue'
  );

// User
const UserList = () =>
  import(/* webpackChunkName: "Users" */ '../views/UserList.vue');
const UserDetail = () =>
  import(/* webpackChunkName: "Users" */ '../views/UserDetail.vue');

// Cart
const Cart = () =>
  import(/* webpackChunkName: "Cart" */ '../views/Cart.vue');

const Checkout = () =>
  import(/* webpackChunkName: "Cart" */ '../views/Checkout.vue');

// Calendar
const Calendar = () =>
  import(/* webpackChunkName: "Calendar" */ '../views/Calendar.vue');

// Order
const OrderList = () =>
  import(/* webpackChunkName: "Orders" */ '../views/OrderList.vue');
const OrderDetail = () =>
  import(/* webpackChunkName: "Orders" */ '../views/OrderDetail.vue');
// const OrderError404 = () =>
//   import(/* webpackChunkName: "Orders" */ '../views/Error404.vue');

// Scheduler
const Scheduler = () =>
  import(/* webpackChunkName: "Settings" */ '../views/Settings.vue');

// Settings
const Settings = () =>
  import(
    /* webpackChunkName: "Scheduler" */ '../views/Scheduler.vue'
  );

Vue.use(VueRouter);

const routes = [
  // App 'entry point'
  { path: '', redirect: { name: 'Login' } },
  {
    path: '/auth',
    name: 'Login',
    component: Login,
  },
  {
    path: '/tool',
    component: Tool,
    children: [
      // {
      //   path: '*',
      //   name: '404',
      //   component: Error404,
      // },
      {
        path: '',
        name: 'Dashboard',
        component: Dashboard,
      },
      {
        path: 'cart',
        name: 'Cart',
        component: Cart,
      },
      {
        path: 'cart/checkout',
        name: 'Checkout',
        component: Checkout,
      },

      // Product
      {
        path: 'products',
        name: 'Products',
        component: Products,
      },
      {
        path: 'products/new',
        name: 'New Product',
        component: NewProduct,
      },
      {
        /* EditProducts not implemented yet */
        path: 'products/edit/:id',
        name: 'Edit Product',
        component: EditProduct,
        props: true,
      },
      {
        path: 'products/room/:id',
        name: 'Room',
        component: RoomDetail,
        props: true,
      },
      {
        path: 'products/desk/:id',
        name: 'Desk',
        component: DeskDetail,
        props: true,
      },

      // User
      {
        path: 'users',
        name: 'Users',
        component: UserList,
      },
      {
        path: 'users/:uid',
        name: 'User',
        component: UserDetail,
        props: true,
      },

      // Calendar
      {
        path: 'calendar',
        name: 'Calendar',
        component: Calendar,
      },

      // Orders
      {
        path: 'orders',
        name: 'Orders',
        component: OrderList,
      },
      {
        path: 'orders/:id',
        name: 'Order',
        component: OrderDetail,
        props: true,
      },
      // {
      //   path: 'orders/*',
      //   name: 'Orders 404',
      //   component: OrderError404,
      // },

      // Scheduler
      {
        path: 'scheduler',
        name: 'Scheduler',
        component: Scheduler,
      },

      // Settings
      {
        path: 'settings',
        name: 'Settings',
        component: Settings,
      },
    ],
  },
];

const router = new VueRouter({
  mode: 'history',
  routes,
});

export default router;
