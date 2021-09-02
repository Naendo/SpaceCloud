<template>
  <div class="NavbarSide">
    <aside class="menu">
      <ul class="menu-list">
        <li>
          <router-link
            :to="{ name: 'Dashboard' }"
            id="router-link-dashboard"
          >
            <img src="@/assets/icons/dashboard.svg" alt="Dashboard" />
            <span>Dashboard</span>
          </router-link>
        </li>
        <li>
          <router-link :to="{ name: 'Products' }">
            <img
              src="@/assets/icons/box-outline.svg"
              alt="Products"
            />
            <span>Products</span>
          </router-link>
        </li>
        <!--Some nav items are only visible to admins or owners-->
        <li v-if="$store.getters['auth/isAdmin']" class="admin">
          <router-link :to="{ name: 'Users' }">
            <img
              src="@/assets/icons/person-outline.svg"
              alt="Users"
            />
            <span>Users</span>
          </router-link>
        </li>
        <li class="admin">
          <router-link :to="{ name: 'Calendar' }">
            <img
              src="@/assets/icons/calendar-outline.svg"
              alt="Calendar-Icon"
            />
            <span>Calendar</span>
          </router-link>
        </li>
        <li>
          <router-link :to="{ name: 'Orders' }">
            <img
              src="@/assets/icons/documents-outline.svg"
              alt="Orders"
            />
            <span>Orders</span>
          </router-link>
        </li>
        <li v-if="$store.getters['auth/isAdmin']" class="admin">
          <router-link :to="{ name: 'Scheduler' }">
            <img src="@/assets/icons/cog-outline.svg" alt="Users" />
            <span>Scheduler</span>
          </router-link>
        </li>
        <li>
          <router-link :to="{ name: 'Settings' }">
            <img
              src="@/assets/icons/cog-outline.svg"
              alt="Settings"
            />
            <span>Settings</span>
          </router-link>
        </li>
        <li id="logout">
          <button
            class="button is-outlined is-rounded is-medium"
            v-on:click="$store.dispatch('auth/logout')"
          >
            Logout
          </button>
        </li>
        <!--<li id="account" class="is-flex">
          <router-link to="/auth">
            <img src="@/assets/icons/person-outline.svg" alt="Account" />
          </router-link>
        </li>-->
      </ul>
    </aside>
  </div>
</template>
<script>
export default {
  name: 'NavbarSide',
  components: {},
  data() {
    return {};
  },
};
</script>
<style scoped lang="scss">
li > * {
  display: flex !important;
  align-items: center;
}

.NavbarSide {
  padding: 0 !important;
  width: 250px !important;
}

.menu {
  height: 100%;
  width: 250px !important;
  margin-top: 100px;
  padding: 10px;
  border-radius: 0 50px 0 0;
  font-size: 25px;
  background-color: white;
}

.router-link-exact-active span,
.router-link-active:not(#router-link-dashboard) span {
  font-weight: bold;
  color: $primary;
}

.router-link-exact-active img,
.router-link-active:not(#router-link-dashboard) img {
  filter: $primary-filter;
}

a {
  margin: 15px 0;
  padding: 15px !important;
}

a:hover {
  text-decoration: none;
}

li#account,
li#logout {
  position: fixed;
  bottom: 20px;

  justify-content: center;
}

li#account * {
  margin: 0;
  width: 100%;
}

li#logout * {
  left: 55px;
}

img,
svg {
  height: 34px;
  width: 34px;

  margin-right: 30px;

  fill: none;
  stroke: black;
}
</style>
