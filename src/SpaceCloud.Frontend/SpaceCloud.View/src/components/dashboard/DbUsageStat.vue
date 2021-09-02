<template>
  <div class="DbUsageStat">
    <div class="heading-wrapper">
      <h3 class="title is-3">Coworking usage</h3>
    </div>
    <div class="stat">
      <h4 class="title is-4">{{ value + ' / ' + max }}</h4>
      <progress
        class="progress is-large is-primary"
        :value="value"
        :max="max"
        >{{ value + ' / ' + max }}</progress
      >
    </div>
  </div>
</template>
<script>
export default {
  name: 'DbUsageStat',
  components: {},
  data() {
    return {};
  },
  props: {
    value: {
      type: Number,
      default: 0,
    },
    max: {
      type: Number,
      default: 0,
    },
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.DbUsageStat {
  // width: 330px;

  padding: 25px;
}

.heading-wrapper {
  width: 100%;
  text-align: center;
  margin-top: 30px;
}

.stat {
  height: 80%;
  padding: 60px 0;

  text-align: center;
}
</style>
