<template>
  <div class="NewProduct">
    <div class="wrapper">
      <!-- <TitleH1 text="Create new Product" /> -->
      <EditProductForm v-on:formSubmit="createProduct($event)" />
    </div>
  </div>
</template>
<script>
import api from '@/services/api';
// import TitleH1 from '@/components/base/TitleH1';
import EditProductForm from '@/components/products/EditProductForm';

export default {
  name: 'NewProduct',
  components: { EditProductForm },
  data() {
    return {};
  },
  methods: {
    async createProduct(product) {
      const { user } = this.$store.state.auth;
      if (user == null) {
        this.$swal({
          html: `Seems like you're not logged in<br />Please try to log in again`,
          icon: 'error',
          confirmButtonColor: '#1b53a8',
        }).then(this.$store.dispatch('auth/logout'));
      }

      if (product.category === 'r') {
        api
          .createRoom(product)
          .then(() => {
            this.$swal({
              text: `Room has been successfully created`,
              icon: 'success',
              confirmButtonColor: '#1b53a8',
            });
            this.$router.push({ name: 'Products' });
          })
          .catch(error => {
            this.$swal({
              title: `Something went wrong`,
              text: error,
              icon: 'error',
              confirmButtonColor: '#1b53a8',
            });
          });
      } else if (product.category === 'd') {
        api
          .createDesk(product)
          .then(() => {
            this.$swal({
              text: `Desk has been successfully created`,
              icon: 'success',
              confirmButtonColor: '#1b53a8',
            });
            this.$router.push({ name: 'Products' });
          })
          .catch(error => {
            console.log('An error ocurred: ', error);
          });
      } else
        console.error(
          'Something went wrong! (Product Category is neither room nor desk!)',
        );
    },
  },
};
</script>
<style scoped lang="scss">
.TitleH1 {
  margin-bottom: 0px !important;
}
</style>
