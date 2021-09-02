<template>
  <div class="ProductTagList" v-if="tags">
    <div
      class="product-tag"
      v-for="tag in tags.filter(t => t.name !== '')"
      :key="tag.tagId"
    >
      <span>{{ tag.name }}</span>
    </div>
  </div>
</template>
<script>
export default {
  name: 'name',
  components: {},
  data() {
    return {};
  },
  props: ['tags'],
};
</script>
<style scoped lang="scss">
.ProductTagList {
  margin: 10px 0;
}

.product-tag {
  display: inline-block;
  font-size: 16px;

  padding: 0 10px;
  text-align: center;
  margin: 0 10px 5px 0;

  border: 1px solid black;
  border-radius: 50px;
}
</style>
