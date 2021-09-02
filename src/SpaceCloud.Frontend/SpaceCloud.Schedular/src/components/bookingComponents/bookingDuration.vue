<template>
  <div class="item-wrapper">
    <h3 class="sub-headline">Duration</h3>
    <div class="duration-container">
      <div class="duration-inner-container">
      <span class="time-container">
        <span>{{ viewModel.from }}</span>
        <span>-</span>
        <span>{{ viewModel.to }}</span>
      </span>

        <span class="time-duration">15 min</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">

import {defineComponent} from 'vue';
import {ItemViewModel} from "../../viewModels/viewModel";


export default defineComponent({
  name: 'bookingDuration',
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
  width: 90%;
  margin: 0 auto;
  font-family: "Roboto Thin";
  color: white;
  height: 100%;
}

.duration-container {
  width: calc(100%);
  height: 3em;
  background-color: $secondary-bg;
  border-radius: $corner-radius;

  .duration-inner-container {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 90%;
    height: 100%;
    margin: 0 auto;

    .time-container {
      width: 25%;
      height: 100%;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
}

</style>