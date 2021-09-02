<template>
  <div class="Error404">
    <div class="contentContainer">
      <TitleH1 text="Oops!" />
      <div class="absolute-center">
        <p>Something went wrong!</p>
        <p>The requested URL {{ this.$route }} does not exist :(</p>
        <p>{{ this.$route.params.pathMatch }}</p>
      </div>
    </div>
  </div>
</template>
<script>
import TitleH1 from '@/components/base/TitleH1';

export default {
  name: 'Error404',
  components: { TitleH1 },
  data() {
    return {};
  },
  methods: {},
};
</script>
<style scoped lang="scss">
.contentContainer {
  height: 100%;
  width: 100%;
  border-radius: 20px;
  background-color: $tile-background;
  overflow-y: auto;
  padding: 20px 35px;
}
</style>
