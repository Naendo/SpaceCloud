<template>
  <div class="login">
    <div class="login-container">
      <div class="login-col-content flex-center">
        <div class="content-container">
          <div class="welcome-container">
            <p class="bold">We are</p>
            <p class="logo-text">SpaceCloud</p>
            <p>{{ this.welcomeBackTxt }}</p>
          </div>
          <form class="form" @submit="onSubmit">
            <!-- Register -->
            <!-- <transition name="fade-control" mode="in-out"> -->
            <div class="register-controls" v-if="showSignUp">
              <div class="field">
                <div class="control">
                  <label class="label" for="firstName"
                    >First Name</label
                  >
                  <input
                    id="firstName"
                    name="firstName"
                    autocomplete="given-name"
                    class="input"
                    type="text"
                    v-model="form.firstName"
                    placeholder="First Name"
                    required
                    v-validate="'required'"
                  />
                </div>
              </div>
              <div class="field">
                <div class="control">
                  <label class="label" for="lastName"
                    >Last Name</label
                  >
                  <input
                    id="lastName"
                    name="lastName"
                    autocomplete="family-name"
                    class="input"
                    type="text"
                    v-model="form.lastName"
                    placeholder="Last Name"
                    required
                    v-validate="'required'"
                  />
                </div>
              </div>
            </div>
            <!-- </transition> -->
            <!-- Login -->
            <div class="login-controls">
              <div class="field">
                <div class="control">
                  <label class="label" for="mail">E-Mail</label>
                  <input
                    id="mail"
                    name="mail"
                    autocomplete="email"
                    class="input"
                    :class="{ 'is-danger': errorMsg.mail }"
                    type="text"
                    v-model="form.mail"
                    placeholder="E-Mail"
                    required
                    v-validate="'required|email'"
                  />
                  <div v-if="errorMsg.mail" class="help is-danger">
                    {{ errorMsg.mail }}
                  </div>
                </div>
              </div>
              <div class="field">
                <div class="control">
                  <label class="label" for="password">Password</label>
                  <input
                    id="password"
                    name="password"
                    autocomplete="current-password"
                    class="input"
                    :class="{ 'is-danger': errorMsg.password }"
                    type="password"
                    v-model="form.password"
                    placeholder="Password"
                    required
                    v-validate="'required'"
                  />
                  <div
                    v-if="errorMsg.password"
                    class="help is-danger"
                  >
                    {{ errorMsg.password }}
                  </div>
                  <div v-if="!showSignUp" class="help underlined">
                    Forgot Password?
                  </div>
                </div>
              </div>
              <!-- <div v-if="showSignUp" class="field">
                <div class="control">
                  <label class="label" for="confirmPassword"
                    >Confirm Password</label
                  >
                  <input
                    id="confirmPassword"
                    name="confirmPassword"
                    autocomplete="current-password"
                    class="input"
                    type="password"
                    placeholder=" Password"
                    v-validate="'confirmed:password'"
                  />
                </div>
              </div> -->
              <div class="field">
                <div class="control">
                  <label class="checkbox" for="stayLogged">
                    <input
                      id="stayLogged"
                      type="checkbox"
                      v-model="form.stayLogged"
                    />
                    Remember Me
                  </label>
                </div>
              </div>
              <!-- Login Buttons -->
              <div class="field is-grouped btns-field">
                <div class="control">
                  <button
                    type="submit"
                    ref="submitBtn"
                    class="button is-link"
                    :class="{ 'is-loading': loading }"
                  >
                    {{ showSignUp ? 'Register' : 'Log In' }}
                  </button>
                  <!-- <p class="help is-danger">{{ message }}</p> -->
                </div>
                <div class="control">
                  <button
                    type="button"
                    class="button"
                    v-on:click="switchLoginReg"
                  >
                    {{ showSignUp ? 'Log In' : 'Register' }}
                  </button>
                  <!-- <p class="help is-danger">{{ message }}</p> -->
                </div>
              </div>
              <div class="field" v-if="errorMsg.general">
                <p class="help is-danger error-general">
                  {{ errorMsg.general }}
                </p>
              </div>
            </div>
          </form>
        </div>
      </div>
      <div class="login-col-image"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Login',
  components: {},
  data() {
    return {
      form: {
        mail: 'timo.bader.tb@gmail.com',
        password: '12345678',
        firstName: 'Max',
        lastName: 'Müster',
        companyId: 1,
        stayLogged: true,
      },
      showSignUp: false,
      welcomeBackTxt: 'Welcome back! Please Log In',
      loading: false,
      errorMsg: {
        general: '',
        mail: '',
        password: '',
      },
    };
  },
  watch: {
    loading(val) {
      let t = null;
      // loading for 15 seconds -> cancel
      if (val === true)
        t = setTimeout(() => {
          if (this.loading === true) {
            this.errorMsg.general =
              'Something has gone wrong. Please try again later';
            this.loading = false;
            t = 0;
          }
        }, 25000);
      else if (t !== null) clearTimeout(t);
    },
  },
  created() {
    // window.addEventListener('keyup', evt => {
    //   if (evt.keyCode === 13)
    //     this.onSubmit();
    // });

    if (this.$store.state.auth.loggedIn) {
      this.$router.push({ name: 'Dashboard' });
    }
  },
  methods: {
    async onSubmit(evt) {
      this.loading = true;

      this.clearErrors();
      if (evt) evt.preventDefault();

      await this.$validator
        .validateAll()
        .catch(error => {
          console.log('Validation Error: ', error);
          this.loading = false;
          this.errorMsg.general = error;
        })
        .then(() => {
          // Login
          if (!this.showSignUp) {
            this.$store
              .dispatch('auth/login', this.form)
              .then(
                () => {
                  this.$router.push({ name: 'Dashboard' });
                },
                error => {
                  // set correct error-message
                  const errorRespMsg = error.response.data.message;
                  if (errorRespMsg === 'mail-not-found')
                    this.errorMsg.mail =
                      'No Account with this email adress has been found';
                  else if (errorRespMsg === 'password-wrong')
                    this.errorMsg.password = 'Wrong password';
                  else
                    this.errorMsg.general = `Something went wrong!\n${errorRespMsg}`;
                  this.loading = false;
                },
              )
              .finally((this.loading = false));
          }

          // Register
          else {
            this.$store
              .dispatch('auth/register', this.form)
              .then(
                () => {
                  this.$swal({
                    title:
                      'Your account has successfully been created',
                    text: 'Email Verification is coming soon...',
                    icon: 'success',
                    confirmButtonText: 'Take me to my Dashboard',
                    confirmButtonColor: '#1b53a8',
                  }).then(res => {
                    console.log(res);
                    this.$router.push({ name: 'Dashboard' });
                  });
                },
                error => {
                  if (!('response' in error)) console.log(error);
                  // this.errorMsg.general = `Something went wrong!\n${error.response.data.message}`;
                  else if (error.response.status === 409)
                    this.errorMsg.mail =
                      'This Email Adress is already registered. Try to Log In instead';
                  else
                    this.errorMsg.general = `Something went wrong!\n${error.response.data.message}`;
                },
              )
              .finally((this.loading = false));
          }
        });
    },
    switchLoginReg() {
      this.clearErrors();
      if (this.showSignUp)
        this.welcomeBackTxt = 'Welcome back! Please Log In';
      else this.welcomeBackTxt = 'Nice to meet you!';

      this.showSignUp = !this.showSignUp;
    },
    clearErrors() {
      this.errorMsg.general = '';
      this.errorMsg.mail = '';
      this.errorMsg.password = '';
    },
  },
};
</script>
<style lang="scss">
.register-controls {
  transform-origin: top;
}

