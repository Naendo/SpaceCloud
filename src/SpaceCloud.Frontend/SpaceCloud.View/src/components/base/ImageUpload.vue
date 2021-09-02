<template>
  <div class="ImageUpload">
    <label class="label" for="image">{{ labelTxt }}</label>
    <div class="control file has-name">
      <label class="file-label">
        <input
          class="file-input"
          type="file"
          accept="image/png,image/jpeg"
          name="resume"
          @change="
            selectFile();
            $emit('image-changed', image);
          "
          ref="file"
        />
        <span class="file-cta">
          <!-- Icon for File Upload
          <span class="file-icon">
              <i class="fas fa-upload"></i>
          </span> -->
          <span class="file-label">
            Choose a file…
          </span>
        </span>
        <span class="file-name">
          {{ imageName }}
        </span>
      </label>
    </div>

    <div class="help">supported files: jpg or png, max. 2 MB</div>
  </div>
</template>
<script>
export default {
  name: 'ImageUpload',
  props: ['labelTxt'],
  data() {
    return {
      image: '',
      imageName: 'no file selected',
    };
  },
  methods: {
    selectFile() {
      // eslint-disable-next-line prefer-destructuring
      this.image = this.$refs.file.files[0];
      this.imageName = this.$refs.file.files[0].name;
    },
  },
};
</script>
<style scoped lang="scss">
</style>
