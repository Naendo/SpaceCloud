<template>
  <div class="DbNewTickets">
    <!--<h3 class="title is-3">Open Tickets</h3>-->
    <TitleH3
      text="Open Tickets"
      button="add"
      v-on:button-click="createDemoTicket"
    />
    <div class="table-container scrollable">
      <table
        v-if="tickets.length > 0"
        class="table is-hoverable is-fullwidth"
      >
        <tbody>
          <tr id="table-headers">
            <th>ID</th>
            <th>Creator</th>
            <th>Subject</th>
            <th>Status</th>
            <th>Creation</th>
            <!--<th>Status</th>-->
          </tr>
          <tr v-for="(ticket, ticketId) in tickets" :key="ticketId">
            <td>{{ ticket.ticketId }}</td>
            <td>{{ ticket.name }}</td>
            <td>{{ subjectShort(ticket.subject) }}</td>
            <td class="has-text-success">{{ 'open' }}</td>
            <td>{{ ticket.creationDate }}</td>
            <!--<td>{{ tickets[i].status }}</td>-->
          </tr>
        </tbody>
      </table>
      <p v-else class="table-alt">There are no open Tickets</p>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
import api from '@/services/api';

export default {
  name: 'DbNewTickets',
  components: { TitleH3 },
  props: { tickets: Array },
  data() {
    return {};
  },
  methods: {
    subjectShort(subject) {
      if (subject.length > 25)
        return `${subject.substring(0, 25)}...`;
      return subject;
    },
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
.DbNewTickets {
  padding: 25px;
  background-color: $tile-background;
}

tr > * {
  text-align: center !important;
}

.table-container {
  height: 74%;
  overflow-y: auto;
}
</style>
