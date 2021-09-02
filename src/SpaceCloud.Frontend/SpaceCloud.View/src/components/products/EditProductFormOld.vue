<template>
  <div class="EditProductForm">
    <form @submit="Submit">
      <section class="section">
        <div class="field is-grouped">
          <div class="control width-45">
            <label class="label" for="name">Product Name</label>
            <input
              id="name"
              name="name"
              class="input is-fullwidth"
              type="text"
              v-model="product.name"
              placeholder="Flex Desk 1"
              v-validate="'required'"
            />
          </div>
          <div class="control width-45">
            <label class="label" for="price">Price</label>

            <div class="field price has-addons">
              <!--<p class="control">
                <span class="select">
                  <select>
                    <option>$</option>
                    <option>£</option>
                    <option>€</option>
                  </select>
                </span>
              </p>-->
              <div class="control">
                <input
                  id="price"
                  name="price"
                  class="input"
                  type="text"
                  v-model="product.price"
                  placeholder="20"
                  v-validate="'required'"
                />
              </div>
              <p class="control">
                <a class="button is-static">€ per Hours</a>
              </p>
            </div>
          </div>
        </div>
        <div class="field is-grouped">
          <div class="control width-45">
            <label class="label" for="description">Description</label>
            <input
              id="description"
              name="description"
              class="input"
              type="text"
              v-model="product.description"
              placeholder="Product Description"
            />
          </div>
          <div class="control width-45">
            <label class="label" for="imageUri">Product Image</label>
            <input
              id="imageUri"
              name="imageUri"
              class="input"
              type="link"
              v-model="product.imageUri"
              placeholder="Product Image Url"
            />
          </div>
        </div>
      </section>
      <section class="section category">
        <TitleH3 text="Category" />
        <div class="field fieldLeft">
          <div class="control">
            <label class="label" for="category">Category</label>
            <div class="select is-fullwidth">
              <select id="category" v-model="product.category">
                <option value="default" disabled
                  >Choose Product Category</option
                >
                <option value="r" selected>Room</option>
                <option value="d">Desk</option>
                <!-- <option value="o">Diverse</option> -->
              </select>
            </div>
          </div>

          <!-- DESK -->
          <div class="field" v-if="product.category == 'd'">
            <div class="control">
              <label class="label" for="amount">Amount</label>
              <input
                id="amount"
                name="amount"
                class="input"
                type="text"
                v-model="product.stockAmount"
                placeholder="5"
                v-validate="'required|numeric'"
              />
            </div>
          </div>
          <div class="field" v-if="product.category == 'd'">
            <div class="control">
              <label class="label" for="interval">intervalType</label>
              <input
                id="interval"
                name="interval"
                class="input"
                type="text"
                v-model="product.intervalType"
                placeholder="Interval Type"
                v-validate="'required|numeric'"
              />
            </div>
          </div>

          <!-- ROOM -->
          <div class="field" v-if="product.category == 'r'">
            <div class="control">
              <label class="label" for="capacity">Capacity</label>
              <input
                id="capacity"
                name="capacity"
                class="input"
                type="text"
                v-model="product.capacity"
                placeholder="5"
                v-validate="'required|numeric'"
              />
            </div>
          </div>
          <!--<div class="control width40">
              <div class="file has-name">
                <label class="file-label is-fullwidth">
                  <input class="file-input" type="file" />
                  <span class="file-cta">
                    <span class="file-label">Upload Images</span>
                  </span>
                  <span class="file-name is-fullwidth">{{ imgFilename }}</span>
                </label>
              </div>
          </div>-->
        </div>
        <div
          class="field fieldRight openings"
          v-if="product.category == 'r'"
        >
          <div class="field is-grouped openingLabels">
            <div class="control">
              <label class="label labelRight" for="day">Day</label>
            </div>
            <div class="control">
              <label class="label" for="open">Opening Time</label>
            </div>
            <div class="control">
              <label class="label" for="close">Closing Time</label>
            </div>
          </div>
          <div class="field is-grouped" v-for="i in 5" :key="i">
            <div class="control">
              <input
                disabled
                :id="'day' + i"
                name="day"
                class="input text-right"
                type="text"
                :value="weekday(i)"
              />
            </div>
            <div class="control">
              <input
                :id="'open' + i"
                name="open"
                class="input"
                type="text"
                v-model="product.time.openings[i].open"
                placeholder="07:00"
              />
            </div>
            <div class="control">
              <input
                :id="'close' + i"
                name="close"
                class="input"
                type="text"
                v-model="product.time.openings[i].close"
                placeholder="20:00"
              />
            </div>
          </div>
        </div>
      </section>

      <section
        id="Tags"
        class="section"
        v-if="product.category == 'r'"
      >
        <TitleH3 text="Tags" />
        <div class="field">
          <div class="control">
            <label class="label" for="tags">Tags</label>
            <input
              id="tags"
              name="tags"
              class="input"
              type="text"
              v-model="product.tagsStr"
              placeholder="Categorize your Products by labeling them with tags"
            />
            <p class="help">Seperate Tags with commas</p>
          </div>
        </div>
      </section>
      <div class="field is-grouped is-pulled-right">
        <div class="control">
          <button
            class="button is-primary is-outlined is-medium is-rounded"
            v-on:click="Reset()"
          >
            Reset
          </button>
        </div>
        <div class="control">
          <button class="button is-primary is-rounded is-medium">
            Save
          </button>
        </div>
      </div>
    </form>

    <!--modal confirmation-->
    <div v-if="showConfirmation" class="modal is-active is-clipped">
      <div class="modal-background"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">Create Product</p>
          <button
            v-on:click="toggleConfirmation()"
            class="delete"
            aria-label="close"
          ></button>
        </header>
        <section class="modal-card-body">
          <h3 class="title" is-3>
            Do you want to add this Product to the Database?
          </h3>
          <div class="confirmation-info-wrapper">
            <p>
              <span class="confirm-col-title">Name: </span>
              {{ product.name }}
            </p>
            <p>
              <span class="confirm-col-title">Category: </span>Room
            </p>
            <p>
              <span class="confirm-col-title">Price: </span
              >{{ product.price }},-€ per Hours
            </p>
            <p>
              <span class="confirm-col-title">Capacity: </span
              >{{ product.capacity + ' People' }}
            </p>
            <p>
              <span class="confirm-col-title">Open:</span>
              {{
                product.time.openings[1].open +
                  ' - ' +
                  product.time.openings[1].close
              }}
            </p>
            <p>
              <span class="confirm-col-title">Tags:</span
              >{{ product.tags }}
            </p>
          </div>
          <!--<pre>{{ JSON.stringify(product, null, 2) }}</pre>-->
        </section>
        <footer class="modal-card-foot">
          <div class="is-pulled-right">
            <button
              v-on:click="submitConfirmed"
              class="button is-primary"
            >
              Save
            </button>
            <button v-on:click="toggleConfirmation()" class="button">
              Cancel
            </button>
          </div>
        </footer>
      </div>
    </div>
  </div>
