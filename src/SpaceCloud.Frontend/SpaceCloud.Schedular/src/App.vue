<template>
  <router-view />
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "app",
});
</script>

<style>
body {
  box-sizing: border-box;
  height: 100vh;
  padding: 0;
  margin: 0;
  color: white;
  position: absolute;
  overflow: hidden;
  background-color: #1f1f1f;
}

#app {
  height: 100vh;
  width: 100vw;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: black;
}

/*Login Transition*/

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
