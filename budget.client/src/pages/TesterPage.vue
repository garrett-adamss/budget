<template>
    <div>
      <div class="columns-filter">
        <div v-for="(column, index) in columns" :key="index">
          <input type="checkbox" :id="column" v-model="selectedColumns" :value="column">
          <label :for="column">{{ column }}</label>
        </div>
      </div>
      <table>
        <thead>
          <tr>
            <th v-for="(column, index) in selectedColumns" :key="index">{{ column }}</th>
          </tr>
        </thead>
        <tbody>
          <template v-for="p in appstate.paychecks">
            <div :key="p.id" v-if="p">
              <Paycheck :paycheck="p" />
            </div>
          </template>
          <paycheck/>
        </tbody>
      </table>
    </div>
  </template>
  
  <script>
  import { computed } from '@vue/reactivity';
  import { AppState } from '../AppState'
  import Paycheck from '../components/Paycheck.vue'
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
  
  export default {
    components: { Paycheck },
    data() {
      return {
        columns: ['paycheckDate', 'payPeriodStartDate', 'payPeriodEndDate', 'grossIncome', 'taxes', 'netIncome', 'savings', 'tithe', 'investment'],
        selectedColumns: ['paycheckDate', 'grossIncome', 'netIncome'],
      };
    },
    computed: {
        paychecks: computed(() => AppState.paychecks),
      filteredColumns() {
        return this.columns.filter(column => {
          return this.selectedColumns.includes(column);
        });
      },
    },
    mounted() {
      this.getPaycheck();
    },
    methods: {
      async getPaycheck() {
        try {
        logger.log("[account]", AppState.account)
        logger.log(AppState.paychecks)
        await paychecksService.getPaychecksByProfileId()
      }
      catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
      },
    },
  };
  </script>
    
  <style scoped>
  .columns-filter {
    display: flex;
    flex-wrap: wrap;
    gap: 0.5rem;
  }
  .columns-filter label {
    margin-left: 0.25rem;
  }
  </style>
  