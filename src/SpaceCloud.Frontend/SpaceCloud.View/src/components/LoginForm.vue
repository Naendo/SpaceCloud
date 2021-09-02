<template>
  <div class="loginform">
    <form @submit="handleLogin" @reset="onReset" v-if="show">
      <div class="field">
        <label class="label" for="mail">E-Mail</label>
        <div class="control">
          <input
            id="mail"
            name="mail"
            autocomplete="email"
            class="input"
            type="text"
            v-model="user.mail"
            placeholder="E-Mail"
            required
            v-validate="'required|email'"
            value="timo.bader@trash-mail.com"
          />
        </div>
      </div>
      <div class="field">
        <label class="label" for="password">Password</label>
        <div class="control">
          <input
            id="password"
            name="password"
            autocomplete="current-password"
            class="input"
            type="password"
            v-model="user.password"
            placeholder="Password"
            required
            v-validate="'required'"
            value="Timo123"
          />
        </div>
      </div>
      <div class="field">
        <div class="control">
          <label class="checkbox" for="stayLogged">
            <input
              id="stayLogged"
              type="checkbox"
              v-model="user.stayLogged"
            />
            Stay Logged In
          </label>
        </div>
      </div>
      <div class="control">
        <button
          type="submit"
          :class="{ 'is-loading': loading }"
          class="button is-link"
        >
          Log In
        </button>
        <p class="help is-danger">{{ message }}</p>
      </div>
    </form>
  </div>
</template>

<script>
import User from '@/models/user';

export default {
  name: 'LoginForm',
  props: {},
  data() {
    return {
      user: new User('', '', '', '', true),
      loading: false,
      message: '',
      show: true,
    };
  },
  methods: {
    handleLogin(evt) {
      evt.preventDefault();
      this.loading = true;
      this.$validator.validateAll().then(isValid => {
        if (!isValid) {
          this.loading = false;
          return;
        }

        if (this.user.mail && this.user.password) {
          this.$store.dispatch('auth/login', this.user).then(
            () => {
              this.$router.push({ name: 'Dashboard' });
            },
            // eslint-disable-next-line no-unused-vars
            error => {
              // this.message =
              //   (error.resonse && error.resonse.data) ||
              //   error.message ||
              //   error.toString();
              this.message = 'Something went wrong. Please try again';
              this.loading = false;
            },
          );
        }
      });
    },
    onReset(evt) {
      evt.preventDefault();
      this.user = new User('');
      this.message = '';
    },
  },
};
</script>
<style>
a.pwReset {
  text-decoration: underline;
}
</style>
