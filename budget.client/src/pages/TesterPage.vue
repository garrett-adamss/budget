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
          <!-- <template v-for="p in appstate.paychecks">
            <div :key="p.id" v-if="p">
              <Paycheck :paycheck="p" />
            </div>
          </template> -->
          <paycheck/>
        </tbody>
      </table>
    </div>
  </template>
  
  <script>
  import { computed } from '@vue/reactivity';
  import { AppState } from '../AppState'
import Paycheck from '../components/Paycheck.vue';
//   import Paycheck from '@/components/Paycheck.vue';
  
  export default {
    components: {
        Paycheck
    //   Paycheck,
    },
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
    //   this.getPaycheck();
    },
    methods: {
      getPaycheck() {
        // Your code to get paycheck data goes here
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
  