// .fade-control-enter-active,
// .fade-control-leave-active {
//   transition: transform 150ms cubic-bezier(0.26, 0.93, 0.82, 0.97);
// }

// .fade-control-enter-to,
// .fade-control-leave {
//   transform: scaleY(1);
// }

// .fade-control-enter,
// .fade-control-leave-to {
//   transform: scaleY(0);
// }

.login,
.login-container {
  height: 100%;
  width: 100%;
}

.login-container {
  display: flex;
  flex-wrap: nowrap;
  background-color: white;
}

.login-container > * {
  height: 100%;
}

.login-col-content {
  width: 40%;
  min-width: 500px;
}

.login-col-image {
  flex-grow: 1;
  background-image: url('../assets/login/login-bg.png');
  background-color: #09192f;
  background-size: cover;
  background-position: center;
}

.content-container {
  display: flex;
  width: 400px;
  flex-direction: column;
  flex-wrap: nowrap;
  justify-content: center;
}

.content-container > *:not(form),
.field {
  margin: 25px 0;
}

.logo-text {
  font-size: 2.8em;
  font-weight: 600;
  letter-spacing: 0.1em;
  margin: 0;
  color: #1b53a8;
}

.btns-field {
  justify-content: space-around !important;
}

.btns-field > .control {
  width: 40%;
  margin: 0;
}

.btns-field > .control > button {
  width: 100%;
}

.help.is-danger {
  text-overflow: '…';
  overflow: hidden;
}

.help.is-danger:not(.error-general) {
  max-height: 1.2rem;
}

.error-general {
  height: 3.2rem;
}
</style>
