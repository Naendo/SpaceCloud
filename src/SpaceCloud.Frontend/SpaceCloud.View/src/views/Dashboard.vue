<template>
  <div id="Dashboard">
    <DbNewsfeed v-if="!userIsAdmin" id="newsfeed" />
    <div id="dashboard-container" class="scrollable">
      <DbMeetings class="flex-size-2 top-comp" />
      <DbNewTickets
        v-if="userIsAdmin"
        class="flex-size-3 top-comp"
        :tickets="FetchRecentTickets"
      />
      <div class="break"></div>
      <DbOrders
        v-if="userIsAdmin"
        status="Pending"
        class="flex-size-3"
        :key="updateDbOrdersKey"
      />
      <div>
        <DbSpacecoinBalance />
        <DbCreateTicket />
      </div>
      <DbNewActivities
        v-if="userIsAdmin"
        class="flex-size-3"
        :activities="FetchRecentActivities"
      />
      <DbOrders
        v-if="!userIsAdmin"
        title="Your last orders"
        class="flex-size-3"
      />
    </div>
  </div>
</template>
<script>
import DbNewTickets from '@/components/dashboard/DbNewTickets';
import DbNewActivities from '@/components/dashboard/DbNewActivities';
import DbMeetings from '@/components/dashboard/DbMeetings';
import DbSpacecoinBalance from '@/components/dashboard/DbSpacecoinBalance';
import DbCreateTicket from '@/components/dashboard/DbCreateTicket';
import DbOrders from '@/components/dashboard/DbOrders';
import DbNewsfeed from '@/components/dashboard/DbNewsfeed';

import * as signalR from '@microsoft/signalr';
import api from '@/services/api';

import textFormatMixin from '@/mixins/TextFormat';

export default {
  name: 'dashboard',
  components: {
    DbMeetings,
    DbNewTickets,
    DbOrders,
    DbSpacecoinBalance,
    DbCreateTicket,
    DbNewActivities,
    DbNewsfeed,
  },
  data() {
    return {
      FetchRecentActivities: [],
      FetchRecentTickets: [],
      signalRconnection: null,
      userIsAdmin: false,
      usage: {
        value: 0,
        max: 0,
      },
      updateDbOrdersKey: 0,
    };
  },
  mixins: [textFormatMixin],
  async created() {
    // Fetch Coworking Usage dashboard/fetch/usage
    if (await this.$store.getters['auth/isAdmin']) {
      this.userIsAdmin = true;

      api.fetchCoworkingUsage().then(
        res => {
          this.usage.value = res.currentDesks;
          this.usage.max = res.maxDesks;
        },
        error => {
          console.log('an error occurred; ', error);
        },
      );

      // Auth
      const token = `${this.$store.state.auth.jwt.token}`;
      const API_URL = process.env.VUE_APP_API_URL;

      // SignalR
      // create and start SignalR connection
      const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${API_URL}dashboard`, {
          accessTokenFactory: () => token,
        })
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Error) // Trace, Debug, Information, Warning, Error, Critical, None
        .build();
      this.signalRconnection = connection;

      try {
        await connection.start();
      } catch (err) {
        console.log('SignalR connection error: ', err);
      }

      // Fetch RecentActions
      let fetchResult = await api.fetchRecentActions();

      // Format Dates and sort Array
      if (fetchResult != null) {
        fetchResult.sort((a, b) => {
          return new Date(b.date) - new Date(a.date);
        });
        fetchResult.forEach(act => {
          act.action = this.truncate(act.action);
          act.name = this.truncate(
            `${act.firstName} ${act.lastName}`,
          );
          act.date = this.formatDate(
            new Date(act.date),
            true,
            true,
            true,
          );
        });
        this.FetchRecentActivities = fetchResult;
      }

      // Same thing for Recent Tickets
      fetchResult = await api.fetchTickets();

      if (fetchResult != null) {
        fetchResult.sort((a, b) => {
          return new Date(b.creationDate) - new Date(a.creationDate);
        });
        fetchResult.forEach(ticket => {
          ticket.name = this.truncate(
            `${ticket.firstName} ${ticket.lastName}`,
          );
          ticket.subject = this.truncate(ticket.subject);
          ticket.creationDate = this.formatDate(
            new Date(ticket.creationDate),
            true,
            true,
            true,
          );
        });
        this.FetchRecentTickets = fetchResult;
      }

      /* Subscribe to SignalR events */
      /* Callback Dates include Timezone -> no adjustment needed (no 4th prop on this.formatDate) */
      connection.on('OnActivity', callback => {
        callback.action = this.truncate(callback.action);
        callback.name = this.truncate(
          `${callback.firstName} ${callback.lastName}`,
        );
        callback.date = this.formatDate(
          new Date(callback.date),
          true,
          true,
        );
        this.FetchRecentActivities.unshift(callback);

        // (temporary?) update DbOrders Component when new order has been created
        if (callback.action === 'Order created') {
          this.updateDbOrdersKey += 1;
        }
      });

      connection.on('OnTicket', callback => {
        callback.name = this.truncate(
          `${callback.firstName} ${callback.lastName}`,
        );
        callback.subject = this.truncate(callback.subject);
        callback.creationDate = this.formatDate(
          new Date(callback.creationDate),
          true,
          true,
        );
        this.FetchRecentTickets.unshift(callback);
      });

      connection.on('OnUsage', callback => {
        console.log('OnUsage SignalR Event Called: ', callback);
        this.usage.value = callback.currentDesks;
        this.usage.max = callback.maxDesks;
      });
    }
  },
  // beforeDestroy() {
  //   this.signalRconnection.stop();
  // },
};
</script>
<style scoped lang="scss">
#Dashboard {
  display: flex;
  flex-wrap: nowrap;
}

// flexbox
#dashboard-container {
  // height: calc(100% - 35px);
  display: flex;
  flex-wrap: wrap;
  align-content: flex-start;
  // padding-bottom: 35px !important;
  overflow-y: auto;
}

// Dashboard Components
#dashboard-container > div:not(.break) {
  height: 47.5%;

  border-radius: 20px;

  margin-right: 30px;
}

.flex-size-3 {
  flex: 6 1 27.5%;
  min-width: 550px;
}

.flex-size-2 {
  flex: 2 1 400px;
  min-width: 420px;
}

.flex-size-1 {
  flex: 1 1 330px;
  min-width: 330px;
}

.break {
  flex-basis: 100%;
  height: 0;
}

.top-comp {
  margin-bottom: 30px;
}

#newsfeed {
  width: 700px;
  margin-right: 30px;
}
</style>
