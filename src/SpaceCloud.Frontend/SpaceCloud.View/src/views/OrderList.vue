<template>
  <div class="OrderList">
    <div class="wrapper">
      <div class="content-wrapper-with-subnav">
        <SubNavSide
          v-if="$store.getters['auth/isAdmin']"
          :topTitle="orders.length"
          :topSubtitle="'orders'"
          :items="[
            'All orders',
            'Pending orders',
            'Billed orders',
            'Paid orders',
            'Declined orders',
            'Your orders',
          ]"
          v-on:item-clicked="subNavItemClicked($event)"
        /><SubNavSide
          v-else
          :topTitle="orders.length"
          :topSubtitle="'orders'"
          :items="['Your orders']"
          v-on:item-clicked="subNavItemClicked($event)"
        />
        <div class="full-height-table-container scrollable">
          <table
            v-if="orders.length > 0"
            class="sticky-header table is-hoverable is-fullwidth is-striped"
          >
            <colgroup>
              <col id="colNr" />
              <col id="colUser" />
              <col id="colCat" />
              <col id="colPrice" class="bold has-text-danger" />
              <col id="colDate" />
              <col id="colEdit" />
            </colgroup>
            <tbody>
              <tr id="table-headers">
                <th>Order Nr</th>
                <th>User</th>
                <th>Status</th>
                <th>Price</th>
                <th>Date</th>
                <th>Edit</th>
                <!--<th>Status</th>-->
              </tr>
              <tr
                @click="editOrder(o.orderId)"
                v-for="o in orders"
                :key="o.orderId"
              >
                <td>{{ o.orderId }}</td>
                <td>
                  {{ o.name }}
                </td>
                <td :class="'status' + o.status">
                  {{ o.status }}
                </td>
                <td :class="formatPrice(o)">{{ o.price }} €</td>
                <td v-if="o.creationDate">
                  {{ o.creationDate }}
                </td>
                <td v-else>
                  No Invoice
                </td>
                <td>
                  <IconBtn
                    type="edit"
                    v-on:clicked="editOrder(o.orderId)"
                  />
                </td>
              </tr>
            </tbody>
          </table>
          <p v-else class="table-alt">No orders could be found</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import api from '@/services/api';
import IconBtn from '@/components/base/IconBtn';
import SubNavSide from '@/components/navs/SubNavSide';
import textFormatMixin from '@/mixins/TextFormat';

export default {
  name: 'OrderList',
  components: { IconBtn, SubNavSide },
  data() {
    return {
      orders: [],
      allOrders: [],
    };
  },
  computed: {},
  mixins: [textFormatMixin],
  async created() {
    api
      .getOrders()
      .then(response => {
        const o = response.data.reverse();
        o.forEach(order => {
          if (order.creationDate !== undefined)
            order.creationDate = this.formatDate(
              new Date(order.creationDate),
              true,
              true,
            );
          if (order.price === undefined) order.price = '?';
          o.sort(this.sortByInvId);

          this.allOrders = o;
          this.orders = o;
        });
      })
      .catch(error => {
        console.log(error);
      });
  },
  methods: {
    subNavItemClicked(item) {
      // no new ip call, list is only filtered
      switch (item) {
        case 'Pending orders':
          this.orders = this.allOrders.filter(
            o => o.status === 'pending',
          );
          break;

        case 'Billed orders':
          this.orders = this.allOrders.filter(
            o => o.status === 'billed',
          );
          break;

        case 'Declined orders':
          this.orders = this.allOrders.filter(
            o => o.status === 'declined',
          );
          break;

        case 'Paid orders':
          this.orders = this.allOrders.filter(
            o => o.status === 'paid',
          );
          break;

        case 'Your orders':
          this.orders = this.allOrders.filter(
            o =>
              o.userId ===
              parseInt(this.$store.state.auth.user.UserId, 10),
          );
          break;

        case 'All orders':
        default:
          this.orders = this.allOrders;
          break;
      }
    },
    editOrder(oid) {
      this.$router.push({
        name: 'Order',
        params: { id: oid },
      });
    },
    formatPrice(order) {
      return order.status === 'new' ? 'red' : '';
    },
    sortByInvId(a, b) {
      if (a.invoiceId < b.invoiceId) return 1;
      if (a.invoiceId > b.invoiceId) return -1;
      return 0;
    },
  },
};
</script>
<!--<style scoped lang="css">
@import '@/styles/css/mystyles.css';
</style>-->
<style scoped lang="scss">
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

.statuspending {
  font-weight: 600;
  color: $red;
}

.statusbilled {
  font-weight: 500;
}

.statusdenied {
  color: gray;
}
</style>
