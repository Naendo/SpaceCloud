<template>
  <div @click="generateQR" class="qr-button">{{ valueInMinutes }} MIN</div>

  <div
    @keyup.esc.stop="toggleState"
    v-focus
    @focusout="toggleState"
    ref="img"
    tabindex="-1"
    v-show="showUri"
    id="img-container"
  >
    <div class="img-container-toggle-button" @click="toggleState">
      <span class="fas fa-times fa-2x"></span>
    </div>
    <img :src="uri" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  name: "qr-button",
  props: {
    ProductId: {
      types: [Number, String],
      required: true,
    },
    ValueInMinutes: {
      types: [Number, String],
      required: true,
    },
  },

  setup(props) {
    const valueInMinutes = ref<Number | unknown>(props.ValueInMinutes);
    const roomId = ref<Number | unknown>(props.ProductId);
    let uri = ref<string>("");
    let showUri = ref(false);

    async function generateQR() {
      showUri.value = true;
      const baseUri = "https://chart.googleapis.com/chart?cht=qr&";
      const encodedUri = encodeURI(
        "https://www.spacecloud.cc&chs=400x400&choe=UTF-8&chld=L|2'"
      );
      uri.value = baseUri + "chl=" + encodedUri;
    }

    function toggleState() {
      showUri.value = false;
    }

    return {
      valueInMinutes,
      roomId,
      uri,
      showUri,
      generateQR,
      toggleState,
    };
  },
});
</script>

<style scoped lang="scss">
.qr-button {
  color: black;
  background: #18a4e0;
  width: 200px;
  height: 40px;
  border-radius: 3px;
  margin: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

$img-container-width: 40%;
$img-container-height: 40%;
#img-container {
  top: 50% - ($img-container-height / 2);
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  height: $img-container-height;
  width: $img-container-width;
  background: white;

  img {
    height: 250px;
    width: 250px;
  }

  .img-container-toggle-button {
    position: absolute;
    top: 0;
    right: 0;
    padding: 5px 10px;

    span {
      height: 100%;
      width: 100%;
      color: black;
    }

    :hover {
      cursor: pointer;
    }
  }
}
</style>
