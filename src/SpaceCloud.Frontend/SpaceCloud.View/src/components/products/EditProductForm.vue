<template>
  <div class="EditProductForm">
    <div class="wrapper">
      <div class="content-wrapper-with-subnav">
        <SubNavSide
          :topTitle="'New'"
          :topSubtitle="'Room/Desk'"
          :items="tabTitles"
          :checkmarks="checkmarks"
          :button="'Add new Product'"
          :activeItem="activeTab"
          v-on:item-clicked="changeActiveTab($event)"
          v-on:button-clicked="submit"
        />
        <div
          ref="scrollWrapper"
          id="content-wrapper"
          class="fullwidth scrollable"
        >
          <button
            v-on:click="nextTab"
            id="btn-next"
            class="button is-rounded is-primary is-fullwidth"
            :class="{ 'is-loading': loading }"
          >
            Next
          </button>
          <form>
            <!-- :v-on:change="formInputChanged" -->
            <!-- General -->
            <section id="tabGeneral" class="tab-wrapper">
              <TitleH2 text="General Information" />
              <div class="field">
                <div class="control">
                  <label class="label" for="name"
                    >Product Name *</label
                  >
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
              </div>
              <div>
                <div class="control">
                  <label class="label" for="category"
                    >Category *</label
                  >
                  <div class="select is-fullwidth">
                    <select
                      name="category"
                      v-model="product.category"
                    >
                      <option value="default" disabled
                        >Choose Product Category</option
                      >
                      <option value="r" selected>Room</option>
                      <option value="d">Desk</option>
                    </select>
                  </div>
                </div>
              </div>
              <div>
                <div class="control">
                  <label class="label" for="description"
                    >Description *</label
                  >
                  <textarea
                    id="description"
                    name="description"
                    class="textarea"
                    type="text"
                    v-model="product.description"
                    placeholder="Product Description"
                    v-validate="'required'"
                  />
                </div>
              </div>
              <!-- General: Room -->
              <div v-if="product.category === 'r'">
                <div class="field">
                  <div class="control">
                    <label class="label" for="capacity"
                      >Capacity *</label
                    >
                    <input
                      name="capacity"
                      class="input is-fullwidth"
                      type="text"
                      v-model="product.capacity"
                      placeholder="Capacity"
                      v-validate="'numeric'"
                    />
                  </div>
                </div>
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
              </div>
              <!-- General: Desk -->
              <div v-if="product.category === 'd'">
                <div class="field">
                  <div class="control">
                    <label class="label" for="amount">Amount</label>
                    <input
                      name="amount"
                      class="input is-fullwidth"
                      type="text"
                      v-model="product.stockAmount"
                      placeholder="Amount"
                      v-validate="'numeric'"
                    />
                    <p class="help">
                      Please specify how many Desks of this type you
                      want to create
                    </p>
                  </div>
                </div>
                <div class="field">
                  <div class="control">
                    <label class="label" for="intervalType"
                      >Interval Type</label
                    >
                    <div class="select is-fullwidth">
                      <select
                        name="category"
                        v-model="product.intervalType"
                      >
                        <option value="default" disabled
                          >Choose Interval Type</option
                        >
                        <option
                          v-for="(intv, i) in intervalTypes"
                          :value="i"
                          :key="i"
                          >{{ intv }}</option
                        >
                        <!-- <option value="4">Custom</option> -->
                      </select>
                    </div>
                  </div>
                </div>
              </div>
            </section>
            <!-- Images -->
            <section
              id="tabImages"
              class="tab-wrapper fullwidth scrollable"
            >
              <TitleH2 text="Product Images" />
              <div class="field">
                <ImageUpload
                  v-on:image-changed="
                    product.image = $event;
                    checkmarks[1] = true;
                  "
                />
                <p class="help is-danger">{{ errorMsgs.img }}</p>
              </div>
            </section>
            <!-- Opening Hours -->
            <section
              id="tabOpening"
              class="tab-wrapper fullwidth scrollable"
            >
              <TitleH2 text="Opening Hours" />
              <div
                v-if="product.category === 'r'"
                class="opening-hours-wrapper fullwidth"
              >
                <div class="opening-headers">
                  <p></p>
                  <p>Day</p>
                  <p>Open</p>
                  <p></p>
                  <p>Close</p>
                  <p></p>
                </div>

                <div v-for="n in 7" :key="n" class="opening-row">
                  <label class="checkmark flex-center">
                    <input
                      type="checkbox"
                      v-model="product.time.openings[n].active"
                    />
                  </label>
                  <div class="control opening-label">
                    <label
                      class="label"
                      :class="{
                        'text-grey': !product.time.openings[n].active,
                      }"
                      :for="'label' + weekday(n)"
                    >
                      {{ weekday(n) }}</label
                    >
                  </div>
                  <div class="control time-input">
                    <input
                      :name="'opening' + weekday(n)"
                      class="input"
                      type="text"
                      v-model="product.time.openings[n].open"
                      placeholder="7:00"
                      v-validate="'required'"
                      :disabled="!product.time.openings[n].active"
                    />
                  </div>
                  <span class="flex-center">-</span>
                  <div class="control time-input">
                    <input
                      :name="'opening' + weekday(n)"
                      class="input"
                      type="text"
                      v-model="product.time.openings[n].close"
                      placeholder="20:00"
                      v-validate="'required'"
                      :disabled="!product.time.openings[n].active"
                    />
                  </div>
                  <IconBtn type="add"></IconBtn>
                </div>
              </div>
              <p v-else>
                Opening hours can only be set for rooms.
              </p>
            </section>
            <!-- Pricing -->
            <section
              id="tabPricing"
              class="tab-wrapper fullwidth scrollable"
            >
              <TitleH2 text="Pricing" />
              <div class="field has-addons">
                <!-- <label class="label" for="priceH"
                >Price per Person per Hour</label
              > -->
                <p class="control">
                  <input
                    id="priceH"
                    name="priceH"
                    class="input"
                    type="text"
                    v-model="product.price"
                    placeholder="Price"
                    v-validate="'required|numeric'"
                  />
                </p>
                <p class="control">
                  <a class="button is-static">
                    € per
                    {{
                      product.category === 'r'
                        ? ' person per hour'
                        : ` desk per ${intervalTypes[
                            product.intervalType
                          ].toLowerCase()}`
                    }}
                  </a>
                </p>
              </div>
            </section>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import SubNavSide from '@/components/navs/SubNavSide';
