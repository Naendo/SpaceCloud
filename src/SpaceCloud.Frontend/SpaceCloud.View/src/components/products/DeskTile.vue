<template>
  <div @click="bookNow($event)" class="DeskTile">
    <div class="desk-img-container flex-center">
      <img
        v-if="showImg"
        :class="{ imgAltBgColor: !showImg }"
        :src="desk.imageUri"
        @error="showImg = false"
        alt="Desk Preview"
      />
      <p v-else>Image not found</p>
    </div>
    <div class="desk-info">
      <TitleH3
        v-if="$store.getters['auth/isAdmin']"
        :text="desk.name"
        button="delete"
        v-on:button-click="deleteDesk"
      />
      <TitleH3 v-else :text="desk.name" />
      <!-- <h3 class="title is-3">{{ desk.name }}</h3> -->
      <div class="desk-info-text">
        <span class="container-nowrap">
          <span class="desk-info-bold">{{ desk.price }}€</span>
          <span>per {{ desk.interval }}</span>
        </span>
        <span class="container-nowrap">
          <span class="desk-info-bold">{{
            desk.availableAmount
          }}</span>
          <span>currently available</span>
        </span>
      </div>
      <button class="book-now button is-primary is-rounded">
        See more
      </button>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
import api from '@/services/api';

export default {
  name: 'DeskTile',
  props: ['desk'],
  components: { TitleH3 },
  data() {
    return {
      showImg: true,
    };
  },
  methods: {
    bookNow(ev) {
      if (!ev.target.parentElement.classList.contains('IconBtn')) {
        this.$router
          .push({
            name: 'Desk',
            params: { id: this.desk.productId },
          })
          // Ignore duplicate navigation router error
          .catch(err => {
            if (
              err.name === 'NavigationDuplicated' ||
              err.message.includes(
                'Avoided redundant navigation to current location',
              )
            )
              console.log('vue router: avoided redundant navigation');
            else console.log('DeskTile Error (?):', err);
          });
      }
    },
    deleteDesk() {
      this.$swal({
        title: 'Are you sure?',
        text: `Product #${this.desk.productId} will be permanently deleted!`,
        icon: 'warning',
        confirmButtonColor: '#f53f27',
        showCancelButton: true,
        confirmButtonText: 'Delete',
      }).then(res => {
        if (res.value) {
          api.deleteProduct(this.desk.productId).then(
            () => {
              this.$swal({
                text: `Product #${this.desk.productId} has been successfully deleted`,
                icon: 'success',
                confirmButtonColor: '#1b53a8',
              });

              this.$emit('remove-desk', this.desk.productId);
            },
            error => {
              if (error.message) {
                if (
                  error.response.data.message.includes(
                    'The association between entity types',
                  )
                ) {
                  this.deactivateDesk();
                  return;
                }
              }
              this.$swal({
                html: `Product #${this.desk.productId} could not be deleted<br />${error}`,
                icon: 'error',
                confirmButtonColor: '#1b53a8',
              });
            },
          );
        }
      });
    },
    deactivateDesk() {
      api.disableProduct(this.desk.productId).then(
        () => {
          this.$swal({
            html: `Product #${this.desk.productId} could not be deleted (existing orders).<br />The Product has been disabled instead.`,
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          });

          this.$emit('remove-desk', this.desk.productId);
        },
        error => {
          this.$swal({
            html: `Product #${this.desk.productId} could not be deactivated<br />${error}`,
            icon: 'error',
            confirmButtonColor: '#1b53a8',
          });
        },
      );
    },
  },
};
</script>
<style scoped lang="scss">
.DeskTile {
  display: flex;
  height: 220px;
  width: 98%;
  margin: 40px 0;
  line-height: 30px;
  letter-spacing: 2px;
  border-radius: 20px;
}

.DeskTile:hover {
  background-color: $hover-background;
  cursor: pointer;
}

.DeskTile > div {
  height: 100%;
}

.desk-img-container {
  width: 45%;
  max-width: 400px;

  border-radius: 20px 0 0 20px;

  overflow: hidden;
}

.desk-img-container img {
  min-width: 100%;
  width: auto;

  min-height: 100%;
  height: auto;

  object-fit: cover;

  border-radius: 20px 0 0 20px;
}

.img-alt-bgcolor {
  background-color: #eeeeee;
}

.desk-info {
  position: relative;
  width: 65%;
  padding: 20px;
  padding-left: 30px;
  // padding: 20px;
}

.desk-info-bold {
  font-weight: 500;
  font-size: 1.5em;

  margin: 0 0.5em 0 0.3em;
}

button.book-now {
  width: 150px;
  position: absolute;
  float: right;
  bottom: 20px;
  right: 20px;
}
</style>

<!--

<template>
  <div @click="bookNow()" class="DeskTile">
    <div class="desk-img-container flex-center">
      <img
        v-if="showImg"
        :class="{ imgAltBgColor: !showImg }"
        :src="desk.imageUri"
        @error="showImg = false"
        alt="Desk Preview"
      />
      <p v-else>Image not found</p>
    </div>
    <div class="desk-info">
      <h3 class="title is-3">{{ desk.name }}</h3>
      <div class="desk-info-text">
        <p>
          <span class="price">{{ desk.price }}€</span>
          <span>per person/hour</span>
        </p>
        <p class="capacity">Capacity: {{ desk.capacity }}</p>
        <p class="pid">pid: {{ desk.pid }}</p>
      </div>
      <button
        class="book-now button is-primary is-rounded"
        v-on:click="bookNow()"
      >
        Book now
      </button>
    </div>
  </div>
</template>
<script>
export default {
  name: 'DeskTile',
  props: ['desk'],
  components: {},
  data() {
    return { showImg: true };
  },
  methods: {
    // ROOM:
    bookNow() {
      this.$router.push({
        name: 'Desk',
        params: { desk: this.desk },
      });
    },
  },
};
</script>
<style scoped lang="scss">
.DeskTile {
  display: flex;
  height: 250px;
  width: 98%;
  margin: 30px 0;
  line-height: 30px;
  letter-spacing: 2px;
  background-color: #f9f9f9;
  border-radius: 20px;
}

.DeskTile:hover {
  background-color: $hover-background;
  cursor: pointer;
}

.DeskTile > div {
  height: 100%;
}

.DeskTile > .desk-info {
  width: 65%;
  padding: 20px;
  padding-left: 30px;
}

.desk-img-container {
  width: 350px;

  border-radius: 20px 0 0 20px;

  overflow: hidden;
}

.imgAltBgColor {
  background-color: #eeeeee;
}

.desk-img-container img {
  min-width: 100%;
  width: auto;

  min-height: 100%;
  height: auto;

  object-fit: cover;

  border-radius: 20px 0 0 20px;
}

.price {
  font-weight: 500;
  font-size: 1.5em;

  margin: 0 0.5em 0 0.3em;
}

button.book-now {
  width: 150px;
  float: right;
}
</style>
-->
