<template>
  <div class="OrderDetail">
    <div class="contentContainer">
      <div
        v-if="order.status === 'pending'"
        class="leftContainer flex-center"
      >
        Invoice has not been created yet
      </div>
      <div v-else class="leftContainer">
        <IconBtn
          class="btn-dl-pdf"
          type="download"
          v-on:click="downloadPdf"
        />
        <pdf
          :src="order.pdfUri"
          @page-loaded="pdfLoaded"
          :key="reloadPdfKey"
        ></pdf>
      </div>
      <div class="rightContainer">
        <section class="section">
          <TitleH2 :text="'Order Nr #' + id" />
          <div class="sectionContent btns">
            <button
              v-if="order.status == 'pending'"
              @click="acceptOrder"
              class="button is-rounded is-success"
              :class="{ 'is-loading': loading.accept }"
            >
              Confirm Order and send Invoice
            </button>
            <button
              v-if="order.status == 'pending'"
              @click="denyOrder"
              class="button is-rounded is-danger"
              :class="{ 'is-loading': loading.deny }"
            >
              Deny Order
            </button>
            <button
              v-if="order.status == 'billed'"
              @click="paidOrder"
              class="button is-rounded is-primary"
              :class="{ 'is-loading': loading.paid }"
            >
              Mark Order as Paid
            </button>
          </div>
        </section>
        <section class="section">
          <TitleH2 text="Products" />
          <div class="sectionContent">
            <BookingTile
              :booking="b"
              v-for="(b, bid) in order.items"
              :key="bid"
            />
          </div>
        </section>
      </div>
    </div>
  </div>
</template>
<script>
import api from '@/services/api';
import pdf from 'vue-pdf';

import BookingTile from '@/components/OrderDetailBookingTile';
import TitleH2 from '@/components/base/TitleH2';
import IconBtn from '@/components/base/IconBtn';

export default {
  name: 'OrderDetail',
  components: {
    BookingTile,
    TitleH2,
    pdf,
    IconBtn,
  },
  data() {
    return {
      loading: {
        accept: false,
        deny: false,
        paid: false,
      },
      order: {},
      confirmation: {
        show: false,
        error: false,
        title: '',
        text: '',
      },
      reloadPdfKey: 1,
    };
  },
  props: ['id'],
  created() {
    api.getOrder(this.id).then(response => {
      this.order = response.data;
    });
  },
  methods: {
    acceptOrder() {
      this.loading.accept = true;
      api
        .acceptOrder(this.id)
        .then(() => {
          this.$swal({
            title: 'Accepting successful',
            text: `Order ${this.id} has been confirmed. The User will soon receive a confirmation Email including his Invoice.`,
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          });
          this.order.status = 'billed';
        })
        .catch(error => {
          this.$swal({
            title: 'Something went wrong',
            text: `The order could not be accepted: \n${error}`,
            icon: 'error',
            confirmButtonColor: '#1b53a8',
          });
        })
        .finally((this.loading.accept = false));
    },
    denyOrder() {
      this.loading.deny = true;
      api
        .denyOrder(this.id)
        .then(() => {
          this.$swal({
            title: 'Denying successful',
            text: `Order ${this.id} has been denied. The User will be notified via email.`,
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          });
          this.order.status = 'denied';
        })
        .catch(error => {
          this.$swal({
            title: 'Something went wrong',
            text: `The order could not be denied: \n${error}`,
            icon: 'error',
            confirmButtonColor: '#1b53a8',
          });
        })
        .finally((this.loading.deny = false));
    },
    paidOrder() {
      this.loading.paid = true;
      api
        .paidOrder(this.id)
        .then(() => {
          this.$swal({
            title: 'Success',
            text: `Order ${this.id} has been marked as paid.`,
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          });
          this.order.status = 'paid';
        })
        .catch(error => {
          this.$swal({
            title: 'Something went wrong',
            text: `The order could not be marked as paid: \n${error}`,
            icon: 'error',
            confirmButtonColor: '#1b53a8',
          });
        })
        .finally((this.loading.paid = false));
      api
        .paidOrder(this.id)
        .then(
          this.displayConfirmation(
            `Order ${this.id} has been marked as paid`,
          ),
        );
    },
    displayConfirmation(title, text) {
      this.confirmation.show = true;
      this.confirmation.title = title;
      this.confirmation.text = text;
    },
    closeConfirmation(push) {
      if (push) this.$router.push({ name: 'Orders' });
      else {
        this.confirmation.show = false;
        this.confirmation.error = false;
      }
    },
    pdfLoaded() {
      if (this.reloadPdfKey === 0) {
        api.getOrder(this.id).then(response => {
          this.order = response.data;
          this.reloadPdfKey = 1;
        });
      }
    },
    downloadPdf() {
      window.open(this.order.pdfUri, '_blank');
    },
  },
};
</script>
<style scoped lang="scss">
.contentContainer {
  display: flex;
  flex-wrap: nowrap;

  height: 100%;
  width: 100%;
  border-radius: 20px;
  background-color: $tile-background;
  padding: 20px 35px;
}

.leftContainer,
.rightContainer {
  height: 100%;
}

// left Container (Invoice Pdf)
.leftContainer {
  width: 570px;

  padding-right: 35px;
  border-right: 2px solid black;
}

.leftContainer > span {
  height: 98%;
  width: auto;
  margin: 0 auto;
}

.rightContainer {
  width: calc(100% - 570px);
  padding: 30px;
  padding-right: 0;
}

.flexContent {
  display: flex;
  width: 100%;

  margin-top: 50px;

  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.flexContent > * {
  margin: 20px 0;
}

.sectionContent.btns button {
  margin: 10px;
}

.nameH1 {
  color: $primary;
  font-weight: 600;
  font-size: 30px !important;
  letter-spacing: 0.2em;
}

.mainInfo {
  width: 70%;
}

.col {
  width: 100%;
  height: 45px;
}

.key,
.val {
  display: inline-block;
}

.key {
  width: 40%;
}

.val {
  float: right;
  width: 60%;
}

figure.is-256x256 {
  height: 256px;
  width: 256px;
}

figure.is-300x300 {
  height: 300px;
  width: 300px;
}

img.pfp {
  border: 5px solid $primary;
}

.btn-dl-pdf {
  position: relative;
  top: 35px;
  left: 20px;
  z-index: 1;
}

// .status-new {
//   font-weight: 600;
//   color: $primary;
// }

// .status-accepted {
//   color: yellowgreen;
// }

// .status-denied {
//   color: gray;
// }

// .status-paid {
//   color: green;
// }
</style>
