<template>
  <div
    class="TitleH3"
    :style="color ? `border-bottom-color: ${color}` : ''"
  >
    <h3 class="title" :style="color ? `color: ${color}` : ''">
      {{ text }}
    </h3>
    <IconBtn
      :type="button"
      v-if="button"
      v-on:click="$emit('button-click')"
    />
  </div>
</template>
<script>
import IconBtn from '@/components/base/IconBtn';

export default {
  name: 'TitleH3',
  components: { IconBtn },
  props: {
    text: {
      type: String,
      default: '',
    },
    button: {
      type: String,
      default: '',
    },
    color: {
      type: String,
      defualt: '',
    },
  },
  data() {
    return {};
  },
};
</script>
<style scoped lang="scss">
div.TitleH3 {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  width: 100%;
  margin-bottom: 20px;
  padding: 0 0 5px 10px;
  border-bottom: 1px solid black;
}

div.TitleH3 > h3.title {
  display: inline-block;
  font-size: 26px !important;
  font-weight: 500; /* 500: Medium */
  letter-spacing: 0.2em;
  margin: 0;
}
</style>