</template>
<script>
import TitleH3 from '@/components/base/TitleH3';
// import BtnPlus from '@/components/base/BtnPlus';

export default {
  name: 'EditProductForm',
  components: { TitleH3 /* , BtnPlus */ },
  data() {
    return {
      showConfirmation: false,
      imgFilename: '',
      product: {
        category: 'r',
        name: '',
        description: '',
        imageUri: '',
        price: 20,
        capacity: 10,
        time: {
          pid: 0,
          openings: [
            {},
            { day: '1', open: '7:00', close: '20:00' },
            { day: '2', open: '7:00', close: '20:00' },
            { day: '3', open: '7:00', close: '20:00' },
            { day: '4', open: '7:00', close: '20:00' },
            { day: '5', open: '7:00', close: '20:00' },
          ],
        },
        tagsStr: '',
        tags: [],
        stockAmount: 1,
        intervalType: 1,
      },
    };
  },
  methods: {
    Submit(evt) {
      evt.preventDefault();

      if (this.product.category === 'r') {
        this.product.tags = this.product.tagsStr
          .trim()
          .split(/\s*,\s*/);
      }

      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          console.log(this.message);
        } else {
          this.toggleConfirmation();
        }
      });
    },
    submitConfirmed() {
      if (this.product.category === 'r')
        this.product.time.openings.shift();
      this.$emit('formSubmit', this.product);
    },
    toggleConfirmation() {
      this.showConfirmation = !this.showConfirmation;
    },
    addAddon() {
      this.product.addons.push([]);
    },
    Reset() {
      this.product = {};
    },
    weekday(i) {
      const weekday = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
      ];
      return weekday[i - 1];
    },
  },
};
</script>
<style scoped lang="scss">
// only temporary classes
.confirmation-info-wrapper {
  width: 50%;
  margin: 0 auto;
}

.confirm-col-title {
  display: inline-block;
  font-weight: 600;
  width: 70px;
  margin: 0 20px 10px;
  text-align: right;
}

.control {
  margin: 10px;
}

.label {
  padding-left: 0.3em;
}

.width-45 {
  width: 45%;
}

label.file-label,
label.file-label span.file-name {
  width: 100%;
}

form .field.price * {
  margin: 0;
}

section:first-of-type {
  padding-top: 0;
}

section.category {
  min-height: 320px;
}

div.control.is-half-width {
  width: 50%;
}

div.fieldLeft,
div.field.fieldRight {
  display: inline-block;
  width: 45%;
}

div.fieldRight div.control {
  width: 30%;
  min-width: 100px;
  margin-top: 0;
}

div.fieldRight {
  float: right;

  margin-right: 5%;
}

.openings div.field,
.openings div.control {
  margin-bottom: 2px;
}

.openingLabels,
.openingLabels label {
  text-align: center;
  margin-bottom: 0 !important;
  margin-left: 0;
  padding-left: 0;
}

.openings input {
  text-align: center;
}

.text-right {
  text-align: right !important;
}

div.room div.control,
div.room div.control label {
  margin-top: 0 !important;
}

.modal-card-body {
  padding: 30px !important;
  letter-spacing: 0px !important;
}
</style>
