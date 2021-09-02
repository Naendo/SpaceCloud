<template>
  <!--role="navigation" -->
  <nav class="NavbarTop">
    <router-link :to="{ name: 'Dashboard' }" class="navbar-brand">
      <img
        id="spacecloud-logo-main"
        src="@/assets/logo.png"
        alt="SpaceCloud Logo"
      />
    </router-link>
    <!-- <div id="active-page">{{ $route.name }}</div> -->
    <router-link :to="{ name: 'Cart' }" id="cart">
      <img
        src="@/assets/icons/cart.svg"
        alt="Cart"
        class="is-32x32"
      />
    </router-link>
    <MenuTop />
  </nav>
</template>
<script>
import MenuTop from '@/components/navs/MenuTop';

export default {
  name: 'NavbarTop',
  components: { MenuTop },
  data() {
    return {
      activePage: 'Test',
    };
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.NavbarTop {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  flex-wrap: nowrap;

  z-index: 2000;

  height: 100px;
  width: 100vw;
  position: fixed;

  padding: 10px;
  padding-right: 100px;
  background-color: transparent;
}

.NavbarTop > *:hover {
  background-color: #e7e6eb;
}

.navbar-brand {
  margin-top: 6px;
  padding: 10px;

  justify-self: flex-start;
  margin-right: auto;
}

.navbar-brand:hover {
  background-color: transparent;
}

.navbar-brand img {
  width: auto;
  max-height: 50px;
}

#cart {
  display: block;
  height: 50px;
  margin-right: 20px;
  padding: 7px 10px 0;
}

.router-link-active img:not(#spacecloud-logo-main) {
  filter: $primary-filter;
}

// #active-page {
//   color: $primary;
//   font-size: 1.8em;
//   font-weight: bold;
//   margin-left: 50px;
//   letter-spacing: 4px;
// }
</style>
