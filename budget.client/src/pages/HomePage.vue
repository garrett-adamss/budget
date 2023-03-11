<template>
  <div class="">
    <Paycheck/>
    <div class="" v-for="p in keeps" :key="p.id">
      <Paycheck :paycheck="p"/>
    </div>
  </div>s
</template>

<script>
import { AppState } from '../AppState'
import Paycheck from '../components/Paycheck.vue'
import { logger } from '../utils/Logger'
import { paychecksService } from '../services/PaychecksService';
import Pop from '../utils/Pop'
import { computed, onMounted } from 'vue';
export default {
  components: { Paycheck },
  setup() {
    // async function getPaychecks(){
    //   try {
    //      logger.log(AppState.paychecks)
    //      await paychecksService.getPaychecksByProfileId()
    //   }
    //   catch (error) {
    //      logger.error(error)
    //      Pop.toast(error.message, 'error')
    //   }
    // }
    // onMounted(()=> {
    //   getPaychecks();
    // })
    return {
      // paychecks: computed(()=> AppState.paychecks)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
