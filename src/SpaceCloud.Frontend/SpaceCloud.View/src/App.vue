<template>
  <div id="app">
    <transition name="trans-login" mode="out-in">
      <router-view class="" />
    </transition>
  </div>
</template>

<script>
export default {
  name: 'app',
  components: {},
  data() {
    return {};
  },
  created() {
    this.setDocTitle();
  },
  computed: {},
  watch: {
    user() {
      if (this.$store.state.auth.user == null) {
        this.$store.dispatch('auth/logout');
      }
    },
    $route(to /* , from */) {
      this.setDocTitle(to);
      // console.log(`routing from ${from.name} to ${to.name}`);
    },
  },
  methods: {
    setDocTitle(route) {
      if (!route || route.name === 'Dashboard' || !route.name)
        document.title = 'SpaceCloud';
      else {
        document.title = `SpaceCloud | ${route.name}`;
        if (route.params.id) document.title += ` ${route.params.id}`;
      }
    },
  },
};
</script>
<style lang="scss">
#app {
  height: 100vh;
  margin: 0;

  background-color: #f6f5fa;

  font-family: 'Roboto', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

// Login Transition
.trans-login-enter-active,
.trans-login-leave-active {
  transition: transform 100ms ease-in, opacity 80ms ease-out;
}

.trans-login-enter {
  opacity: 0;
}
.trans-login-leave-to {
  transform: scale(0.8);
  opacity: 0;
}
</style>
