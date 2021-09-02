<template>
  <div class="booking-view">
    <header id="booking-header">{{ title}}<img src="../assets/spacecloud_logo.svg" alt="spacecloud logo"></header>
    <section id="booking-container">
      <booking-usage :viewModel="itemViewModel"></booking-usage>
      <booking-item :viewModel="itemViewModel"/>
      <booking-duration :viewModel="itemViewModel"></booking-duration>
    </section>
    <footer id="booking-footer">
      <div id="booking-inner-footer">
        <div class="booking-caption-container">
          <div class="booking-footer-caption">
            <span>Your Balance:</span>
            <span>c 40</span>
          </div>
          <div class="booking-footer-caption">
            <span>Balance after Purchase:</span>
            <span>c 32</span>
          </div>
        </div>
        <h2 id="booking-order-total">
          <div>
            <span>Order Total:</span>
            <span>c 24</span>
          </div>
        </h2>
        <button id="booking-footer-submit">Checkout Order</button>
      </div>
    </footer>
  </div>
</template>

<script lang="ts">

import {defineComponent, reactive, ref} from 'vue';
import BookingItem from '../components/bookingComponents/bookingItem.vue'
import BookingUsage from '../components/bookingComponents/bookingUsage.vue'
import BookingDuration from '../components/bookingComponents/bookingDuration.vue'
import {ItemViewModel} from '../viewModels/viewModel'


export default defineComponent({
  name: "booking",
  components: {
    'bookingItem': BookingItem,
    'bookingUsage': BookingUsage,
    'bookingDuration': BookingDuration,
  },
  props: {
    query: {
      type: String,
      required: true,
    }
  },

  setup() {
    const itemViewModel = reactive<ItemViewModel>(new ItemViewModel());
    let title = ref<string>();

    return{
      itemViewModel,
      title
    }
  }
});

</script>

<style lang="scss" scoped>
@import "src/assets/style/global";


.booking-view {
  color: white;
  background-color: $main-bg;
  height: 100vh;
}


header#booking-header {
  position: absolute;
  top: 0;
  background-color: $secondary-bg;
  width: 100%;
  height: 10vh;

  img {
    width: 250px;
    height: 100%;
    object-fit: cover;
  }
}

#booking-container {
  height: 50vh - 15vh;
  padding-top: 15vh;
  background-color: $main-bg;
  display: grid;
  grid-template-rows: 10vh 1fr 10vh;
  grid-gap: 30px;
}

footer#booking-footer {
  font-family: "Roboto Thin";
  position: fixed;
  bottom: 0;
  height: 25%;
  width: 100%;
  border-top-left-radius: $corner-radius;
  border-top-right-radius: $corner-radius;
  background-color: $secondary-bg;
  display: flex;
  align-items: center;
  justify-content: center;

  #booking-inner-footer {
    width: 90%;
    height: 80%;
    border: none;
    text-decoration: none;
    display: grid;
    grid-template-rows:2fr 1fr auto;
    grid-gap: 2px;

    .booking-caption-container {
      height: 100%;
      width: 100%;
      font-size: 0.8em;

      .booking-footer-caption {
        display: flex;
        justify-content: space-between;
        padding: 0 1em;
        font-size: 1.4em;
        margin-bottom: 5px;
      }
    }

    #booking-order-total {
      padding: 0 1em;

      > div {
        display: flex;
        justify-content: space-between;
        width: 100%;
        height: 100%;
      }
    }

    #booking-order-total::after {
      width: 100%;
      height: 1px;
      background-color: white;
      display: block;
      content: ' ';
    }
  ;

    #booking-footer-submit {
      width: 100%;
      border-radius: $corner-radius;
      height: 3.5em;
      background-color: $contrast;
    }
  }
}

</style>