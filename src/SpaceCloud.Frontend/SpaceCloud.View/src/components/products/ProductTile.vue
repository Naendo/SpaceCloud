<template>
  <div @click="bookNow()" class="ProductTile">
    <div class="productImgContainer">
      <img
        v-if="showImg"
        :class="{ imgAltBgColor: !showImg }"
        :src="product.imageUri"
        @error="showImg = false"
        alt="Product Preview"
      />
      <p v-else>Image not found</p>
    </div>
    <div class="productInfo">
      <h3 class="title is-3">{{ product.name }}</h3>
      <div class="productInfoText">
        <!--Interval Enum:
        NoTime = 0, Hour = 1, HalfDaily = 2, Day = 3, Month = 4, Yearly = 5-->
        <p>
          <span class="productPrice">{{ product.price }}€</span>
          <span>per Person/Hours</span>
        </p>
        <p class="capacity">Capacity: {{ product.capacity }}</p>
        <p class="pid">pid: {{ product.pid }}</p>
      </div>
      <button
        class="bookNow button is-primary is-rounded"
        v-on:click="bookNow()"
      >
        Book now
      </button>
    </div>
  </div>
</template>
<script>
export default {
  name: 'ProductTile',
  props: ['product'],
  components: {},
  data() {
    return { showImg: true };
  },
  methods: {
    // ROOM:
    bookNow() {
      this.$router.push({
        name: 'Room',
        params: { id: this.product.pid },
      });
    },
  },
};
</script>
<style scoped lang="scss">
.ProductTile {
  display: flex;
  height: 250px;
  width: 98%;
  margin: 30px 0;
  line-height: 30px;
  letter-spacing: 2px;
  background-color: #f9f9f9;
  border-radius: 20px;
}

.ProductTile:hover {
  background-color: $hover-background;
  cursor: pointer;
}

.ProductTile > div {
  height: 100%;
}

.ProductTile > .productInfo {
  width: 65%;
  padding: 20px;
  padding-left: 30px;
}

.imgAltBgColor {
  background-color: #eeeeee;
}

.productPrice {
  font-weight: 500;
  font-size: 1.5em;

  margin: 0 0.5em 0 0.3em;
}

button.bookNow {
  width: 150px;
  float: right;
}
</style>
