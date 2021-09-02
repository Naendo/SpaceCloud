<template>
  <div class="DbOrders">
    <TitleH3 :text="titleComp" />
    <div class="table-container scrollable">
      <table
        v-if="orders.length > 0"
        class="table is-hoverable is-fullwidth"
      >
        <!-- <tbody v-if="user === -1">
          <tr>
            <th>ID</th>
            <th>Status</th>
            <th>Price</th>
            <th>Date</th>
          </tr>
          <tr v-for="order in orders" :key="order.orderId">
            <td>{{ order.orderId }}</td>
            <td :class="{ 'text-error': order.status === 'pending' }">
              {{ order.status }}
            </td>
            <td>{{ order.price + ' €' }}</td>
            <td>{{ order.created }}</td>
          </tr>
        </tbody> -->
        <tbody>
          <tr>
            <th>ID</th>
            <th>User</th>
            <th>Status</th>
            <th>Date</th>
          </tr>
          <tr v-for="order in orders" :key="order.orderId">
            <td>{{ order.orderId }}</td>
            <td>{{ order.name }}</td>
            <td :class="{ 'text-error': order.status === 'pending' }">
              {{ order.status }}
            </td>
            <td>{{ order.created }}</td>
          </tr>
        </tbody>
      </table>
      <p v-else class="table-alt">No {{ status }} orders</p>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
import api from '@/services/api';

import textFormatMixin from '@/mixins/TextFormat';

export default {
  name: 'name',
  components: { TitleH3 },
  mixins: [textFormatMixin],
  data() {
    return {
      orders: [],
    };
  },
  props: {
    title: {
      type: String,
      default: '',
    },
    status: {
      type: String,
      default: '',
    },
    user: {
      type: Number,
      default: -1,
    },
  },
  computed: {
    titleComp() {
      let { title } = this;

      if (title !== '') return title;

      if (this.user !== -1) title = `User #${this.user}'s `;
      if (this.status !== '') title += `${this.status} `;
      else title = 'recent ';

      title += 'orders';

      return title;
    },
  },
  async created() {
    await api.getOrders().then(response => {
      if (this.user !== -1)
        response.data = response.data.filter(
          o => o.userId === this.user,
        );
      this.orders = response.data.reverse();
    });

    if (this.status) {
      this.orders = this.orders.filter(
        o => o.status === this.status.toLowerCase(),
      );
    }

    this.orders.forEach(o => {
      o.user = this.truncate(o.name);
      o.created = this.formatDate(new Date(o.created), true, false);
    });
  },
};
</script>
<style scoped lang="scss">
.DbOrders {
  padding: 25px;
  background-color: $tile-background;
}

.table-container {
  height: 74%;
  overflow-y: auto;
}

table > * {
  text-align: center !important;
}
</style>
