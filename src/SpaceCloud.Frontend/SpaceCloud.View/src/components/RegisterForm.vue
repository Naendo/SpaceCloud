<!-- old login page (12.07.2020) -->

<template>
  <div class="registerform">
    <form @submit="handleRegister" @reset="onReset" v-if="show">
      <div class="field is-grouped">
        <div class="control">
          <label class="label" for="firstName">First Name</label>
          <input
            name="firstName"
            autocomplete="given-name"
            class="input"
            type="text"
            v-model="user.firstName"
            placeholder="First Name"
            v-validate="'required'"
          />
        </div>
        <div class="control">
          <label class="label" for="lastName">Last Name</label>
          <input
            name="lastName"
            autocomplete="family-name"
            class="input"
            type="text"
            v-model="user.lastName"
            placeholder="Last Name"
            v-validate="'required'"
          />
        </div>
      </div>
      <div class="field">
        <label class="label" for="mail">E-Mail</label>
        <div class="control">
          <input
            name="mail"
            autocomplete="email"
            class="input"
            type="text"
            v-model="user.mail"
            placeholder="E-Mail"
            v-validate="'required|email'"
          />
        </div>
        <p class="help">
          We'll never share your email with anyone else
        </p>
      </div>
      <div class="field">
        <label class="label" for="password">Password</label>
        <div class="control">
          <input
            name="password"
            autocomplete="new-password"
            class="input"
            type="password"
            v-model="user.password"
            placeholder="Password"
            v-validate="'required|min:8'"
          />
          <!--<span class="icon is-small is-left">
            <i class="fas fa-user"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>-->
        </div>
        <p class="help">Your Password must be 8 characters long</p>
      </div>
      <div class="field select">
        <div class="select">
          <select>
            <option name="locationId" value="1">Lustenau</option>
          </select>
        </div>
      </div>
      <div class="field">
        <label class="checkbox">
          <input
            type="checkbox"
            name="terms"
            v-validate="'required:true'"
            v-model="terms"
          />
          I agree to the
          <a href="#">terms and conditions</a>
        </label>
      </div>
      <!--<div class="field formError">
        <p v-if="errors.length" class="help is-danger">
          <b>Please correct the following error(s):</b>
          <ul>
            <li v-for="error in formErrors">{{ error }}</li>
          </ul>
        </p>
        <p class="help formError is-danger">loading: {{ loading }}</p>
      </div>
      <div class="field formError">
        <p class="help formError is-danger">message: {{ message }}</p>
      </div>
      <div class="field formError">
        <p class="help formError is-danger">
          successful: {{ successful }}
        </p>
      </div>-->
      <div class="control">
        <button type="submit" class="button is-link">
          Create Account
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import User from '@/models/user';

export default {
  name: 'RegisterForm',
  props: { showDataRes: Boolean },
  data() {
    return {
      user: new User('', '', '', '', true, 1),
      terms: true,
      message: '',
      submitted: false,
      successful: false,
      show: true,
      loading: false,
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.loggedIn;
    },
  },
  mounted() {
    if (this.loggedIn) {
      this.$router.push({ name: 'Dashboard' });
    }
  },
  methods: {
    handleRegister(evt) {
      evt.preventDefault();
      this.message = '';
      this.loading = true;
      this.submitted = true;
      this.$validator
        .validate()
        .then(isValid => {
          if (isValid) {
            console.log(this.user.locationId);
            this.$store.dispatch('auth/register', this.user).then(
              data => {
                this.message = data;
                this.successful = true;
                this.$store.dispatch('auth/login', this.user).then(
                  () => {
                    this.$router.push({ name: 'Dashboard' });
                  },
                  error => {
                    this.loading = false;
                    this.message =
                      (error.resonse && error.resonse.data) ||
                      error.message ||
                      error.toString();
                  },
                );
              },
              error => {
                this.message =
                  (error.response && error.response.data) ||
                  error.message ||
                  error.toString();
                this.successful = false;
              },
            );
          } else {
            this.message = 'Inputs not Valid';
            console.log(this.message);
          }
        })
        .finally((this.loading = false));
    },
    onReset(evt) {
      evt.preventDefault();
      this.user = new User('');
      this.terms = false;
      this.message = '';
      this.loading = false;
    },
  },
};
</script>
<style>
form * {
  text-align: left;
}

div.no-horiz-margin {
  margin-top: 1em !important;
  margin-bottom: 0 !important;
}

.form-group {
  margin: 15px 0;
}

div.formError {
  height: 20px;
}

button {
  margin: 10px 0;
  margin-right: 20px;
}
</style>
