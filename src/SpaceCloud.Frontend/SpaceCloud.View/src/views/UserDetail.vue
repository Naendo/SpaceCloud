<template>
  <div class="UserDetail">
    <div class="wrapper">
      <!--<TitleH1 :text="`${user.firstName} ${user.lastName}`" />
      <span>{{ uid }}</span>-->
      <div class="leftContainer">
        <div class="flexContent">
          <div class="imgContainer">
            <figure class="image is-300x300">
              <img
                v-if="showImg"
                class="pfp is-rounded"
                :src="user.imgUri"
                @error="showImg = false"
                alt="User Profile Picture"
              />
              <img
                v-else
                class="is-rounded"
                src="https://bulma.io/images/placeholders/128x128.png"
                alt="Alt User Profile Picture"
              />
            </figure>
          </div>
          <span class="nameH1"
            >{{ user.firstName }} {{ user.lastName }}</span
          >
          <div class="mainInfo">
            <div class="col">
              <div class="key bold">First Name:</div>
              <div class="val">{{ user.firstName }}</div>
            </div>
            <div class="col">
              <div class="key bold">Last Name:</div>
              <div class="val">{{ user.lastName }}</div>
            </div>
            <div class="col">
              <div class="key bold">E-Mail:</div>
              <div class="val">{{ user.mail }}</div>
            </div>
            <div v-if="user.phone != null" class="col">
              <div class="key bold">Phone:</div>
              <div class="val">{{ user.phone }}</div>
            </div>
          </div>
          <div
            v-if="$store.getters['auth/isAdmin']"
            class="promote-demote-wrapper"
          >
            <button
              v-if="user.role === 2"
              @click="promoteUser"
              type="button"
              class="button is-primary is-rounded"
            >
              Promote to Owner
            </button>
            <button
              v-if="user.role === 1"
              @click="promoteUser()"
              type="button"
              class="button is-primary is-rounded"
            >
              Promote to Admin
            </button>
            <button
              v-if="user.role === 3"
              @click="deomoteUser()"
              type="button"
              class="button is-primary is-rounded"
            >
              Demote to Admin
            </button>
            <button
              v-if="user.role === 2"
              @click="demoteUser()"
              type="button"
              class="button is-primary is-rounded"
            >
              Demote to Worker
            </button>
            <!--<button type="button" class="button is-primary is-rounded">Demote to Admin</button> -->
          </div>
        </div>
      </div>
      <div class="rightContainer scrollable">
        <section class="section">
          <TitleH2 text="Billing Adress" />
          <div class="sectionContent">
            <div class="col">
              <div class="key bold">Street:</div>
              <div class="val">Musterstraße 10</div>
            </div>
            <div class="col">
              <div class="key bold">City:</div>
              <div class="val">6844 Altach</div>
            </div>
            <div class="col">
              <div class="key bold">Country:</div>
              <div class="val">Austria</div>
            </div>
          </div>
        </section>
        <section class="section">
          <TitleH2 text="Payment Methods" />
          <div class="sectionContent">
            <div class="col">
              <div class="key bold">PayPal:</div>
              <div class="val">maxmuster@gmail.com</div>
            </div>
            <div class="col">
              <div class="key bold">Debit Card:</div>
              <div class="val">AT44 1234 5678 8765 4321</div>
            </div>
          </div>
        </section>
        <section class="section">
          <TitleH2 text="Other?" />
          <div class="sectionContent">
            <div class="col">
              <div class="key bold">Setting x:</div>
              <div class="val">Value x</div>
            </div>
            <div class="col">
              <div class="key bold">Setting y:</div>
              <div class="val">Value y</div>
            </div>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>
<script>
import api from '@/services/api';
import TitleH2 from '@/components/base/TitleH2';
import textFormatMixin from '@/mixins/TextFormat';

export default {
  name: 'UserDetail',
  components: {
    TitleH2,
  },
  mixins: [textFormatMixin],
  data() {
    return {
      user: {},
    };
  },
  props: ['uid'],
  created() {
    api
      .getUser(this.uid)
      .then(response => {
        this.user = response.data;
        console.log('user\n', this.user);
      })
      .catch(error => {
        console.log('error', error);
      });
  },
  methods: {
    promoteUser() {
      const roleName = this.getRoleName(this.user.role + 1, true);
      api.promoteUser(this.user.userId).then(
        response => {
          this.$swal({
            icon: 'success',
            title: 'Success',
            html: `User successfully promoted to ${roleName}<br />response: ${response}`,
            confirmationButtonColor: '#1b53a8',
          });
        },
        error => {
          this.$swal({
            icon: 'error',
            title: 'Something has gone wrong',
            html: `User could not be promoted to ${roleName}<br />${error}`,
            confirmButtonColor: '#1b53a8',
          });
        },
      );
    },
    demoteUser() {
      const roleName = this.getRoleName(this.user.role - 1, true);
      api.demoteUser(this.user.userId).then(
        response => {
          this.$swal({
            icon: 'success',
            title: 'Success',
            html: `User successfully demoted to ${roleName}<br />response: ${response}`,
            confirmationButtonColor: '#1b53a8',
          });
        },
        error => {
          this.$swal({
            icon: 'error',
            title: 'Something has gone wrong',
            html: `User could not be demoted to ${roleName}<br />${error}`,
            confirmButtonColor: '#1b53a8',
          });
        },
      );
    },
  },
};
</script>
<style scoped lang="scss">
.wrapper {
  display: flex;
  flex-wrap: nowrap;
  overflow-y: auto;
}

.leftContainer,
.rightContainer {
  height: 100%;
}

.leftContainer {
  display: inline-block;
  width: 35%;
  min-width: 350px;

  padding-right: 35px;
  border-right: 2px solid black;
}

.rightContainer {
  width: 65%;
  padding: 30px;
  padding-right: 0;
}

.flexContent {
  display: flex;
  width: 100%;

  margin-top: 50px;

  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.flexContent > * {
  margin: 20px 0;
}

.nameH1 {
  color: $primary;
  font-weight: 600;
  font-size: 30px !important;
  letter-spacing: 0.2em;
}

.mainInfo {
  width: 70%;
}

.promote-demote-wrapper button {
  margin: 0 10px;
}

.col {
  width: 100%;
  height: 45px;
}

.key,
.val {
  display: inline-block;
}

.key {
  width: 40%;
}

.val {
  float: right;
  width: 60%;
}

figure.is-256x256 {
  height: 256px;
  width: 256px;
}

figure.is-300x300 {
  height: 300px;
  width: 300px;
}

img.pfp {
  border: 5px solid $primary;
}
</style>
