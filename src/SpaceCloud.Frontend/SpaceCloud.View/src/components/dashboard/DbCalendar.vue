<template>
  <div class="DbCalendar">
    <div class="calendar">
      <iframe
        src="https://teamup.com/ksuad2s63fnhb9e5ou?date=today&view=d&showHeader=0&showProfileAndInfo=0&showSidepanel=1&disableSidepanel=1&showViewSelector=0&showMenu=0&showStripes=0&weekStartDay=mo&showAgendaHeader=0&showAgendaDetails=0&showYearViewHeader=0"
        width="100%"
        height="100%"
        frameborder="0"
      ></iframe>
    </div>
    <div class="eventList">
      <h5 class="title is-5">{{ date }}</h5>
      <div class="table-container scrollable">
        <table class="table is-hoverable is-fullwidth">
          <tbody>
            <tr v-for="(booking, pid) in bookings" :key="pid">
              <td class="td-title">{{ booking.title }}</td>
              <td class="td-time">
                {{ booking.startTime }} -
                {{ booking.endTime }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
import textFormatMixin from '@/mixins/TextFormat';

export default {
  name: 'DbCalendar',
  components: {},
  data() {
    return {
      date: new Date(),
      bookings: [
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
        {
          title: 'Seminarraum 01',
          startTime: '14:00',
          endTime: '16:00',
        },
      ],
    };
  },
  mounted() {
    this.date = this.formatDate(new Date(), true, true);
  },
  mixins: [textFormatMixin],
  methods: {},
};
</script>

<style scoped lang="scss">
.DbCalendar {
  display: flex;

  width: 700px;
}

.DbCalendar > * {
  display: inline-block;
  height: 100%;
}

.calendar {
  flex-grow: 1;
}

.eventList {
  flex-grow: 2;

  float: right;
  min-width: 320px;
  padding: 25px;

  background-color: $primary;
  border-radius: 0 20px 20px 0;
}

.title {
  margin-left: 20px;

  color: white;
}

.table {
  background-color: transparent;
  color: white;
}

.table tr:hover {
  background-color: $primary-darker !important;
}

td.td-time {
  text-align: right !important;
}
</style>
