<template>
  <div class="tool columns">
    <NavbarTop />
    <NavbarSide class="column is-one-fifth" />
    <div
      v-if="stateLoaded"
      id="content-container"
      class="column is-centered"
    >
      <transition name="trans-tool" mode="out-in">
        <router-view
          :key="$route.fullPath"
          class="router-view-content column"
        />
      </transition>
    </div>
  </div>
</template>

<script>
import NavbarTop from '@/components/navs/NavbarTop';
import NavbarSide from '@/components/navs/NavbarSide';

export default {
  name: 'Home',
  components: {
    NavbarTop,
    NavbarSide,
  },
  data() {
    return {};
  },
  computed: {
    // when page is refreshed, content should be loaded once state is restored
    stateLoaded() {
      return this.$store.state.auth.jwt.token !== undefined;
    },
  },
  created() {
    // check if state needs to be refreshed
    if (
      this.stateLoaded === false &&
      localStorage.getItem('loggedIn') === 'true'
    )
      this.$store.dispatch('auth/silentRefresh');
  },
};
</script>
<style scoped lang="scss">
#content-container {
  margin: 100px 70px 40px 70px;
  padding: 0;
}

.tool {
  margin: 0;
  padding: 0;
  height: 100vh;
}

.tool,
.tool > div {
  overflow: hidden;
}

.router-view-content {
  height: 100%;
  width: 100%;

  padding: 0 !important;
}

// Tool Transition
.trans-tool-enter-active,
.trans-tool-leave-active {
  transition: opacity 80ms ease-in-out, transform 80ms ease-in-out;
}

.trans-tool-enter {
  opacity: 0;
  transform: translateY(10px);
}

.trans-tool-leave-to {
  opacity: 0;
}
</style>
