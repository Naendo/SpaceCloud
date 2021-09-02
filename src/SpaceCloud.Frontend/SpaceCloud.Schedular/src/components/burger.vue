<template>
  <div :class="{ active: isBurgerActive }">
    <slot>

    </slot>
  </div>
</template>
<script lang="ts">
import {defineComponent, ref} from "vue";

export default defineComponent({
  name: "burger",

  setup() {
    const isBurgerActive = ref<boolean>(false);

    return {
      isBurgerActive,
    };
  },
});
</script>
<style scoped>
.hidden {
  visibility: hidden;
}

button {
  cursor: pointer;
}

/* remove blue outline */
button:focus {
  outline: 0;
}

.burger-button {
  position: relative;
  height: 30px;
  width: 32px;
  display: block;
  z-index: 999;
  border: 0;
  border-radius: 0;
  background-color: transparent;
  pointer-events: all;
  transition: transform 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
}

</style>
