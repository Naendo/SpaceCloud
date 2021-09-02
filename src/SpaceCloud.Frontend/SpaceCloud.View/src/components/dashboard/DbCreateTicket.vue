<template>
  <div class="DbCreateTicket">
    <p id="ticket-title">Create Support Ticket</p>
    <IconBtn
      type="add"
      class="icon-btn"
      v-on:click="createDemoTicket"
    />
  </div>
</template>
<script>
import IconBtn from '@/components/base/IconBtn';
import api from '@/services/api';

export default {
  name: 'name',
  components: { IconBtn },
  data() {
    return {
      balance: 50,
    };
  },
  created() {
    // get spacecoin balance
  },
  methods: {
    createDemoTicket() {
      api.createTicket(
        'Testing Ticket',
        'This Ticket was created for development/testing purposes',
      );
    },
  },
};
</script>
<style scoped lang="scss">
.DbCreateTicket {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
  flex-wrap: nowrap;

  height: 47%;
  background-color: $tile-background;
  border-radius: 20px;

  padding: 25px;
}

#ticket-title {
  font-size: 1.5em;
}
</style>
