<template>
  <nav class="navbar navbar-expand-lg navbar-dark nav-color px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <!-- to home  --> HOME
    </router-link>
    <router-link class="navbar-brand d-flex" :to="{ name: 'Tester' }">
      TEST
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'Calendar' }" class="btn text-light lighten-30 selectable text-uppercase">
            Calendar
          </router-link>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(whitesmoke);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-color{
  background-color: #2eaf7e7b;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

</style>
