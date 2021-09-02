<template>
  <div class="products">
    <div class="wrapper">
      <div class="content-wrapper-with-subnav">
        <SubNavSide
          v-if="$store.getters['auth/isAdmin']"
          :topTitle="products.length"
          :topSubtitle="activeTab"
          :items="['Rooms', 'Desks']"
          :button="'Add new Product'"
          v-on:item-clicked="subNavItemClicked($event)"
          v-on:button-clicked="$router.push({ name: 'New Product' })"
        /><SubNavSide
          v-else
          :topTitle="products.length"
          :topSubtitle="activeTab"
          :items="['Rooms', 'Desks']"
          v-on:item-clicked="subNavItemClicked($event)"
          v-on:button-clicked="$router.push({ name: 'New Product' })"
        />
        <div
          class="product-list-container scrollable"
          v-if="activeTab === 'Rooms'"
        >
          <RoomTile
            :room="room"
            v-for="(room, productId) in products"
            :key="productId"
            v-on:remove-room="removeProduct"
          />
          <p v-if="products.length == 0" class="table-alt">
            No products could be found
          </p>
        </div>
        <div
          class="product-list-container scrollable"
          v-if="activeTab === 'Desks'"
          :key="updateDesksKey"
        >
          <DeskTile
            :desk="desk"
            v-for="(desk, productId) in products"
            :key="productId"
          />
          <p v-if="products.length == 0" class="table-alt">
            No products could be found
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import RoomTile from '@/components/products/RoomTile';
import DeskTile from '@/components/products/DeskTile';
import SubNavSide from '@/components/navs/SubNavSide';

export default {
  name: 'Products',
  components: { RoomTile, DeskTile, SubNavSide },
  data() {
    return {
      activeTab: 'Rooms',
      updateDesksKey: 0,
    };
  },
  computed: {
    products() {
      if (this.activeTab === 'Rooms') return this.rooms;
      if (this.activeTab === 'Desks') return this.desks;
      return [];
    },
    rooms() {
      return this.$store.state.rooms;
    },
    desks() {
      return this.$store.state.desks;
    },
  },
  created() {
    this.$store.dispatch('fetchRooms');
    this.$store.dispatch('fetchDesks');
  },
  methods: {
    subNavItemClicked(item) {
      if (item === 'Desks') {
        this.activeTab = 'Desks';
      }
      if (item === 'Rooms') {
        this.activeTab = 'Rooms';
        // this.$store.dispatch('fetchRooms');
      }
    },
    removeProduct(productId) {
      const index = this.products
        .map(x => x.productId)
        .indexOf(productId);
      this.$delete(this.products, index);
    },
  },
};
</script>
<style scoped lang="scss">
.content-header {
  height: 2.5em;
  width: 100%;
}

.product-list-container {
  // min-width: 800px;
  width: 100%;
  // max-height: calc(100% - 2.5em * 2 - 50px);
}

.prod-cat-list-container {
  height: 100%;
  width: 100%;
}

.addProduct {
  float: right;
}
</style>
