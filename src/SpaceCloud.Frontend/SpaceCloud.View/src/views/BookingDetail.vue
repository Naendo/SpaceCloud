<template>
  <div class="BookingDetail">
    <div class="contentContainer">
      <TitleH1 text="Booking" />
      <div class="table-container scrollable">
        <span>{{ uid }}</span>
      </div>
    </div>
  </div>
</template>
<script>
// import api from '@/services/api';
import TitleH1 from '@/components/base/TitleH1';

export default {
  name: 'BookingDetail',
  components: { TitleH1 },
  data() {
    return {
      booking: {},
    };
  },
  props: ['id'],
  created() {
    // api
    //   .getBooking(this.id)
    //   .then(response => {
    //     this.booking = response.data;
    //     console.log('Booking:\n', this.booking);
    //   })
    //   .catch(error => {
    //     console.log('error', error);
    //   });
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.contentContainer {
  height: 100%;
  width: 100%;
  border-radius: 20px;
  background-color: $tile-background;
  overflow-y: auto;
  padding: 20px 35px;
}

.StatusNew {
  font-weight: 600;
  color: $primary;
}

.StatusAccepted {
  color: yellowgreen;
}

.StatusDenied {
  color: gray;
}

.StatusPaid {
  color: green;
}
</style>
