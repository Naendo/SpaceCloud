<template>
  <div class="SubNavSide">
    <div class="sub-nav-side-container">
      <div class="sub-nav-side-top">
        <p
          v-if="topTitle !== undefined"
          class="sub-nav-side-top-title"
        >
          {{ topTitle }}
        </p>
        <p
          v-if="topTitle !== undefined"
          class="sub-nav-side-top-subtitle"
        >
          {{ topSubtitle }}
        </p>
      </div>
      <div
        v-for="(item, i) in items"
        :key="i"
        v-on:click="
          $emit('item-clicked', item), (activeItemIndex = i)
        "
        class="sub-nav-side-item"
        :class="{ 'item-is-active': activeItemIndex == i }"
      >
        <label
          v-if="checkmarks"
          class="checkbox checkmark flex-center"
        >
          <input
            type="checkbox"
            onclick="return false;"
            v-model="checkmarks[i]"
          />
        </label>
        {{ item }}
      </div>
    </div>
    <button
      v-if="button"
      class="btn-bottom button is-rounded is-primary is-outlined"
      v-on:click="$emit('button-clicked')"
    >
      {{ button }}
    </button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      activeItemIndex: 0,
    };
  },
  props: {
    topTitle: [String, Number],
    topSubtitle: String,
    items: Array,
    button: String,
    activeItem: {
      type: Number,
      default: 0,
    },
    checkmarks: {
      type: [Array, Boolean],
      default: false,
    },
  },
  watchers: {
    activeItem(val) {
      this.activeItemIndex = val;
    },
  },
};
</script>

<style lang="scss" scoped>

// custom chechboxes (checkmarks)
.checkmark input[type='checkbox'] {
  content: url('../../assets/icons/checkmark.png');
  height: 25px;
  width: 25px;
  margin-right: 10px;
  display: inline-block;
  appearance: inherit;
}

.checkmark input[type='checkbox']:checked {
  content: url('../../assets/icons/checkmark-checked.png');
}

.SubNavSide {
  display: inline-flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;

  width: 25%;
  min-width: 190px;
  margin-right: 2.5em;
  padding: 0 35px 20px 0;

  border-right: 2px solid black;
}

.sub-nav-side-top {
  height: 115px;
  padding: 10px 0 20px;

  text-align: center;
  border-bottom: 1px solid black;
}

.sub-nav-side-top-title {
  font-size: 60px;
  font-weight: 500;
  color: $green;

  line-height: 60px;
  letter-spacing: 5px;
}

.sub-nav-side-top-subtitle {
  color: $text-grey;
  letter-spacing: 3px;
}

.sub-nav-side-container {
  width: 100%;
}

.sub-nav-side-item {
  cursor: pointer;

  display: flex;
  flex-wrap: nowrap;
  align-items: center;

  font-size: 18px;
  padding: 15px 0;
  padding-left: 10px;

  border-bottom: 1px solid #929292;
  border-collapse: collapse;
}

// .checkbox {
//   margin-right: 10px;
//   display: inline-block;
// }

// input[type='checkbox'] {
//   display: none;
// }

// .checkbox::before {
//   content: url('../../assets/logo_icon.svg');
//   display: inline-block;
//   height: 10px;
//   font-size: inherit;
//   text-rendering: auto;
//   -webkit-font-smoothing: antialiased;
// }

// .checkbox:checked::before {
//   content: url('../../assets/logo_icon.svg');
// }

.sub-nav-side-item.item-is-active {
  color: $primary;
  font-weight: bold;
}

.sub-nav-side-item:hover {
  background-color: $background;
}

.item {
  display: block;
  width: 100%;

  margin: 15px 0;
}
</style>
