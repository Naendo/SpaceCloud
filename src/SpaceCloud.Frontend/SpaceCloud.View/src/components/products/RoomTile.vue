<template>
  <div @click="bookNow($event)" class="RoomTile">
    <div class="room-img-container flex-center">
      <img
        v-if="showImg"
        :class="{ imgAltBgColor: !showImg }"
        :src="room.imageUri"
        @error="showImg = false"
        alt="Room Preview"
      />
      <p v-else>Image not found</p>
    </div>
    <div class="room-info">
      <TitleH3
        v-if="$store.getters['auth/isAdmin']"
        :text="room.name"
        button="delete"
        v-on:button-click="deleteRoom"
      />
      <TitleH3 v-else :text="room.name" />
      <!-- <h3 class="title is-3">{{ room.name }}</h3> -->
      <div class="room-info-text">
        <span class="container-nowrap">
          <span class="room-info-bold">{{ room.price }}€</span>
          <span>per person/hours</span>
        </span>
        <span class="container-nowrap">
          <span v-if="room.capacity === 1" class="room-info-bold"
            >1 Person</span
          >
          <span v-else class="room-info-bold"
            >{{ room.capacity }} People</span
          >
          <span>capacity</span>
        </span>
        <div class="tags-container">
          <ProductTagList :tags="room.tags" />
        </div>
        <!-- <p class="capacity">Capacity: {{ room.capacity }}</p>
        <p class="pid">pid: {{ room.pid }}</p> -->
      </div>
      <button class="book-now button is-primary is-rounded">
        See more
      </button>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
import ProductTagList from '@/components/products/ProductTagList';
import api from '@/services/api';

export default {
  name: 'RoomTile',
  props: ['room'],
  components: { TitleH3, ProductTagList },
  data() {
    return {
      showImg: true,
    };
  },
  methods: {
    // ROOM:
    bookNow(ev) {
      if (!ev.target.parentElement.classList.contains('IconBtn')) {
        this.$router
          .push({
            name: 'Room',
            params: { id: this.room.productId },
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
            else console.log('Roomtile Error (?):', err);
          });
      }
    },
    deleteRoom() {
      this.$swal({
        title: 'Are you sure?',
        text: `Product #${this.room.productId} will be permanently deleted!`,
        icon: 'warning',
        confirmButtonColor: '#f53f27',
        showCancelButton: true,
        confirmButtonText: 'Delete',
      }).then(res => {
        if (res.value) {
          api.deleteProduct(this.room.productId).then(
            () => {
              this.$swal({
                text: `Product #${this.room.productId} has been successfully deleted`,
                icon: 'success',
                confirmButtonColor: '#1b53a8',
              });

              this.$emit('remove-room', this.room.productId);
            },
            error => {
              if (error.message) {
                console.log(error.response.data.message);
                if (
                  error.response.data.message ===
                  'product-has-active-order'
                ) {
                  this.deactivateRoom();
                  return;
                }
              }
              this.$swal({
                html: `Product #${this.room.productId} could not be deleted<br />${error}`,
                icon: 'error',
                confirmButtonColor: '#1b53a8',
              });
            },
          );
        }
      });
    },
    deactivateRoom() {
      api.disableProduct(this.room.productId).then(
        () => {
          this.$swal({
            html: `Product #${this.room.productId} <b>could not be deleted</b> (existing orders).<br />The Product has been <b>disabled</b> instead.`,
            icon: 'success',
            confirmButtonColor: '#1b53a8',
          });

          this.$emit('remove-room', this.room.productId);
        },
        error => {
          this.$swal({
            html: `Product #${this.room.productId} could not be deactivated<br />${error}`,
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
.RoomTile {
  display: flex;
  height: 250px;
  width: 98%;
  margin: 30px 0;
  line-height: 30px;
  letter-spacing: 2px;
  border-radius: 20px;
}

.RoomTile:hover {
  background-color: $hover-background;
  cursor: pointer;
}

.RoomTile > div {
  height: 100%;
}

.room-img-container {
  width: 45%;
  max-width: 400px;

  border-radius: 20px 0 0 20px;

  overflow: hidden;
}

.room-img-container img {
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

.room-info {
  position: relative;
  width: 65%;
  padding: 20px;
  padding-left: 30px;
}

.room-info-bold {
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
