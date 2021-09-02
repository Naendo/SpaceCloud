<template>
  <div class="DbSpacecoinBalance">
    <p id="balance-title">Balance</p>
    <div id="balance-content">
      <span id="balance">{{ balance }}</span>
      <img src="@/assets/icons/spacecoin.svg" alt="Icon" />
    </div>
  </div>
</template>
<script>
export default {
  name: 'name',
  components: {},
  data() {
    return {
      balance: 50,
    };
  },
  created() {
    // get spacecoin balance
  },
};
</script>
<style scoped lang="scss">
.DbSpacecoinBalance {
  height: 47%;
  background-color: $tile-background;
  border-radius: 20px;

  margin-bottom: 5%;
  padding: 25px;
}

#balance-title {
  text-align: center;
  font-size: 1.5em;
  // margin: 0 auto 20px;
}

#balance-content {
  display: flex;
  justify-content: center;
  align-items: center;
}

#balance {
  font-size: 3em;
  margin-right: 20px;
}
</style>
