<template>
  <div class="OrderDetailBookingTile">
    <p class="big-text bold">{{ booking.productName }}</p>
    <p>{{ booking.usage }}</p>
    <p>{{ booking.price + ',- €' }}</p>
  </div>
</template>
<script>
export default {
  name: 'OrderDetailBookingTile',
  components: {},
  data() {
    return {};
  },
  props: ['booking'],
};
</script>
<style scoped lang="scss">
.OrderDetailBookingTile {
  width: 100%;
  margin: 25px 10px;
  padding: 15px;
  background-color: whitesmoke;

  border-radius: 15px;
}

.OrderDetailBookingTile p {
  margin: 0 5px;
}

.big-text {
  font-size: 1.2em;
}
</style>
