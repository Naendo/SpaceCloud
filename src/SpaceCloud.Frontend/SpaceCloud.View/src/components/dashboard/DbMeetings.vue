<template>
  <div class="DbMeetings">
    <TitleH3
      v-if="$store.getters['auth/isAdmin']"
      text="Upcoming Meetings"
      color="white"
    />
    <TitleH3 v-else text="Your Upcoming Meetings" color="white" />
    <div class="meetings-container scrollable">
      <!-- <div class="date-header">Today</div> -->
      <div v-if="meetings.length > 0">
        <div
          v-for="(meeting, i) in meetings"
          :key="i"
          class="meeting"
        >
          <div>{{ meeting.room }}</div>
          <div>{{ meeting.name }}</div>
          <div>{{ meeting.date }}</div>
        </div>
      </div>
      <p v-else class="table-alt">No meetings available</p>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
// import api from '@/services/api';

export default {
  name: 'name',
  components: { TitleH3 },
  data() {
    return {
      meetings: [],
    };
  },
  mounted() {
    // api.getOrders().then(response => {
    //   // this.meetings = response.data;
    //   console.log(response.data);
    // });
    if (this.$store.getters['auth/isAdmin']) {
      this.meetings = [
        {
          room: 'Meeting Room 1',
          name: 'Max Müster',
          date: '27.01.2021',
        },
        {
          room: 'Meeting Room 1',
          name: 'Max Müster',
          date: '27.01.2021',
        },
        {
          room: 'Meeting Room 1',
          name: 'Max Müster',
          date: '27.01.2021',
        },
        {
          room: 'Meeting Room 1',
          name: 'Max Müster',
          date: '27.01.2021',
        },
        {
          room: 'Meeting Room 1',
          name: 'Max Müster',
          date: '27.01.2021',
        },
      ];
    }
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.DbMeetings {
  padding: 30px;
  background-color: $primary !important;
}

h3 {
  color: white;
}

.meetings-container {
  height: 79%;
}

.meeting {
  display: flex;
  justify-content: space-between;
  flex-wrap: nowrap;
  align-items: center;

  min-height: 50px;
  width: 100%;

  margin: 10px 0;

  background-color: #ffffff;
}

.meeting > * {
  width: 30%;
  text-align: center;
}

.date-header {
  font-size: 0.85em;
  color: white;

  padding-left: 5px;
}

.table-alt {
  color: white;
}
</style>