import TitleH2 from '@/components/base/TitleH2';
import ImageUpload from '@/components/base/ImageUpload';
import IconBtn from '@/components/base/IconBtn';
import api from '@/services/api';

export default {
  name: 'EditProductForm',
  components: { SubNavSide, TitleH2, ImageUpload, IconBtn },
  data() {
    return {
      loading: false,
      activeTab: 0,
      showConfirmation: false,
      errorMsgs: {
        general: '',
        img: '',
      },
      tabTitles: ['General', 'Images', 'Opening Hours', 'Pricing'],
      intervalTypes: ['Day', 'Month', 'Quartal', 'Year'],
      tabWrappers: undefined,
      lockActiveTab: false,
      checkmarks: [false, false, true, false],
      product: {
        category: 'r',
        name: '',
        description: '',
        image: undefined,
        imageUri: '',
        price: 0,
        capacity: 10,
        time: {
          pid: 0,
          openings: [
            {},
            { day: '1', active: true, open: '7:00', close: '20:00' },
            { day: '2', active: true, open: '7:00', close: '20:00' },
            { day: '3', active: true, open: '7:00', close: '20:00' },
            { day: '4', active: true, open: '7:00', close: '20:00' },
            { day: '5', active: true, open: '7:00', close: '20:00' },
            { day: '6', active: true, open: '7:00', close: '20:00' },
            { day: '7', active: true, open: '7:00', close: '20:00' },
          ],
        },
        tagsStr: '',
        tags: [],
        stockAmount: 1,
        intervalType: 0,
      },
    };
  },
  mounted() {
    this.$refs.scrollWrapper.addEventListener(
      'scroll',
      this.handleScroll,
    );
    this.tabWrappers = document.querySelectorAll('.tab-wrapper');

    document
      .querySelectorAll(`form input, form select, form textarea`)
      .forEach(e => {
        e.addEventListener('input', this.formInputChanged);
      });
  },
  beforeDestroy() {
    this.$refs.scrollWrapper.removeEventListener(
      'scroll',
      this.handleScroll,
    );
    this.formInputs.forEach(e => {
      e.removeEventListener('input', this.formInputChanged);
    });
  },

  methods: {
    submit(evt) {
      if (evt) evt.preventDefault();

      if (
        this.product.category === 'r' &&
        this.product.tagsStr.trim().length > 1
      ) {
        this.product.tags = this.product.tagsStr
          .trim()
          .split(/\s*,\s*/);
      }

      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.message = 'Inputs are not Valid';
          console.log(this.message, ': ', this.errors.items[0].msg);
        } else {
          let confirmAlertHtml = `<p>Category: <span class="bold">${
            this.product.category === 'r' ? 'Room' : 'Desk'
          }</span></p>
        <p>Name: <span class="bold">${this.product.name}</span></p>
        <p>Description: <span class="bold">${
          this.product.description
        }</span></p>`;

          if (this.product.category === 'r')
            confirmAlertHtml += `<p>Price: <span class="bold">${this.product.price} per person per hour</span></p>
        <p>Capacity: <span class="bold">${this.product.capacity} People</span></p>`;
          else
            confirmAlertHtml += `
        <p>Price: <span class="bold">${
          this.product.price
        } per ${this.intervalTypes[
              this.product.intervalType
            ].toLowerCase()}</span></p>
        <p>Amount: <span class="bold">${
          this.product.stockAmount
        }</span></p>`;

          this.$swal({
            title: 'Do you want to save this Product?',
            html: confirmAlertHtml,
            icon: 'question',
            confirmButtonText: 'Yes',
            confirmButtonColor: '#1b53a8',
          }).then(res => {
            if (res.value) this.submitConfirmed();
          });
        }
      });
    },
    async submitConfirmed() {
      this.loading = true;

      // validate file uplaod
      await api
        .imgUpload(this.product.image)
        .then(response => {
          this.product.imageUri = response.data.uri;
          console.log(
            'Image successfully uploaded: ',
            response.data.uri,
          );
          if (this.product.category === 'r')
            this.product.time.openings.shift();
          this.$emit('formSubmit', this.product);
        })
        .catch(error => {
          if (error)
            this.errorMsgs.img =
              'Please use a jpg or png image smaller than 2 MB';
          else if (error.toString().search('Network Error') !== -1)
            this.errorMsgs.img =
              'Please make sure this file type is supported and it is smaller than the size limit';
          else this.errorMsgs.img = `Something went wrong: ${error}`;
          this.toggleConfirmation = false;
          this.loading = false;
          this.changeActiveTab('Images');
        });
    },

    // validate section and update checkmark in SubNav
    formInputChanged(evt) {
      this.errorMsgs = {
        general: '',
        img: '',
      };

      // get parent section
      let el = evt.target;
      const els = [];
      while (el.tagName !== 'SECTION') {
        els.unshift(el);
        el = el.parentElement;
      }
      this.validateSection(el.getAttribute('id'));
    },
    validateSection(id) {
      const v = this.$validator;
      const p = this.product;
      const { checkmarks } = this;
      // this.checkmarks[0] = false;
      if (id === 'tabGeneral') {
        v.validate('name').then(name => {
          if (name) {
            v.validate('description').then(description => {
              if (description) {
                if (p.category === 'r') {
                  v.validate('capacity').then(cap => {
                    checkmarks[0] = cap;
                  });
                } else if (p.category === 'd') {
                  v.validate('amount')
                    .then(amount => {
                      if (amount) {
                        v.validate('intervalType').then(
                          intervalType => {
                            checkmarks[0] = intervalType;
                          },
                        );
                      } else checkmarks[0] = false;
                    })
                    // desk-specific validation fields aren't registered due to conditional rendering (v-if category = r)
                    // if VeeValidate doesn't recognize these fields, I validate manually:
                    .catch(() => {
                      if (
                        /^\d+$/.test(p.stockAmount) &&
                        /^\d+$/.test(p.intervalType)
                      ) {
                        if (p.intervalType >= 0 && p.intervalType < 5)
                          checkmarks[0] = true;
                        else checkmarks[0] = false;
                      } else checkmarks[0] = false;
                    });
                }
              } else checkmarks[0] = false;
            });
          } else checkmarks[0] = false;
        });
      } else if (id === 'tabOpening') {
        if (this.product.category === 'd') checkmarks[2] = true;
        else {
          checkmarks[2] = false;
          this.product.time.openings.forEach(d => {
            if (d) if (d.active) checkmarks[2] = true;
          });
        }
      } else if (id === 'tabPricing') {
        v.validate('priceH').then(priceH => {
          checkmarks[3] = priceH;
        });
      }

      // this.$set(array, index, val)
      let i = 0;
      checkmarks.forEach(c => {
        if (c !== this.checkmarks[c])
          this.$set(this.checkmarks, i, c);
        i += 1;
      });

      // this.checkmarks = checkmarks;
    },

    // Update SubNav ActiveItem
    handleScroll() {
      let actTab = 0;
      let i = 0;
      this.tabWrappers.forEach(el => {
        i += 1;
        if (el.getBoundingClientRect().top < 200) actTab = i;
      });
      if (!this.lockActiveTab)
        this.activeTab = actTab < 1 ? 0 : actTab - 1;
    },
    // triggers at click on "Next" Button
    nextTab() {
      const nextTabIndex = this.checkmarks.findIndex(
        a => a === false,
      );
      if (nextTabIndex === -1) this.submit();
      else this.changeActiveTab(nextTabIndex);
    },
    // trieggers at click on SubNavItem or "Next" Button
    changeActiveTab(tab) {
      if (typeof tab === 'string')
        this.activeTab = this.tabTitles.findIndex(i => i === tab);
      else this.activeTab = tab;
      this.tabWrappers[this.activeTab].scrollIntoView({
        behavior: 'smooth',
      });
      this.lockActiveTab = true;
      setTimeout(() => {
        this.lockActiveTab = false;
      }, 2000);
    },
    toggleConfirmation() {
      this.showConfirmation = !this.showConfirmation;
    },
    weekday(i) {
      const weekday = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday',
        'Sunday',
      ];
      return weekday[i - 1];
    },
  },
};
</script>
<style scoped lang="scss">
// custom chechboxes (checkmarks)
.checkmark input[type='checkbox'] {
  content: url('../../assets/icons/checkmark.png');
  height: 25px;
  width: 25px;
  appearance: inherit;
}

