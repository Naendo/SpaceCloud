<template>
  <div class="Cart">
    <div class="wrapper">
      <div id="cart-items-wrapper">
        <TitleH2 text="Shopping Cart" />
        <div class="full-height-table-container scrollable">
          <table
            v-if="cartItems.length > 0"
            class="table-cart-items sticky-header table is-hoverable is-fullwidth is-striped"
          >
            <colgroup>
              <col id="colProduct" />
              <col id="colQuantity" />
              <col id="colPrice" />
              <col id="colTotal" />
            </colgroup>
            <tbody>
              <tr id="table-headers">
                <th class="colProduct">Product Name</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Total</th>
              </tr>
              <!-- @click="clickItem(evt)" -->
              <tr v-for="(item, i) in cartItems" :key="i">
                <td class="colProduct">{{ item.name }}</td>
                <td>
                  <div class="field item-quantity">
                    <button
                      class="button is-rounded"
                      @click="setQuantity(i, item.quantity - 1)"
                    >
                      -
                    </button>
                    <div class="control">
                      <input
                        @change="setQuantity(i)"
                        name="quantity"
                        class="input"
                        type="number"
                        v-model="item.quantity"
                        min="0"
                        max="10"
                        placeholder="1"
                      />
                    </div>
                    <button
                      class="button is-rounded"
                      @click="setQuantity(i, item.quantity + 1)"
                    >
                      +
                    </button>
                  </div>
                </td>
                <!-- Note: +num.toFixed(2) removes unnecessary
                     zeros at the end (10.10 -> 10.1) -->
                <td>
                  {{ toPrice(item.price) }}
                </td>
                <td class="priceTotal has-text-success">
                  {{ toPrice(item.price * item.quantity) }}
                </td>
              </tr>
            </tbody>
          </table>
          <p v-else class="table-alt">Your Cart is still empty</p>
        </div>
      </div>
      <div id="cart-summary-wrapper">
        <div id="cart-summary-content-wrapper">
          <TitleH2 text="Order Summary" />
          <div id="cart-summary-content">
            <div>
              <span class="bold">{{
                cartItems.length + ' items total'
              }}</span>
              <span class="priceTotal has-text-success">{{
                toPrice(totalPrice)
              }}</span>
            </div>
            <div class="flex-dir-col">
              <p>
                Do your want to use your SpaceCoins?
              </p>
              <p>
                You have
                <span class="bold has-text-danger">##</span> left.
              </p>
            </div>
            <form class="form-spacecoins flex-dir-col">
              <div class="control">
                <input
                  @change="
                    if (discountCoins > totalPrice)
                      discountCoins = totalPrice;
                  "
                  name="discountCoins"
                  class="input"
                  type="number"
                  min="0"
                  v-model="discountCoins"
                  placeholder="00 SpaceCoins"
                  v-validate="'numeric'"
                />
              </div>
              <div class="control">
                <button
                  type="submit"
                  class="button is-primary is-rounded"
                  :disabled="parseInt(discountCoins, 10) === 0"
                >
                  Use points
                </button>
              </div>
            </form>
            <div>
              <span>Discount</span
              ><span class="bold has-text-danger">{{
                '- ' + toPrice(discountCoins)
              }}</span>
            </div>
            <div></div>
            <div>
              <span class="bold">Your new total is</span
              ><span class="has-text-success is-size-3">{{
                toPrice(discountedPrice)
              }}</span>
            </div>
            <div>
              <div class="control is-fullwidth">
                <router-link
                  :to="{ name: 'Checkout' }"
                  tag="button"
                  type="submit"
                  class="button is-primary is-rounded is-fullwidth"
                  :disabled="cartItems.length === 0"
                >
                  Check out
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import TitleH2 from '@/components/base/TitleH2';

