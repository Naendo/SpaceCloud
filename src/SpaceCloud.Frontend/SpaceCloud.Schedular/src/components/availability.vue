<template>
  <div id="clock-container">
    <div class="base-timer">
      <svg
        class="base-timer__svg"
        viewBox="0 0 100 100"
        xmlns="http://www.w3.org/2000/svg"
      >
        <g class="base-timer__circle">
          <circle class="base-timer__path-elapsed" cx="50" cy="50" r="45" />
          <path
            id="base-timer-path-remaining"
            :stroke-dasharray="dasharray"
            class="base-timer__path-remaining ${remainingPathColor}"
            d="
          M 50, 50
          m -45, 0
          a 45,45 0 1,0 90,0
          a 45,45 0 1,0 -90,0
        "
          ></path>
        </g>
      </svg>
      <span id="base-timer-label" class="base-timer__label"
        ><div id="clock" class="border-gradient">
          <div id="clock-free-for">FREE FOR</div>
          <div id="clock-time">{{ test }}</div>
          <div id="sub-headline">NEXT MEETING</div>
          <div id="next-meeting-title">Daily Scrum Meeting</div>
          <div id="next-meeting-time">18:00 - 20:00</div>
        </div>
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";

export default defineComponent({
  name: "availability",

  props: {
    hasMeeting: Boolean,
    timeLimit: Number,
  },

  setup(props) {
    const roomName = ref<string>("roomName");
    //const time = ref<string>(clockService.run());

    let dasharray = ref<string>("283");

    const TIME_LIMIT = props.timeLimit;
    let timePassed = 0;
    let timeLeft = TIME_LIMIT;
    let timeLeftMinutes = Math.floor(timeLeft / 60);

    /*const COLOR_CODES = {
      info: {
        color: "green",
      },
    };*/

    //let remainingPathColor = COLOR_CODES.info.color;

    let test = ref<String>("asdf");

    onMounted(() => {
      //time.value = clockService.runtime();
      //setInterval(() => (time.value = clockService.runtime()), 1000);
      test.value = formatTime(timeLeftMinutes);
    });
    startTimer();

    function startTimer() {
      setInterval(() => {
        // The amount of time passed increments by one
        if (timeLeft > 0) {
          timePassed = timePassed += 1;
          timeLeft = TIME_LIMIT - timePassed;
          timeLeftMinutes = Math.floor(timeLeft / 60);
        }

        // The time left label is updated
        test.value = formatTime(timeLeftMinutes);
        if (timeLeft < 3600) {
          setCircleDasharray();
        }
      }, 1000);
    }

    function formatTime(time: number) {
      let hours = Math.floor(time / 60);
      let minutes = time % 60;

      // If the value of seconds is less than 10, then display seconds with a leading zero
      if (minutes < 10) {
        var minutesString = `0${minutes}`;
      } else {
        minutesString = `${minutes}`;
      }
      // The output in MM:SS format
      return `${hours}:${minutesString}`;
    }

    function calculateTimeFraction() {
      return timeLeft / 3600;
    }

    // Update the dasharray value as time passes, starting with 283

    function setCircleDasharray() {
      const circleDasharray = `${(calculateTimeFraction() * 283).toFixed(
        2
      )} 283`;

      dasharray.value = circleDasharray;
    }

    return {
      dasharray,
      test,
      roomName
    };
  },
});
</script>

<style scoped lang="scss">
/* Sets the containers height and width */
.base-timer {
  position: relative;
  height: 100%;
  width: 100%;
}

.base-timer__label {
  position: absolute;
  /* Size should match the parent container */
  width: 100%;
  height: 100%;

  /* Keep the label aligned to the top */
  top: 0;

  /* Create a flexible box that centers content vertically and horizontally */
  display: flex;
  align-items: center;
  justify-content: center;

  /* Sort of an arbitrary number; adjust to your liking */
  font-size: 48px;
}

/* Removes SVG styling that would hide the time label */
.base-timer__circle {
  fill: none;
  stroke: none;
}

/* The SVG path that displays the timer's progress */
.base-timer__path-elapsed {
  stroke-width: 4px;
  stroke: #121212;
}

.base-timer__path-remaining {
  /* Just as thick as the original ring */
  stroke-width: 4px;

  /* Rounds the line endings to create a seamless circle */
  stroke-linecap: round;

  /* Makes sure the animation starts at the top of the circle */
  transform: rotate(90deg);
  transform-origin: center;

  /* One second aligns with the speed of the countdown timer */
  transition: 1000ms linear all;

  /* Allows the ring to change color when the color value updates */
  stroke: #6fe00d;
}

.base-timer__svg {
  /* Flips the svg and makes the animation to move left-to-right */
  transform: scaleX(-1);
}

#clock-container {
  display: flex;
  align-items: center;
  width: 100%;
  height: 100%;
  color: white;

  #clock {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin: 0 auto;
    height: 80%;
    letter-spacing: 3px;
    font-size: 18px;
    text-align: center;

    #clock-free-for {
      font-size: 20px;
    }

    #clock-time {
      font-size: 100px;
      letter-spacing: 10px;
    }

    #sub-headline {
      margin-top: 30px;
      margin-bottom: 15px;
    }

    #sub-headline:after {
      display: block;
      content: " ";
      background-color: white;
      height: 1px;
      width: 100%;
      margin: 0 auto;
    }
  }
}
@media only screen and (max-width: 870px) {
  #clock-container {
    #clock {
      font-size: 14px;

      #clock-free-for {
        font-size: 14px;
      }

      #clock-time {
        font-size: 50px;
        letter-spacing: 10px;
      }

      #sub-headline {
        margin-top: 25px;
        margin-bottom: 10px;
      }
    }
  }
}
</style>
