<template>
  <div class="DeskDetail">
    <div class="wrapper scrollable" ref="wrapper">
      <button
        v-on:click="bookNow"
        id="btn-addtocart"
        class="button is-rounded is-medium is-primary is-fullwidth"
      >
        Add to Cart
      </button>
      <div
        id="banner-wrapper"
        :style="{
          'background-image': 'url(' + desk.imageUri + ')',
        }"
      >
        <div
          id="banner-content"
          class="flex-center"
          v-if="showBannerContent"
        >
          {{ desk.name }}
        </div>
      </div>
      <div class="content-wrapper">
        <section id="section-general-info">
          <h1 class="title is-1">{{ desk.name }}</h1>
          <div class="content-row-wrapper">
            <span class="price">{{ desk.price + ' €' }}</span>
            <span class="text-small"
              >per {{ intervalTypes[desk.intervalType] }}</span
            >
            <div class="capacity">
              <img
                src="@/assets/icons/person-outline.svg"
                alt="Users"
              />6
            </div>
          </div>
          <div class="tags-container">
            <ProductTagList :tags="desk.tags" />
          </div>
          <p
            class="description"
            v-if="desk.description !== undefined"
          >
            {{ desk.description }}
          </p>
          <p class="description" v-else>No description available.</p>
        </section>
        <section id="order-details">
          <form id="order-details-form">
            <div class="field">
              <div class="control">
                <label for="usage" class="label"
                  >Why do you want to order this desk?</label
                >
                <textarea
                  rows="2"
                  id="usage"
                  name="usage"
                  class="textarea"
                  v-model="order.usage"
                  placeholder="Describe the purpose of your Event in a few words"
                  required
                  v-validate="'required'"
                />
              </div>
            </div>
            <label for="duration" class="label label-duration"
              >How long do you want to use this desk?</label
            >
            <div class="field input-duration has-addons">
              <div class="control">
                <input
                  name="quantity"
                  class="input"
                  type="number"
                  v-model="duration"
                  min="1"
                  max="11"
                  placeholder="1"
                />
              </div>
              <p class="control">
                <a class="button is-static">
                  {{ intervalTypes[desk.intervalType] + 's' }}
                </a>
              </p>
            </div>
            <!-- <div class="field is-grouped">
              <div class="control">
                <label for="startDate" class="label"
                  >Start Date:</label
                >
                <input
                  type="datetime-local"
                  id="start-time"
                  :value="order.startTime"
                  name="start-time"
                  :min="times.start.min"
                  :max="times.start.max"
                />
              </div>
              <div class="control">
                <label for="endDate" class="label">End Date:</label>
                <input
                  type="datetime-local"
                  id="end-time"
                  :value="order.endTime"
                  name="end-time"
                  :min="times.end.min"
                  :max="times.end.max"
                />
              </div>
            </div> -->
          </form>
        </section>
      </div>
      <!-- Confirmation Modal -->
      <div v-if="showConfirmation" class="modal is-active is-clipped">
        <div class="modal-background"></div>
        <div class="modal-card">
          <header class="modal-card-head">
            <p class="modal-card-title">
              Do you want to confirm your Order?
            </p>
            <button
              v-on:click="toggleConfirmation"
              class="delete"
              aria-label="close"
            ></button>
          </header>
          <div class="modal-card-body">
            <pre>{{ JSON.stringify(order, null, 2) }}</pre>
          </div>
          <footer class="modal-card-foot">
            <div class="is-pulled-right">
              <button
                v-on:click="submit"
                class="button is-primary"
                :disabled="loading"
              >
                Create Order
              </button>
              <button v-on:click="toggleConfirmation" class="button">
                Cancel
              </button>
            </div>
          </footer>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import api from '@/services/api';
import textFormatMixin from '@/mixins/TextFormat';
import ProductTagList from '@/components/products/ProductTagList';

