<template>
<div id="login-wrapper">
  <img src="../assets/spacecloud_logo.svg">
  <div id="login-form">
    <h3>Welcome Back</h3>
    <h2>Start your Scheduler!</h2>
    <form @submit.prevent="authenticate">
      <div>
        <label name="token">Enter 6 digit code</label>
        <img src="../assets/help_icon.png" alt="helper">
      </div>
      <input name="token" type="text" v-model="token" placeholder="________" maxlength="8"/>
      <input name="submit" type="submit">
    </form>
  </div>
</div>
</template>

<script lang="ts">

import {defineComponent, ref} from 'vue';
import axios from '../services/axiosService';
import {setCookie} from '../services/handlers/cookieHandler';
import {useRouter} from 'vue-router';

export default defineComponent({
  name: "login",

  setup() {

    let token = ref<string>("");
    const router = useRouter();

    async function authenticate() {

      if (token.value.length !== 8)
        return;

      axios.post("/scheduler/login/" + token.value,"")
      .then(async response => {
        setCookie("token",response.data.token,new Date(Date.parse(response.data.exp)));


        await router.push("scheduler");

      }).catch(ex =>
      console.log(ex));

    }

    return {
      token,
      authenticate
    }

  }
})
</script>
<style lang="scss" scoped>
@import '../assets/style/global.scss';

#login-wrapper {
  background-color: $main-bg;
  height: 100vh;
  overflow: hidden;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;

  > img {
    top: 10px;
    position: absolute;
    margin-top: 2vh;
    height: 10vh;
    width: 250px;
    object-fit: cover;
  }
}

#login-form {
  font-family: "Roboto Thin";
  color: white;
  width: 500px;
  height: auto;
  background-color: $secondary-bg;
  border-radius: 15px;
  text-align: center;
  padding: 50px 20px;


  > h3 {
    font-weight: 100;
    letter-spacing: 1px;
    text-transform: uppercase;
    font-size: 1.3rem;
  }

  > h2 {
    letter-spacing: 3px;
    font-size: 2rem;
  }

  > h2::after {
    display: block;
    content: ' ';
    background-color: white;
    width: 30%;
    margin: 20px auto;
    height: 2px;
  }

  form {
    height: 60%;
    margin: 0 auto;
    width: 80%;

    div {
      display: flex;
      justify-content: space-between;
      width: 100%;
    }


    input {
      margin: 15px 0;
      width: 100%;
      border: none;
      outline: none;
    }

    input[type="submit"] {
      background-color: $contrast;
      color: white;
      font-size: 1.5rem;
      border-radius: $corner-radius;
      padding: 25px 0;
    }

    input[type="text"] {
      background-color: $secondary-bg;
      border: 1px solid white;
      border-radius: $corner-radius;
      font-size: 2rem;
      padding: 20px 20px;
      color:white;
      box-sizing: border-box;
      letter-spacing: 4px;
    }
    input[type="text"]::placeholder {
      letter-spacing: 4px;
    }
  }
}


</style>