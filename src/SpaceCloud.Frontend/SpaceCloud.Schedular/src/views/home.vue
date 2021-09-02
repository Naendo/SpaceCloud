<template>
  <div :class="[sideBarActive ? 'grid-container' : 'grid-container-open-sidebar']">
    <!--<div class="grid-container">-->
    <nav id="nav-bar">
      <div id="nav-logo-container" class="nav-item">
        <img src="../assets/spacecloud_logo.svg" id="nav-logo"/>
      </div>
      <div id="nav-room" class="nav-item"></div>
      <div id="nav-clock" class="nav-item"></div>
    </nav>
    <main id="home-main">
      <div id="availability-container">
        <availability/>
      </div>
    </main>
    <aside id="schedule-container">
      <sidebar @myevent="toggleSidebar"/>
    </aside>

    <div id="reservation-container">
      <h2>Quick Reservation</h2>
      <div id="qr-container">
        <qrbutton ProductId="1" ValueInMinutes="15"></qrbutton>
        <qrbutton ProductId="1" ValueInMinutes="30"></qrbutton>
        <qrbutton ProductId="1" ValueInMinutes="60"></qrbutton>
      </div>
    </div>
  </div>
</template>


<script lang="ts">
import {defineComponent, onMounted, ref} from "vue";
import clockService from "../services/clockService";
import availability from "../components/availability.vue";
import qrbutton from "../components/qrbutton.vue";
import sidebar from "../components/sidebar.vue";
import {getTokenBody} from "../services/authorizationService";
import axiosService from "../services/axiosService";

export default defineComponent({
  name: "home",
  components: {availability, qrbutton, sidebar},
  props: {
    token: {
      required: true,
      types: [String],
    },
  },
  

  setup(props) {
    const roomName = ref<string>("Meeting Room");
    const timeFrom = clockService.run();
    let sideBarActive = ref<boolean>(false);
    const token = getTokenBody();
    let runtime = ref<Number>();

    onMounted(() => {
      axiosService.get("/scheduler/get/" + props.token, "bearer " + token?.token)
      .then(response =>
      runtime.value = response.data.meetings.runTime);
    });




    function toggleSidebar() {
      sideBarActive.value = !sideBarActive.value;
    }

    return {
      runtime,
      sideBarActive,
      roomName,
      timeFrom,
      toggleSidebar
    };
  },
});
</script>

<style scoped>
h2 {
  text-align: center;
}

.grid-container {
  display: grid;
  grid-template-columns: auto 4rem;
  grid-template-rows: 8vh 72vh 20vh;
  align-content: space-between;
  justify-content: center;
  width: 100vw;
}

.grid-container-open-sidebar {
  display: grid;
  grid-template-columns: auto 18rem;
  grid-template-rows: 8vh 72vh 20vh;
  align-content: space-between;
  justify-content: center;
  width: 100vw;
}

#schedule-container {
  grid-column-start: 2;
  grid-row-start: 2;
  grid-row-end: 4;
  width: 100%;
  background-color: #121212;
  grid-template-columns: 100%;
}

#burger-icon {
  width: 3rem;
  height: 3rem;
  border-radius: 50px;
  background-color: #18a4e0;
  margin: 0 auto;
}

#todays-schedule {
  transform: rotate(90deg);
  grid-row-start: 2;
  color: white;
  margin: 0;
  align-self: center;
  white-space: nowrap;
}

#nav-bar {
  grid-column-start: 1;
  grid-column-end: 3;
  color: white;
  width: 100vw;
  height: 100%;
  background-color: #121212;
  display: flex;
  align-items: center;
  justify-content: space-between;
  box-sizing: border-box;
  padding: 0.5rem;
}

#nav-logo {
  background-image: url("../assets/spacecloud_logo.svg");
  width: 220px;
  min-width: 150px;
}

.nav-logo-container {
  text-align: left !important;
}

#nav-clock {
  font-weight: 300;
  text-align: right;
}

#nav-room {
  text-align: center;
}

.nav-item {
  width: 25%;
  font-size: 1em;
}

#availability-container {
  grid-row-start: 2;
  width: 55vh;
  height: 55vh;
  /**
  border-radius: 50%;
  border: 15px solid #6fe00d;
  border-top-color: black;
  background: #1f1f1f;
  **/
  margin: 0 auto;
}

#home-main {
  display: flex;
  align-items: center;
  justify-content: center;
}

#qr-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

#reservation-container {
  grid-row-start: 3;
  grid-column-start: 1;
  grid-column-end: 2;
  color: white;
  display: flex;
  align-items: center;
  flex-direction: column;
}

#reservation-container > div {
  display: flex;
}

#reservation-container > h2 {
  font-size: 14px;
  width: 200px;
}

#reservation-container > h2:after {
  display: block;
  margin: 4px auto 0 auto;
  width: 60%;
  height: 0.5px;
  background-color: white;
  content: " ";
}

@media only screen and (max-width: 870px) {
  #schedule-container {
    display: none;
  }

  .grid-container {
    grid-template-rows: 8vh 65vh auto;
    grid-template-columns: 100%;
  }

  .grid-container-open-sidebar {
    grid-template-columns: 100%;
    grid-template-rows: 8vh 65vh auto;
  }

  #availability-container {
    width: 35vh;
    height: 35vh;
  }

  #qr-container {
    flex-direction: column;
  }

  #nav-logo {
    width: 150px;
  }
}

</style>