export default {
  name: 'DeskDetail',
  components: { ProductTagList },
  data() {
    return {
      desk: {},
      order: {
        startTime: '',
        endTime: '',
        usage:
          'I am a Freelancer and I am looking for a space to work in Bregenz',
      },
      times: {
        start: {
          min: '',
          max: '',
        },
        end: {
          min: '',
          max: '',
        },
      },
      duration: 1,
      intervalTypes: ['Day', 'Month', 'Quartal', 'Year'],
      showConfirmation: false,
      message: '',
      loading: false,
      showBannerContent: false,
    };
  },
  props: ['id'],
  mixins: [textFormatMixin],
  created() {
    api.getDesk(this.id).then(
      response => {
        this.desk = response.data;
      },
      error => {
        console.log(error);
      },
    );
  },
  mounted() {
    this.$refs.wrapper.addEventListener('scroll', this.handleScroll);

    let d = this.roundUTCtoNextQuarter(this.getDateUTCNow());
    this.order.startTime = d;
    this.times.start.min = d;

    d = this.roundUTCtoNextQuarter(
      this.getDateUTC(new Date(new Date().getTime() + 15 * 60000)),
    );
    this.order.endTime = d;
    this.times.end.min = d;
  },
  beforeDestroy() {
    this.$refs.wrapper.removeEventListener(
      'scroll',
      this.handleScroll,
    );
  },
  methods: {
    dateNow() {
      return this.getDateUTCNow();
    },
    bookNow() {
      /*
        {
          "usage": "string",
          "startDate": "2020-12-06T20:17:24.709Z",
          "endDate": "2020-12-06T20:17:24.709Z",
          "productId": 0
        }
      */

      // Input Validation
      if (
        typeof this.order.startTime === 'undefined' ||
        typeof this.order.endTime === 'undefined'
      ) {
        this.$swal({
          title: 'Inpus not valid',
          text: 'Please provide valid times for your order',
          icon: 'warning',
          confirmButtonColor: '#1b53a8',
        });
      }
      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.$swal({
            title: 'Inputs not valid',
            text: 'Please state the reason of your Event',
            icon: 'warning',
            confirmButtonColor: '#1b53a8',
          });
        } else {
          this.$swal({
            title: 'Do you want to place this order?',
            html: `<p class="swal-bigfont-temp"><span class="bold">${
              this.desk.name
            }</span></p>
            <p class="swal-bigfont-temp">Usage: <span class="bold">${this.truncate(
              this.order.usage,
              40,
            )}</span></p>
                   <p class="swal-bigfont-temp">Starting Date: <span class="bold">${this.formatDate(
                     new Date(this.order.startTime),
                     true,
                     false,
                   )}</span></p>
                   <p class="swal-bigfont-temp">Duration: <span class="bold">${
                     this.duration
                   } ${
              this.intervalTypes[this.desk.intervalType]
            }s</span></p>`,
            icon: 'question',
            confirmButtonColor: '#1b53a8',
            showCancelButton: true,
          }).then(res => {
            if (res.value) this.submit();
          });
        }
      });
    },
    submit() {
      this.loading = true;

      this.$store.dispatch('addCartItem', {
        usage: this.order.usage,
        startTime: `2021-01-27'T'14:30:00`,
        endTime: `2021-0${this.duration + 1}-27'T'14:30:00`,
        productId: this.desk.productId,
        duration: this.duration,
      });
      this.$swal({
        title: 'Item has been added to your Cart',
        text: 'Do you want to see your Cart?',
        icon: 'success',
        showConfirmButton: true,
        showDenyButton: true,
        confirmButtonColor: '#1b53a8',
        denyButtonColor: '#1b53a8',
        confirmButtonText: 'Cart',
        denyButtonText: 'Dashbaord',
      }).then(res => {
        if (res.isConfirmed) this.$router.push({ name: 'Cart' });
        else if (res.isDenied)
          this.$router.push({ name: 'Dashboard' });
      });

      // api
      //   .createOrder(item)
      //   .then(response => {
      //     console.log(response);
      //     this.$swal({
      //       title: 'Order created',
      //       text:
      //         'Your Order has been created. You will receive an email with your invoice soon.',
      //       icon: 'success',
      //       confirmButtonColor: '#1b53a8',
      //     }).then(res => {
      //       if (res.value) this.$router.push({ name: 'Dashboard' });
      //     });
      //   })
      //   .catch(error => {
      //     console.log('An error ocurred: ', error);
      //   })
      //   .finally((this.loading = false));
    },
    toggleConfirmation() {
      this.showConfirmation = !this.showConfirmation;
    },
    handleScroll() {
      if (this.$refs.wrapper.scrollTop <= 250) {
        if (this.showBannerContent) this.showBannerContent = false;
        document.querySelector(
          '#banner-wrapper',
        ).style.height = `${400 - this.$refs.wrapper.scrollTop}px`;
      } else if (!this.showBannerContent)
        this.showBannerContent = true;
    },
  },
};
</script>
<style scoped lang="scss">
.swal-bigfont-temp {
  font-size: 18px !important;
}