export default {
  name: 'Cart',
  components: { TitleH2 },
  data() {
    return {
      discountCoins: 0,
    };
  },
  computed: {
    cartItems() {
      const { cart, rooms, desks } = this.$store.state;

      let room;
      let desk;
      const cartItems = [];
      // eslint-disable-next-line no-plusplus
      for (let i = 0; i < cart.length; i += 1) {
        room = undefined;
        desk = undefined;

        room = rooms.find(r => r.productId === cart[i].productId);
        if (room)
          cartItems.push({
            name: room.name,
            productId: room.productId,
            quantity: cart[i].quantity,
            price: room.price,
          });
        else {
          desk = desks.find(d => d.productId === cart[i].productId);
          if (desk)
            cartItems.push({
              name: desk.name,
              productId: desk.productId,
              quantity: cart[i].quantity,
              price: desk.price,
            });
        }
      }

      return cartItems;
    },
    totalPrice() {
      return this.cartItems.reduce(
        (a, b) => a + b.price * b.quantity,
        0,
      );
    },
    discountedPrice() {
      return this.discountCoins < this.totalPrice
        ? this.totalPrice - this.discountCoins
        : 0;
    },
    //   this.cartItems = [
    //     { name: 'Desk 1', quantity: 1, price: 10 },
    //     { name: 'Desk 2', quantity: 2, price: 20 },
    //     { name: 'Desk 3', quantity: 3, price: 30 },
    //     { name: 'Room 1', quantity: 1, price: 10 },
    //     { name: 'Room 2', quantity: 2, price: 20 },
    //     { name: 'Room 3', quantity: 3, price: 30 },
    //   ];
  },
  methods: {
    setQuantity(i, q = this.cartItems[i].quantity) {
      if (q.toString() === '0') this.removeCartItem(i);
      else if (q > 10) q = 10;
      this.$store.dispatch('cartItemSetQuantity', {
        productId: this.cartItems[i].productId,
        quantity: q,
      });
      if (this.discountCoins > this.totalPrice)
        this.discountCoins = this.totalPrice;
    },
    removeCartItem(i) {
      this.$swal({
        title: 'Are you sure?',
        text: `${this.cartItems[i].name} will be removed from your Cart`,
        icon: 'question',
        confirmButtonColor: '#f53f27',
        showCancelButton: true,
        confirmButtonText: 'Remove',
      }).then(res => {
        if (res.value)
          this.$store.dispatch(
            'removeCartItem',
            this.cartItems[i].productId,
          );
        else this.setQuantity(i, 1);
      });
    },
    toPrice(num) {
      return `${parseInt(num, 10).toFixed(2)} €`;
    },
  },
};
</script>
<style scoped lang="scss">
.wrapper {
  display: flex;
  flex-wrap: nowrap;
  justify-content: stretch;
}

.wrapper > div {
  height: 100%;
  flex-grow: 1;
  padding: 30px 0;
}

// Cart Items
#cart-items-wrapper {
  width: 50%;
  // background-color: rgb(200, 233, 200);
}

.full-height-table-container {
  margin-top: 50px;
}

.table-cart-items > * {
  text-align: center !important;
}

.table-cart-items th:first-of-type {
  text-align: left !important;
}

.table-cart-items th:last-of-type {
  text-align: right !important;
}

td {
  height: 4em !important;
  padding: 0;
  vertical-align: middle !important;
}

col {
  min-width: 100px;
}

.colProduct {
  font-weight: bold;
  text-align: left !important;
}

#colTotal {
  width: 200px;
}

.priceTotal {
  text-align: right !important;
  font-weight: bold;
  font-size: 1.2em;
  letter-spacing: 2px;
}

.item-quantity {
  display: flex;
  flex-wrap: nowrap;
  justify-content: center !important;
  align-items: center;
}

.item-quantity .control {
  width: 70px;
  margin: 0 8px;
}

.item-quantity input {
  text-align: center;
}

.item-quantity button {
  width: 36px;
  background: transparent;
  padding: 0.7em !important;
  border: none;
}

.item-quantity button:hover {
  background-color: #fafafa;
}

// Order Summary
#cart-summary-wrapper {
  margin-left: 40px;
}

#cart-summary-content-wrapper {
  height: 100%;
  width: 100%;
  border-left: 2px solid black;

  padding: 0 20px;
}

#cart-summary-content {
  height: 100%;
  width: 100%;
  display: grid;
  grid-template-rows: repeat(8, 60px);
  grid-template-columns: 1fr;
  grid-gap: 20px;
}

#cart-summary-content > * {
  display: flex;
  justify-content: space-between;
  align-items: center;
  // background-color: rgb(200, 233, 200);
}

.flex-dir-col {
  flex-direction: column;
  justify-content: space-around !important;
  align-items: flex-start !important;
}

.form-spacecoins {
  grid-row: 3 / 5;
}

.is-fullwidth {
  width: 100%;
}

// Hide arrows on number input
// Chrome, Safari, Edge, Opera
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

// Firefox
input[type='number'] {
  -moz-appearance: textfield;
}
</style>
