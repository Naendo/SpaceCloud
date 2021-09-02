<template>
  <div class="SubNavTop">
    <div
      v-for="(item, i) in items"
      :key="i"
      v-on:click="
        $emit('item-clicked', item);
        activeItem = i;
      "
      class="sub-nav-top-item"
      :class="{ 'item-is-active': activeItem == i }"
    >
      {{ item }}
    </div>
    <button
      v-if="button"
      class="sub-nav-top-btn button is-rounded is-primary"
      v-on:click="$emit('button-clicked')"
    >
      {{ button }}
    </button>
  </div>
</template>
<script>
export default {
  name: 'SubNavTop',
  data() {
    return {
      activeItem: 0,
    };
  },
  props: ['items', 'button'],
};
</script>
<style scoped lang="scss">
.SubNavTop {
  width: 100%;
  padding: 10px 0 20px;
  margin: 20px 0;

  display: flex;
  align-items: center;
  flex-wrap: nowrap;

  border-bottom: 1px solid #929292;
}

.sub-nav-top-item {
  cursor: pointer;

  display: inline-block;
  margin: 0 30px;
}

.SubNavTop :first-child {
  margin-left: 0;
}

.SubNavTop :last-child {
  margin-right: 0;
}

.sub-nav-top-item.item-is-active {
  color: $primary;
  font-weight: bold;
}

.sub-nav-top-btn {
  margin-left: auto;
}
</style>
