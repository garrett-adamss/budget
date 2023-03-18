<template>
  <div class="">
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#paycheckCreate">
      paycheck +
    </button>
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#psCreate">
      paycheck settings +
    </button>
    <div class="container-fluid p-4">
    <div class="row justify-content-center">
      <div class="col-12 col-lg-10">
        <div class="d-flex flex-column align-items-center">
          <div class="d-flex justify-content-between w-100 mb-3">
            <label v-for="column in columns" :key="column" class="toggle-label">
              <input type="checkbox" v-model="visibleColumns" :value="column" class="toggle-input">
              <span class="toggle-button"></span>
              <span class="toggle-text">{{ column }}</span>
            </label>
          </div>
          <div class="table-responsive">
            <table class="table table-bordered table-striped">
              <thead class="thead-dark">
                <tr>
                  <th v-for="(column, index) in filteredColumns" :key="index">{{ column }}</th>
                </tr>
              </thead>
              <tbody>
                <tr  v-for="paycheck in filteredPaychecks" :key="paycheck.id" class="selectable" @click="toPaycheckPage(paycheck.id)">
                  <td v-if="visibleColumns.includes('Pay Period')">{{ paycheck.payPeriod }}</td>
                  <td v-if="visibleColumns.includes('Payed On')">{{ paycheck.paycheckDate }}</td>
                  <td v-if="visibleColumns.includes('Gross Income')">${{ paycheck.grossIncome }}</td>
                  <td v-if="visibleColumns.includes('Net Income')">${{ paycheck.netIncome }}</td>
                  <td v-if="visibleColumns.includes('Taxes')">${{ paycheck.taxAmount }}</td>
                  <td v-if="visibleColumns.includes('Savings')">${{ paycheck.savings }}</td>
                  <td v-if="visibleColumns.includes('Tithe')">${{ paycheck.tithe }}</td>
                  <td v-if="visibleColumns.includes('Investments')">${{ paycheck.investments }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
  <PsCreateModal/>
  <PaycheckCreateModal/>
  </div>
</template>


<script>
import { onMounted, computed, ref } from 'vue';
import { paychecksService } from '../services/PaychecksService';
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState';
import { router } from '../router';
import { top } from '@popperjs/core';

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
      async toPaycheckPage(id){
        try {
          await paychecksService.getOne(id);
          router.push({name: 'Paycheck', params: {id}})  
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
.selectable:hover{
  cursor: pointer;
}
  .toggle-label {
    display: flex;
    align-items: center;
    margin-bottom: 0.5rem;
  }
  .toggle-input {
    display: none;
  }
  .toggle-button {
    width: 20px;
    height: 20px;
    border-radius: 10px;
    background-color: #ddd;
    margin-right: 0.5rem;
    position: relative;
  }
  .toggle-input:checked + .toggle-button {
    background-color: #1abc9c;
  }
  .toggle-text {
    font-weight: bold;
  }
  .table {
    margin-top: 2rem;
    margin-bottom: 2rem;
    width: 75vw; /* increase the width of the table */
  }
  .table thead th {
    font-weight: bold;
    text-align: center;
  }
  .table tbody td {
    text-align: center;
  }
  .table-responsive {
    overflow-x: auto;
  }
  .container-fluid {
    padding-left: 5%;
    padding-right: 5%;
  }
  @media (max-width: 767.98px) {
    .container-fluid {
      padding-left: 2%;
      padding-right: 2%;
    }
  }
</style>
