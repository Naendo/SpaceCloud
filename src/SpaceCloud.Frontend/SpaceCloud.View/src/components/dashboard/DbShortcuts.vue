<template>
  <div class="DbShortcuts">
    <div class="heading-wrapper">
      <h3 class="title is-3">Shortcuts</h3>
    </div>
    <div class="shortcut-items">
      <router-link :to="{ name: 'Products' }" class="flex-center"
        >Products</router-link
      >
      <router-link :to="{ name: 'Cart' }" class="flex-center"
        >Cart</router-link
      >
      <router-link :to="{ name: 'Calendar' }" class="flex-center"
        >Calendar</router-link
      >
      <router-link :to="{ name: 'Orders' }" class="flex-center"
        >Orders</router-link
      >
    </div>
  </div>
</template>
<script>
export default {
  name: 'DbShortcuts',
  components: {},
  data() {
    return {};
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.DbShortcuts {
  // width: 330px;

  padding: 25px;
}

.heading-wrapper {
  width: 100%;
  text-align: center;
  margin-top: 30px;
}

.shortcut-items {
  display: flex;
  justify-content: space-between;
  align-content: space-between;
  flex-wrap: wrap;

  height: 75%;
  width: 100%;
  margin-top: 2.5%;
  padding: 15px;
}

.shortcut-items > a {
  cursor: pointer;

  width: 45%;
  height: 45%;

  font-size: 1.5em;
  color: $primary;
  border-radius: 5px;
  border: 1px solid $primary;
}

.shortcut-items > a:hover {
  background-color: whitesmoke;
}
</style>
