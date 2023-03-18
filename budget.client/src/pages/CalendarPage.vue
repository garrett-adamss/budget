<template>
  <div class="d-flex flex-column align-items-center mt-4">
    <div class="column-toggles mb-3">
      <label v-for="column in columns" :key="column" :class="{ 'selected': visibleColumns.includes(column) }">
        <input type="checkbox" v-model="visibleColumns" :value="column">{{ column }}
      </label>
    </div>
    <table class="table table-bordered">
      <thead>
        <tr>
          <th v-for="(column, index) in filteredColumns" :key="index">{{ column }}</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="paycheck in filteredPaychecks" :key="paycheck.id" @click="toPaycheckPage()" class="selectable">
          <td v-if="visibleColumns.includes('Pay Period')">{{ paycheck.payPeriod }}</td>
          <td v-if="visibleColumns.includes('Payed On')">{{ paycheck.paycheckDate }}</td>
          <td v-if="visibleColumns.includes('Gross Income')">${{ paycheck.grossIncome }}</td>
          <td v-if="visibleColumns.includes('Net Income')">${{ paycheck.netIncome }}</td>
          <td v-if="visibleColumns.includes('Taxes')">${{ paycheck.taxAmount}}</td>
          <td v-if="visibleColumns.includes('Savings')">${{ paycheck.savings }}</td>
          <td v-if="visibleColumns.includes('Tithe')">${{ paycheck.tithe }}</td>
          <td v-if="visibleColumns.includes('Investments')">${{ paycheck.investments }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { onMounted, computed, ref } from 'vue';
import { paychecksService } from '../services/PaychecksService';
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState';
import { router } from '../router';

export default {
  name: 'PaychecksPage',

  setup() {
    const columns = ['Pay Period', 'Payed On', 'Gross Income', 'Net Income', 'Taxes', 'Savings', 'Tithe', 'Investments'];

    const visibleColumns = ref(columns);

    function formatDate(dateString) {
    const date = new Date(dateString);
    const month = date.getMonth() + 1;
    const day = date.getDate();
    const year = date.getFullYear().toString().slice(-2);
    return `${month < 10 ? '0' : ''}${month}/${day < 10 ? '0' : ''}${day}/${year}`;
    }

    const filteredColumns = computed(() => {
      return columns.filter(column => visibleColumns.value.includes(column));
    });

    const filteredPaychecks = computed(() => {
      return AppState.paychecks.map((paycheck) => {
       return {
          id: paycheck.id,
          payPeriod: `${formatDate(paycheck.payPeriodStartDate)} - ${formatDate(paycheck.payPeriodEndDate)}`,
          paycheckDate: formatDate(paycheck.paycheckDate),
          grossIncome: paycheck.grossIncome,
          netIncome: paycheck.netIncome,
          taxAmount: paycheck.taxAmount,
          savings: paycheck.savings,
          tithe: paycheck.tithe,
          investments: paycheck.investments,
        };
      });
    });

    async function getPaychecks() {
      try {
        logger.log(AppState.paychecks);
        await paychecksService.getPaychecksByProfileId();
      } catch (error) {
        logger.error(error);
        Pop.toast(error.message, 'error');
      }
    }

    onMounted(() => {
      getPaychecks();
    });

    return {
      columns,
      visibleColumns,
      filteredPaychecks,
      filteredColumns,
      async toPaycheckPage(){
        try {
          router.push({name: 'PaycheckPage'})  
        }
        catch (error) {
           logger.error(error)
           Pop.toast(error.message, 'error')
        }
      }
    };
  },
};
</script>

<style scoped>
.selected {
    color: white;
    background-color: rgba(4, 171, 4, 0.666);
    padding: 5px 10px;
    border-radius: 5px;
  }
.column-toggles {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
.selectable:hover{
  cursor: pointer;
}
.column-toggles label {
  margin-right: 10px;
  padding: 5px 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.column-toggles label:hover {
  background-color: #f2f2f2;
}

.column-toggles input[type="checkbox"] {
  display: none;
}

.column-toggles input[type="checkbox"]:checked + label {
  background-color: #007bff;
  color: #fff;
}

table {
  width: 100%;
}

th, td {
  text-align: center;
  vertical-align: middle !important;
}
</style>
