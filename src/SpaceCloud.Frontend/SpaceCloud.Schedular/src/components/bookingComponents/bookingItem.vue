<template>
  <div class="item-wrapper">
    <h3 class="sub-headline">Checkout Item</h3>
    <div class="item-container">
      <div class="item-inner-container">
        <div class="item-image">
          <img :src="viewModel.ImageUri" alt="product image">
        </div>
        <div class="item-content-container">
          <h4 class="item-content-headline">{{ viewModel.title }}</h4>
          <div class="item-content-description">{{ viewModel.coins }} coins / hour</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">

import {defineComponent} from 'vue';
import {ItemViewModel} from "../../viewModels/viewModel";


export default defineComponent({

  name: 'bookingItem',
  props: {
    viewModel: {
      type: [ItemViewModel],
      required: true,
    },
  },

  setup() {


    return {}
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

}

.item-container {
  background-color: $secondary-bg;
  border-radius: $corner-radius;
  height: 25vh;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;


  .item-inner-container {
    display: grid;
    grid-template-columns: 40% 1fr;
    grid-template-rows:100%;
    width: 95%;
    height: 90%;


    .item-image {
      margin: 0 auto;
      width: 100%;
      height: 100%;


      img {
        border-radius: $corner-radius;
        object-fit: cover;
        width: 100%;
        height: 100%;
      }
    }

    .item-content-container {
      text-align: left;
      height: 100%;
      width: 80%;
      margin: 0 auto;
      display: flex;
      flex-direction: column;
      justify-content: center;

      .item-content-headline {
        font-size: 1.4em;
        margin: 0;

      }

      .item-content-description {
        font-family: "Roboto Thin";
        font-weight: 100;
        font-size: 1em;
      }

    }
  }
}

@media only screen and (max-width: 600px) {
  .item-container{
    height: 15vh;
  }
}


</style>