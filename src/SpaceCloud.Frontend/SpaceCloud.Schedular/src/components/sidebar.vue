<template>
  <div class="sidebar-container">
    <div class="sidebar-toggle" @click="test">
      <span class="burger-bar burger-bar--1"></span>
      <span class="burger-bar burger-bar--2"></span>
      <span class="burger-bar burger-bar--3"></span>
    </div>
    <div class="sidebar-content">


    </div>
  </div>
</template>
<script lang="ts">

import {defineComponent} from 'vue';

export default defineComponent({
  name: 'sidebar',
  setup(props, {emit}) {

    function test() {
      emit('myevent', null);
    }

    return {
      test,
      props
    }
  }

});
</script>
<style scoped lang="scss">

.sidebar-container {
  width: 100%;
  height: 100%;
}

.sidebar-toggle {
  position: relative;
  height: 42px;
  width: 42px;
  cursor: pointer;
  background-color: #18A4E0;
  border-radius: 50%;
  margin: 0 auto;


  .burger-bar {
    background-color: #121212;
    position: absolute;
    top: 50%;
    right: 9px;
    left: 9px;
    height: 2px;
    width: auto;
    margin-top: -1px;
    transition: transform 0.6s cubic-bezier(0.165, 0.84, 0.44, 1),
    opacity 0.3s cubic-bezier(0.165, 0.84, 0.44, 1),
    background-color 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
  }

  .burger-bar--1 {
    -webkit-transform: translateY(-6px);
    transform: translateY(-6px);
  }

  .burger-bar--2 {
    transform-origin: 100% 50%;
    transform: scaleX(0.8);
  }


  .burger-bar--3 {
    transform: translateY(6px);
  }
}

.sidebar-toggle:hover .burger-bar--2 {
    transform: scaleX(1);
}
</style>
