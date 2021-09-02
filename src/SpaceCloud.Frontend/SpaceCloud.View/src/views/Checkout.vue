<template>
  <div class="Checkout">
    <div class="wrapper scrollable">
      <TitleH2 text="Please select your payment option" />
      <div class="field">
        <div class="control">
          <label class="label">Payment Option</label>
          <div class="select">
            <select v-model="paymentOption">
              <option value="PayPal">PayPal</option>
              <option value="Rechnung">Rechnung</option>
            </select>
          </div>
        </div>
      </div>

      <form
        v-if="paymentOption === 'Rechnung'"
        id="contact-info-form"
      >
        <div class="field is-grouped">
          <div class="control is-expanded">
            <label class="label">First Name</label>
            <input
              v-model="contactInfo.firstName"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
          <div class="control is-expanded">
            <label class="label">Last Name</label>
            <input
              v-model="contactInfo.lastName"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
        </div>

        <div class="field is-grouped">
          <div class="control is-expanded">
            <label class="label">Street</label>
            <input
              v-model="contactInfo.street"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
          <div class="control">
            <label class="label">Nr.</label>
            <input
              v-model="contactInfo.streetNr"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
        </div>

        <div class="field is-grouped">
          <div class="control">
            <label class="label">Postal Code</label>
            <input
              v-model="contactInfo.postalCode"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
          <div class="control is-expanded">
            <label class="label">City</label>
            <input
              v-model="contactInfo.city"
              class="input"
              type="text"
              placeholder="Text input"
            />
          </div>
        </div>
        <div class="field">
          <label class="label">Email</label>
          <div class="control">
            <input
              v-model="contactInfo.email"
              class="input"
              type="email"
              placeholder="Email input"
            />
            <span class="icon is-small is-left">
              <i class="fas fa-envelope"></i>
            </span>
            <span class="icon is-small is-right">
              <i class="fas fa-exclamation-triangle"></i>
            </span>
          </div>
          <!-- <p class="help is-danger">This email is invalid</p> -->
        </div>

        <div class="field">
          <div class="control">
            <button
              @click="formCheckout"
              type="button"
              class="button is-primary is-rounded is-fullwidth"
            >
              Complete Checkout
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import TitleH2 from '@/components/base/TitleH2';
import api from '@/services/api';

export default {
  name: 'Checkout',
  components: { TitleH2 },
  data() {
    return {
      paymentOption: 'Rechnung',
      contactInfo: {
        firstName: '',
        lastName: '',
        street: 'Musterstraße',
        streetNr: '36',
        postalCode: '6900',
        city: 'Bregenz',
        email: '',
      },
    };
  },
  created() {
    api.getCurrentUser().then(response => {
      this.contactInfo.firstName = response.data.firstName;
      this.contactInfo.lastName = response.data.lastName;
      this.contactInfo.email = response.data.mail;
    });
  },
  methods: {
    formCheckout(evt) {
      if (evt) evt.preventDefault();

      // this.$swal({
      //   title: 'Order complete (demo)',
      //   html:
      //     "Your order has been processed.<br />You will soon receive an email including your invoice and order confirmation.<br />(no you won't lol)",
      //   icon: 'success',
      //   confirmButtonColor: '#1b53a8',
      // }).then(res => {
      //   if (res) this.$router.push({ name: 'Dashboard' });
      // });

      // html: `<p class="swal-bigfont-temp"><span class="bold">${
      //           this.room.name
      //         }</span></p>
      //             <p class="swal-bigfont-temp">Usage: <span class="bold">${
      //               this.order.usage
      //             }</span></p>
      //                    <p class="swal-bigfont-temp">Date: <span class="bold">${this.formatDate(
      //                      new Date(this.order.startTime),
      //                      true,
      //                      false,
      //                    )}</span></p>
      //                    <p class="swal-bigfont-temp">Time: <span class="bold">${this.formatDate(
      //                      new Date(this.order.startTime),
      //                      false,
      //                    )} - ${this.formatDate(
      //           new Date(this.order.endTime),
      //           false,
      //         )}</span></p>`,
      this.$swal({
        title: 'Do you want to place this order?',
        html: `Your order includes ${this.$store.state.cart.length} item(s)`,
        icon: 'question',
        confirmButtonColor: '#1b53a8',
        showCancelButton: true,
      }).then(res => {
        if (res.value) this.submit();
      });
    },
    submit() {
      const orderItems = [];

      this.$store.state.cart.forEach(item => {
        orderItems.push({
          usage: item.usage,
          startTime: item.startTime,
          endTime: item.endTime,
          productId: item.productId,
        });
      });

      api
        .createOrder(orderItems)
        .then(() => {
          this.$store.dispatch('clearCart');
          this.$swal({
            title: 'Order created',
            text:
              'Your Order has been created. You will receive an email with your invoice soon.',
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          }).then(res => {
            if (res.value) this.$router.push({ name: 'Dashboard' });
          });
        })
        .catch(error => {
          console.log('An error ocurred: ', error);
        });
    },
  },
};
</script>
<style scoped lang="scss">
.wrapper > * {
  padding: 15px 25%;
}
</style>
