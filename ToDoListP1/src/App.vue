<script setup>
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="@/assets/logoCheckMark.png" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
    </div>
  </header>

  <RouterView />
</template>

<style scoped>

* {
  box-sizing: border-box;
}

.logo {
  display: block;
  margin: 0 auto;
}

</style>
