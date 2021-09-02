<template>
  <div class="BookingList">
    <div class="wrapper">
      <div class="content-wrapper-with-subnav">
        <SubNavSide
          :topTitle="bookings.length"
          :topSubtitle="'bookings'"
          :items="['All Bookings', 'Desks', 'Meeting Rooms', 'Other']"
          :button="'Book a Product'"
          v-on:item-clicked="subNavItemClicked($event)"
          v-on:button-clicked="$router.push({ name: 'Products' })"
        />

        <div class="full-height-table-container scrollable">
          <table
            v-if="bookings.length > 0"
            class="sticky-header table is-hoverable is-fullwidth is-striped"
          >
            <colgroup>
              <col id="colproduct" />
              <col id="colUser" />
              <col id="colTime" />
              <col id="colStatus" />
              <col id="colEdit" />
            </colgroup>
            <tbody>
              <tr id="table-headers">
                <th>Product</th>
                <th>User</th>
                <th>Time</th>
                <th>Status</th>
                <th>Edit</th>
              </tr>
              <tr v-for="b in bookings" :key="b.bid">
                <td>{{ b.name }}</td>
                <td>
                  {{ b.user.firstName + ' ' + b.user.lastName }}
                </td>
                <td>
                  {{ `${b.date} ${b.startTime} - ${b.endTime}` }}
                </td>
                <td>{{ b.status }}</td>
                <td>
                  <BtnEdit v-on:clicked="editBooking(b.id)" />
                </td>
              </tr>
            </tbody>
          </table>
          <p v-else class="table-alt">No bookings could be found</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import SubNavSide from '@/components/navs/SubNavSide';
import api from '@/services/api';
import BtnEdit from '@/components/base/BtnEdit';

export default {
  name: 'BookingList',
  components: { SubNavSide, BtnEdit },
  data() {
    return {
      bookings: [],
      locationId: 0,
      showImg: true,
    };
  },
  created() {
    this.bookings = api.getBookings().then(response => {
      this.bookings = response.data;
      this.locationId = response.data.locationId;
      this.formatBookingData();
    });
    // this.bookings = DummyData();
  },
  methods: {
    formatBookingData() {
      /* let bookings = this.bookings;
      let altMsg = '- - - - -';

      bookings.forEach(u => {
        if (this.invStr(u.firstName) && this.invStr(u.lastName))
          u.firstName = altMsg;
        if (this.invStr(u.mail)) u.mail = altMsg;

        if (u.role == 1) u.role = 'User';
        else if (u.role == 2) u.role = 'Admin';
        else if (u.role == 3) u.role = 'Owner';
        else u.role = '?: ' + u.role;
        }); */
    },
    // invalid String: returns true if string is undefined, null or empty
    invStr(str) {
      if (typeof str === 'undefined') return true;
      if (str === null) return true;
      if (str === '') return true;
      return false;
    },
    editBooking(id) {
      this.$router.push({
        name: 'BookingDetail',
        params: { id },
      });
    },
    subNavItemClicked(item) {
      console.log('method called on: ', item);
    },
  },
};
</script>
<style scoped lang="scss">
/*
.contentContainer {
  height: 100%;
  width: 100%;
  border-radius: 20px;
  background-color: $tile-background;
  overflow-y: auto;
  padding: 20px 35px;
}

.table-container {
  width: 75%;
  min-width: 600px;
  float: right;
}

.scrollable {
  overflow-y: auto !important;
}

table > * {
  text-align: center !important;
}

td {
  padding: 0;
  vertical-align: middle !important;
}

col {
  min-width: 100px;
}
*/
table > * {
  text-align: center !important;
}

td {
  padding: 0;
  vertical-align: middle !important;
}

col {
  min-width: 100px;
}

#colEdit {
  width: 90px;
}
</style>
