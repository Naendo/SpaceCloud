<template>
  <div class="DbNewActivities">
    <TitleH3 text="New Activities" />
    <!--<h3 class="title is-3">New Activities</h3>-->
    <div class="table-container scrollable">
      <table
        v-if="activities.length > 0"
        class="table is-hoverable is-fullwidth"
      >
        <tbody>
          <tr>
            <th>Activity</th>
            <th>User</th>
            <th>Date</th>
          </tr>
          <tr v-for="(act, actionId) in activities" :key="actionId">
            <td>{{ act.action }}</td>
            <td>{{ act.name }}</td>
            <td>{{ act.date }}</td>
          </tr>
        </tbody>
      </table>
      <p v-else class="table-alt">
        No recent activities are available
      </p>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';

export default {
  name: 'DbNewActivities',
  components: { TitleH3 },
  props: { activities: Array },
  data() {
    return {};
  },
};
</script>
<style scoped lang="scss">
.DbNewActivities {
  width: 600px;
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