.checkmark input[type='checkbox']:checked {
  content: url('../../assets/icons/checkmark-checked.png');
}

#content-wrapper {
  margin-bottom: 40px;
  padding: 30px 35px 20px 15px;
}

section {
  margin: 75px 0;
}

form :first-child {
  margin-top: 0;
}

form :last-child {
  margin-bottom: 0;
}

.tab-wrapper {
  min-height: 100%;
}

.EditProductForm {
  height: 100%;
  width: 100%;
}

#btn-next {
  position: absolute;
  bottom: 65px;
  right: 120px;
  z-index: 10;
  width: 200px;
}

// Opening Hours
.opening-hours-wrapper {
  margin-top: 40px;
}

.opening-hours-wrapper > div {
  display: grid;
  grid-template-rows: 1fr;
  grid-template-columns: 1fr 8fr 2fr 10px 2fr 1fr;
  grid-gap: 20px;

  width: 100%;
  margin: 20px auto;
}

.opening-hours-wrapper label {
  margin: 0 !important;
}

.opening-hours-wrapper .control {
  display: flex;
  align-items: center;
}

.opening-label > label {
  font-weight: 400;
  font-size: 1.2em;
}

.time-input input {
  text-align: center;
}

.opening-headers {
  font-weight: bold;
}
</style>
