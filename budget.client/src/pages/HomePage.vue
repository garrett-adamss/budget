<template>
  <div class="">
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#paycheckCreate">
      paycheck +
    </button>
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#psCreate">
      paycheck settings +
    </button>
    <!-- <Paycheck /> -->
    <div class="" v-for="p in paychecks" :key="p.id">
      <Paycheck :paycheck="p" />
    </div>
    <PaycheckCreateModal />
    <PsCreateModal />
  </div>
</template>

<script>
import { AppState } from '../AppState'
import Paycheck from '../components/Paycheck.vue'
import { logger } from '../utils/Logger'
import { paychecksService } from '../services/PaychecksService';
import Pop from '../utils/Pop'
import { computed, onMounted } from 'vue';
import PaycheckCreateModal from '../components/PaycheckCreateModal.vue';

export default {
  components: { Paycheck, PaycheckCreateModal },
  setup() {
    async function getPaychecks() {
      try {
        logger.log("[account]", AppState.account)
        logger.log(AppState.paychecks)
        await paychecksService.getPaychecksByProfileId()
      }
      catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    }
    onMounted(() => {
      getPaychecks();
    })
    return {
      paychecks: computed(() => AppState.paychecks),
      // async setActive() {
      //   try {
      //     Modal.getOrCreateInstance(document.getElementById("paycheckCreate")).toggle();
      //     await keepsService.getOne(props.paycheck.id);
      //   }
      //   catch (error) {
      //     logger.error(error);
      //     Pop.toast(error.message, "error");
      //   }
      // }
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

    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
