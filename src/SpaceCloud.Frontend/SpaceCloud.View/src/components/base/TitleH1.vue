<template>
  <div class="TitleH1">
    <h1 class="title">{{ text }}</h1>
  </div>
</template>
<script>
export default {
  name: 'TitleH1',
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
div.TitleH1 {
  width: 100%;
  margin: 35px 20px;
  padding: 0 0 5px 10px;
  border-bottom: 1px solid black;
  position: sticky;
}

div.TitleH1 > h1.title {
  display: inline-block;
  color: $primary;
  font-weight: bold;
  font-size: 30px !important;
  letter-spacing: 0.2em;
}
</style>
