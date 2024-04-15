<script setup>
import ToDoList from './components/ToDoList.vue'
</script>

<template>
    <div id="app" class="container">
      <img alt="Vue logo" class="logo" src="@/assets/logoCheckMark.png" width="125" height="125" />
      <ToDoList />
    </div>
</template>

<style>

* {
  box-sizing: border-box;
}

.container {
  max-width: 600px;
  margin: 0 auto;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #275a8d;
  margin-top: 60px;
  font-size: 24px;
}

.logo {
  display: block;
  margin: 5px auto;
}

</style>
