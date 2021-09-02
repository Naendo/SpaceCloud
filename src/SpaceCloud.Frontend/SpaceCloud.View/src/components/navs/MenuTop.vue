<template>
  <div class="MenuTop">
    <div class="menu-collapsed-wrapper">
      <div class="img-wrapper">
        <figure class="is-32x32">
          <img
            v-if="user.imageUri"
            class="pfp is-rounded"
            :src="user.imageUri"
          />
          <img
            v-else
            class="is-rounded"
            src="@/assets/icons/person-circled.svg"
            alt="Menu"
          />
        </figure>
      </div>
    </div>
    <div class="menu-expanded-wrapper">
      <div class="flex-center">C</div>
      <router-link :to="{ name: 'Cart' }"> Cart</router-link>

      <div></div>
      <div @click="clearConsole">Clear Console</div>

      <div class="flex-center">
        <input
          id="switchExample"
          type="checkbox"
          name="switchExample"
          class="switch"
          checked="checked"
        />
      </div>
      <div>Darkmode</div>

      <div></div>
      <div>Language</div>

      <div></div>
      <div class="logout" v-on:click="$store.dispatch('auth/logout')">
        Log Out
      </div>
    </div>
  </div>
</template>
<script>
import api from '@/services/api';

export default {
  name: 'MenuTop',
  components: {},
  data() {
    return {
      user: {},
    };
  },
  async mounted() {
    // prevent fetching user before jwt is ready (temp solution)
    setTimeout(this.getUser, 2000);
  },
  methods: {
    getUser() {
      api.getCurrentUser().then(
        response => {
          this.user = response.data;
        },
        error => {
          console.log('User could not be loaded: ', error);
          this.user = {
            imageUri:
              'https://dev.spacecloud.cc/img/person-outline.9ff295fa.svg',
            firstName: '- - -',
            lastName: '- - -',
          };
        },
      );
    },
    clearConsole() {
      console.clear();
    },
  },
};
</script>
<style scoped lang="scss">
.MenuTop {
  display: flex;
  position: relative;
  flex-direction: column;
  flex-wrap: nowrap;
  align-items: flex-end;
  height: 50px;
  transition: 0.15s ease-out;
  z-index: 1000;
  padding: 7px 10px 0;
}

.menu-collapsed-wrapper {
  display: flex;
  flex-wrap: nowrap;
  align-items: center;
  justify-content: flex-end;
  height: 100%;
}

.MenuTop:hover,
.menu-expanded-wrapper > div:hover {
  background-color: #e7e6eb;
}

.MenuTop:hover .menu-expanded-wrapper {
  display: grid;
}

.menu-expanded-wrapper {
  display: none;
  grid-template-rows: repeat(4, 50px);
  grid-template-columns: 70px 1fr;
  position: fixed;
  background-color: white;
  width: 250px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  font-weight: bold;
  top: 76px;
  right: 100px;
}

.menu-expanded-wrapper > * {
  display: flex;
  padding: 5px 10px;
  align-items: center;
}

.logout {
  cursor: pointer;
}
</style>
