<template>
  <div class="login">
    <div class="login-wrapper">
      <div id="welcome-back-wrapper">
        <div id="welcome-back-content" class="align-middle">
          <h2 id="welcome-back-heading">{{ text.sideHeadline }}</h2>
          <div id="welcome-back-text" v-html="text.sideText"></div>
          <button
            class="button is-rounded is-primary is-inverted is-outlined"
            variant="outline-light"
            size="lg"
            v-on:click="switchLoginReg()"
          >
            {{ text.sideBtnText }}
          </button>
        </div>
      </div>
      <div id="login-register-wrapper">
        <LoginForm v-if="toggleLogin" />
        <RegisterForm v-else />
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import LoginForm from '@/components/LoginForm';
import RegisterForm from '@/components/RegisterForm';

export default {
  name: 'Login',
  components: {
    LoginForm,
    RegisterForm,
  },
  data() {
    /* all instances of this Component share the same "data", this is prevented by this function */
    return {
      form: {
        mail: 'timo.bader@trash-mail.com',
        pw: 'Timo123',
        firstName: 'Timo',
        lastName: 'Bader',
      },
      toggleLogin: 'true',
      text: {
        headline: 'Log In',
        sideHeadline: 'New here?',
        sideText: "Don't have an account?<br />Register now!",
        sideBtnText: 'Create account',
      },
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.loggedIn;
    },
  },
  /* props are data objects expected to be parsed down by its parent component */
  methods: {
    switchLoginReg() {
      if (this.toggleLogin) {
        this.text.headline = 'Register';
        this.text.sideHeadline = 'Welcome back!';
        this.text.sideText =
          'Already have an account?<br />Log in Now!';
        this.text.sideBtnText = 'Log in';
      } else {
        this.text.headline = 'Log In';
        this.text.sideHeadline = 'New here?';
        this.text.sideText =
          "Don't have an account?<br />Register now!";
        this.text.sideBtnText = 'Create account';
      }
      this.toggleLogin = !this.toggleLogin;
    },
  },
  created() {
    if (this.loggedIn) {
      this.$router.push({ name: 'Dashboard' });
    }
  },
};
</script>
<style lang="scss">

html {
  overflow: hidden !important;
}

.login {
  height: 100%;
  width: 100%;

  display: flex !important;
  justify-content: center;
  align-items: center;

  background-color: rgba(89, 152, 247, 0.3);
}

.login-wrapper {
  height: 600px;
  width: 1100px;

  display: flex;
  flex-wrap: nowrap;
}

#welcome-back-wrapper,
#login-register-wrapper {
  height: 100%;
  min-width: 200px;
  margin: 0;
  padding: 40px;
  vertical-align: top;
}

#welcome-back-wrapper {
  width: 35%;
  color: white;
  background-color: $primary;
  text-align: center;
  border-radius: 30px 0 0 30px;
  display: inline-flex;
  justify-content: center;
  align-items: center;
}

#login-register-wrapper {
  width: 65%;
  display: inline-block;
  background-color: white;

  border-radius: 0 30px 30px 0;
}

#welcome-back-heading {
  font-size: 24px;
  margin-bottom: 20px;
}
#welcome-back-text {
  font-size: 16px;
  margin-bottom: 20px;
}

#bg-triangle {
  height: 716px;
  width: 1540px;
  top: -200px;
  left: 780px;
  transform: rotate(-148deg);
  fill: var(--c-lightgreen);
}

#bg-ellipse {
  height: 1000px;
  width: 1357px;
  top: 300px;
  left: -680px;

  fill: var(--c-red);
}
</style>
