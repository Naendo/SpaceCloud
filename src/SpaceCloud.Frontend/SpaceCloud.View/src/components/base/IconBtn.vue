<template>
  <div class="IconBtn">
    <button
      class="button is-primary is-rounded IconBtn"
      type="button"
      @click="$emit('click')"
    >
      <!-- Delete -->
      <img
        v-if="type === 'delete' || type === 'del'"
        src="@/assets/icons/btn-delete.svg"
        alt="Delete"
      />
      <!-- Edit -->
      <img
        v-else-if="type === 'edit'"
        src="@/assets/icons/btn-edit.svg"
        alt="Edit"
      />
      <!-- Favourite -->
      <img
        v-else-if="
          type === 'favourite' || type === 'fav' || type === 'like'
        "
        src="@/assets/icons/btn-edit.svg"
        alt="Edit"
      />
      <!-- Plus -->
      <img
        v-else-if="type === 'plus' || type === 'add'"
        src="@/assets/icons/btn-plus.svg"
        alt="Add"
      />
      <!-- Download -->
      <img
        v-else-if="type === 'download' || type === 'dl'"
        src="@/assets/icons/download.svg"
        alt="Download"
      />
      <p v-else></p>
    </button>
  </div>
</template>
<script>
export default {
  name: 'IconBtn',
  components: {},
  data() {
    return {};
  },
  props: {
    type: {
      type: String,
      default: '',
    },
  },
};
</script>

<style scoped lang="scss">
.IconBtn {
  height: 30px;
  width: 30px;
}

.IconBtn button {
  height: 30px;
  width: 30px;
  padding: 0.4em !important;
}
</style>