// Content
#banner-wrapper {
  /* not clean */
  left: 320px;
  right: 74px;

  height: 400px;
  position: fixed;
  z-index: 0;

  min-height: 150px;
  background-color: whitesmoke;

  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

#banner-content {
  width: 100%;
  height: 100%;

  color: white;
  background-color: rgba(0, 0, 0, 0.6);

  font-size: 1.6em;
  font-weight: bold;
}

/* move padding from outer wrapper to content-wrapper
so banner can be full width */
.wrapper {
  padding: 0;
}

.content-wrapper {
  height: 84%;
  padding: (400px + 35px) 20px 35px;
}

section {
  width: 100%;
  margin-bottom: 20px 0;
  min-height: 200px;
  // background-color: whitesmoke;
}

#section-general-info {
  display: flex;
  flex-direction: column;
  align-items: center;
  font-size: 1.3em;
}

#section-general-info .price {
  color: $green;
  font-weight: bold;
  margin-right: 10px;
}

#section-general-info .content-row-wrapper {
  font-size: 1.2em;
}

#section-general-info .capacity {
  display: inline-flex;
  justify-content: center;
  align-items: center;
  height: 30px;
  width: 60px;
  margin: 0 25px;
  padding: auto 2px;
  border-radius: 20px;

  color: $primary;
  background-color: lightgrey;
  font-weight: bold;
}

#section-general-info .capacity img {
  height: 0.8em;
  margin-right: 7px;
  color: $primary;
}

.text-small {
  font-size: 0.8em;
  color: $text-grey;
}

.content-row-wrapper {
  margin: 30px 0 10px;
}

.description {
  max-width: 60%;
  margin: 40px 0 20px;
  text-align: center;
}

#btn-addtocart {
  position: absolute;
  bottom: 80px;
  right: 120px;
  z-index: 10;
  width: 200px;
}

// Confirmation Modal
.confirmation-info-wrapper {
  width: 50%;
  margin: 0 auto;
}

.confirm-col-title {
  display: inline-block;
  font-weight: 600;
  width: 70px;
  margin: 0 20px 10px;
  text-align: right;
}

.subContainer {
  width: 100%;
  margin: 20px 0;
}

.dateTimeContainer {
  width: 50%;
  min-width: 130px;
}

.box {
  box-shadow: none;
  margin: 0 !important;
}

h1 {
  color: $primary !important;
}

.mainInfo {
  font-size: 1.5em;
  letter-spacing: 2px;
  margin: 10px 0;
}

.big-text {
  margin: 0 10px 0 5px;
}

.grey {
  color: $text-grey;
}

.img-container {
  height: 400px;
  max-height: 60vh;

  padding: 0;
}

.img-container img {
  height: 100%;
  object-fit: cover;

  border-radius: 15px;
}

#order-details-form {
  width: 30%;
  margin: 0 auto;
}

.label-duration {
  display: block;
  margin-top: 30px !important;
}

.input-duration {
  margin-top: 0;
}

.input-duration > div {
  width: 150px;
}
</style>
