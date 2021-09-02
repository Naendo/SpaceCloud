<template>
  <div class="item-wrapper">
    <h3 class="sub-headline">Event Title (optional)</h3>
    <input v-bind:value="viewModel.usage" type="text" placeholder="Quick Reservation">
  </div>
</template>

<script lang="ts">

import {defineComponent} from 'vue';
import {ItemViewModel} from "../../viewModels/viewModel";


export default defineComponent({
  name: 'bookingUsage',
  props: {
    viewModel: {
      type: [ItemViewModel],
      required: true,
    }
  }
})
</script>

<style lang="scss" scoped>
@import "src/assets/style/global";

.item-wrapper {
  font-family: "Roboto Thin";
  height: 100%;
  width: 90%;
  margin: 0 auto;
  color: white;

  input[type=text] {
    width: calc(100% - 1em);
    height: 2.5em;
    border-radius: $corner-radius;
    border: 0.8px solid white;
    background-color: $main-bg;
    padding-left: 1em;
    font-size: 1em;
  }

}

</style>