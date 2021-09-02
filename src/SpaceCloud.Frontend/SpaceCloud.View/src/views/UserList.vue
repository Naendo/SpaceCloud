<template>
  <div class="UserList">
    <div class="wrapper">
      <div class="content-wrapper-with-subnav">
        <SubNavSide
          :topTitle="users.length"
          :topSubtitle="'users'"
          :items="['All Users', 'Workers', 'Admins', 'Owners']"
          v-on:item-clicked="subNavItemClicked($event)"
        />
        <div class="full-height-table-container scrollable">
          <table
            v-if="users.length > 0"
            class="sticky-header table is-hoverable is-fullwidth is-striped"
          >
            <colgroup>
              <col id="colName" />
              <col id="colMail" />
              <col id="colRole" />
              <col id="colLocation" />
              <col id="colEdit" />
            </colgroup>
            <tbody>
              <tr id="table-headers">
                <th></th>
                <th>Name</th>
                <th>E-Mail</th>
                <th>Role</th>
                <th>Edit</th>
                <!--<th>Status</th>-->
              </tr>
              <tr
                @click="editUser(user.userId)"
                v-for="user in users"
                :key="user.userId"
              >
                <td>
                  <figure class="image is-32x32">
                    <img
                      v-if="showImg"
                      class="is-rounded"
                      :src="user.imageUri"
                      @error="showImg = false"
                    />
                    <img
                      v-else
                      class="is-rounded"
                      src="https://bulma.io/images/placeholders/48x48.png"
                      alt="User Profile Picture"
                    />
                  </figure>
                </td>
                <td>{{ user.firstName + ' ' + user.lastName }}</td>
                <td>{{ truncateMail(user.mail) }}</td>
                <td>{{ user.role }}</td>
                <td>
                  <IconBtn
                    type="edit"
                    v-on:clicked="editUser(user.userId)"
                  />
                </td>
                <!--<td>{{ tickets[i].status }}</td>-->
              </tr>
            </tbody>
          </table>
          <p v-else class="table-alt">No users could be found</p>
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
  name: 'UserList',
  components: { IconBtn, SubNavSide },
  mixins: [textFormatMixin],
  data() {
    return {
      allUsers: [],
      users: [],
      locationId: 0,
      showImg: true,
    };
  },
  created() {
    api
      .getUsers()
      .then(response => {
        this.allUsers = response.data;
        this.users = response.data;
        this.locationId = response.data.locationId;
        this.formatUserData();
        this.updateUserList += 1;
      })
      .catch(error => {
        console.log('error', error);
      });
  },
  methods: {
    subNavItemClicked(item) {
      // no new ip call, list is only filtered
      if (item === 'Workers')
        this.users = this.allUsers.filter(u => u.role === 'Worker');
      else if (item === 'Admins')
        this.users = this.allUsers.filter(u => u.role === 'Admin');
      else if (item === 'Owners')
        this.users = this.allUsers.filter(u => u.role === 'Owner');
      else if (item === 'All Users') this.users = this.allUsers;
    },
    formatUserData() {
      const { users } = this;
      const altMsg = '- - - - -';

      users.forEach(u => {
        if (
          !this.validStr(u.firstName) &&
          !this.validStr(u.lastName)
        ) {
          u.firstName = altMsg;
          u.lastName = '';
        }
        if (!this.validStr(u.mail)) u.mail = altMsg;

        if (typeof u.role === 'number')
          u.role = this.getRoleName(u.role);
      });
    },
    editUser(uid) {
      this.$router.push({
        name: 'User',
        params: { uid },
      });
    },
  },
};
</script>
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
</style>
