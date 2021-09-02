<template>
  <div class="ProductDetail">
    <div class="contentContainer">
      <div class="tile is-ancestor">
        <div class="tile is-6 is-vertical is-parent leftContainer">
          <div class="imgContainer tile is-child box">
            <img :src="room.imageUri" alt="Room Review" />
          </div>
          <div class="tile is-child box">
            <p class="title">Calendar</p>
            <p>..</p>
          </div>
        </div>
        <div class="tile is-parent is-vertical rightContainer">
          <div class="tile is-child box">
            <!--<TitleH1 :text="room.name" />-->
            <h1 class="title is-1">{{ room.name }}</h1>

            <div class="subContainer">
              <div class="mainInfo">
                <span class="big-text bold"
                  >{{ room.price }},- €</span
                >
                <span class="grey">per person/hour</span>
              </div>
              <div class="mainInfo">
                <span class="big-text bold"
                  >{{ room.capacity }} People</span
                >
                <span class="grey">Capacity</span>
              </div>
            </div>

            <div class="subContainer">
              <p v-if="room.description !== undefined">
                {{ room.description }}
              </p>
              <p v-else>No description available.</p>
            </div>

            <!--<div class="subContainer">
              <p class="big-text">Included features:</p>
            </div>-->

            <!--<div class="subContainer">
              <p class="big-text">Addons:</p>
              <div v-for="(addons, i) in room" :key="i" class="control">
                <label class="checkbox" for="stayLogged">
                  <input
                    :id="'addon' + addon.id"
                    name="addon"
                    v-model="room.addons[i].checked"
                    type="checkbox"
                    value="false"
                  />
                  name? text?
                  {{ addon.name }}
                </label>
              </div>
            </div>-->
            <!--</div>
            <div class="tile is-child box">-->
            <form @submit="bookNow">
              <div class="dateTimeContainer">
                <div class="field">
                  <div class="control">
                    <label class="label" for="date">Date</label>
                    <input
                      id="date"
                      name="date"
                      class="input"
                      type="text"
                      v-model="purchase.date"
                      v-validate="'required'"
                    />
                  </div>
                </div>
                <div class="field">
                  <div class="control">
                    <label class="label" for="startTime"
                      >start Time</label
                    >
                    <input
                      id="startTime"
                      name="startTime"
                      class="input"
                      type="text"
                      v-model="purchase.startTime"
                      v-validate="'required'"
                    />
                  </div>
                </div>
                <div class="field">
                  <div class="control">
                    <label class="label" for="endTime"
                      >end Time</label
                    >
                    <input
                      id="endTime"
                      name="endTime"
                      class="input"
                      type="text"
                      v-model="purchase.endTime"
                      v-validate="'required'"
                    />
                  </div>
                </div>
                <div class="field">
                  <div class="control">
                    <label class="label" for="usage"
                      >Intended Use</label
                    >
                    <input
                      id="usage"
                      name="usage"
                      class="input"
                      type="text"
                      v-model="purchase.usage"
                      placeholder="Please state your intended use"
                      v-validate="'required'"
                    />
                  </div>
                </div>
              </div>
              <button
                type="submit"
                class="button is-primary is-medium is-pulled-right is-rounded"
              >
                Book now
              </button>
            </form>
          </div>
        </div>
      </div>

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
          <section class="modal-card-body">
            <h3 class="title" is-3>
              Your Order:
            </h3>
            <div class="confirmation-info-wrapper">
              <p>
                <span class="confirm-col-title">Name: </span>
                {{ room.name }}
              </p>
              <p>
                <span class="confirm-col-title">Time: </span
                >{{
                  `${purchase.date} ${purchase.startTime} - ${purchase.endTime}`
                }}
              </p>
              <p>
                <span class="confirm-col-title">Usage: </span
                >{{ purchase.usage }}
              </p>
              <p>
                <span class="confirm-col-title">Total: </span
                >{{ room.price + ',- €' }}
              </p>
            </div>
          </section>
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

export default {
  name: 'ProductDetail',
  components: {},
  data() {
    return {
      purchase: {
        date: '2020-09-11',
        startTime: '12:00',
        endTime: '13:00',
        usage: 'Meeting',
      },
      startDateTime: '',
      endDateTime: '',
      showConfirmation: false,
      message: '',
      loading: false,
    };
  },
  props: ['room'],
  mixins: [textFormatMixin],
  // created() {
  //   api.getRoomDetail(this.id).then(response => {
  //     this.room = response.data;
  //   });
  // },
  methods: {
    bookNow(evt) {
      evt.preventDefault();

      // format Times
      // this.startDateTime = this.formatDate(
      //   new Date(`${this.purchase.date}T${this.purchase.startTime}`),
      // );
      // this.endDateTime = this.formatDate(
      //   new Date(`${this.purchase.date}T${this.purchase.endTime}`),
      // );

      this.startDateTime = `${this.purchase.date}T${this.purchase.startTime}`;
      this.endDateTime = `${this.purchase.date}T${this.purchase.endTime}`;

      console.log(
        'startTime: ',
        this.purchase.startTime,
        ' endTime: ',
        this.purchase.endTime,
        ' this.startDateTime: ',
        `${this.purchase.date}T${this.purchase.startTime}`,
        ' this.endDateTime: ',
        `${this.purchase.date}T${this.purchase.endTime}`,
      );

      // Input Validation
      if (
        typeof startTime === 'undefined' ||
        typeof endTime === 'undefined'
      ) {
        this.message = 'Inputs not Valid';
        console.log(this.message);
      }

      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.message = 'Inputs not Valid';
          console.log(this.message);
        } else {
          this.toggleConfirmation();
        }
      });
    },
    submit() {
      this.loading = true;

      api
        .createOrder({
          usage: this.purchase.usage,
          startTime: this.startDateTime,
          endTime: this.endDateTime,
          productId: this.room.productId,
        })
        .then(
          console.log('Order created!'),
          this.$router.push({ name: 'Dashboard' }),
        )
        .catch(error => {
          console.log('An error ocurred: ', error);
        })
        .finally((this.loading = false));
    },
    toggleConfirmation() {
      this.showConfirmation = !this.showConfirmation;
    },
  },
};
</script>
<style scoped lang="scss">
// only temporary classes
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

.contentContainer {
  height: 100%;
  width: 100%;
  border-radius: 20px;
  background-color: $tile-background;
  overflow-y: hidden;
  padding: 20px 35px;
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

.imgContainer {
  height: 600px;
  max-height: 60vh;

  padding: 0;
}

.imgContainer img {
  height: 100%;
  object-fit: cover;
}
</style>
