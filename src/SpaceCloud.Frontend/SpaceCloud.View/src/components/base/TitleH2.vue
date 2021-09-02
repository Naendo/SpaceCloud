<template>
  <div class="TitleH2">
    <h2 class="title">{{ text }}</h2>
  </div>
</template>
<script>
export default {
  name: 'TitleH2',
  components: {},
  props: {
    text: String,
  },
  data() {
    return {};
  },
  methods: {},
};
</script>
<style scoped lang="scss">
div.TitleH2 {
  width: 100%;
  margin-bottom: 20px;
  padding: 0 0 5px 10px;
  border-bottom: 1px solid black;
}

div.TitleH2 > h2.title {
  display: inline-block;
  color: $primary;
  font-weight: bold;
  font-size: 26px !important;
  letter-spacing: 0.2em;
}
</style>
