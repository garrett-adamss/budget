<template>
    <!-- Modal -->
    <div
      class="modal fade"
      id="psCreate"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Create Paycheck Settings</h1>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body" @submit.prevent="handleSubmit">
            <div class="mb-3">
              <form>
                <label for="" class="form-label">Tax Percent</label>
                <input v-model="editable.taxPercent" type="number" class="form-control" name="" id="" aria-describedby="helpId" placeholder="% " min="1.00" step="0.01" max="100"/>
  
                <label for="" class="form-label">Savings Percent</label>
                <input v-model="editable.savingsPercent" type="number" class="form-control" name="" id="" aria-describedby="helpId" placeholder="% " max="100"/>
  
                <label for="" class="form-label">Tithe Percent</label>
                <input v-model="editable.tithePercent" type="number" class="form-control" name="" id="" aria-describedby="helpId" placeholder="% " max="100"/>
  
                <label for="" class="form-label">Investments Percent</label>
                <input v-model="editable.investmentsPercent" type="number" class="form-control" name="" id="" aria-describedby="helpId" placeholder="% " max="100"/>
  
                <button type="submit" class="btn btn-primary">
                  Create Paycheck Settings
                </button>
              </form>
            </div>
          </div>
          <div class="modal-footer"></div>
        </div>
      </div>
    </div>
  </template>
   
  <script>
  import { computed, ref } from 'vue'
  import { useRoute } from 'vue-router'
  import { psService } from '../services/PsService'
  import { logger } from '../utils/Logger'
  import Pop from '../utils/Pop'
  import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
  export default {
    setup() {
      const route = useRoute()
      const editable = ref({ isPrivate: false })
      return {
        route,
        editable,
        account: computed(() => AppState.account),
        async handleSubmit() {
          try {
            if (!AppState.account.id) {
              Pop.error("You must be signed in to log a check.")
              throw new Error("You must be signed in to log a check.");
            }
          //    else if (AppState.account.id != route.params.id) {
          //     Pop.error("You can only create keeps on your vault")
          //     throw new Error("You can only create keeps on your vault");
          //   }
            logger.log("editable.value", editable.value)
            editable.value.accountId = route.params.id
            await psService.createPaycheckSettings(editable.value)
            Modal.getOrCreateInstance("#psModal").hide()
            Pop.success("Paycheck settings set!")
          }
          catch (error) {
            logger.error(error)
            Pop.toast(error.message, 'error')
          }
        }
      }
    }
  }
  </script>
   
  <style>
  </style>