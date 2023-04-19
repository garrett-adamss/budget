<template>
  <div class="container-fluid">
    <div class="row">
      <div>edit<i class="mdi mdi-pencil"></i></div>

      <div class="table-responsive">
            <table class="table table-bordered table-striped">
              <tbody>
                <tr>
                  <td>Pay Period : <i>{{formatDate(paycheck.payPeriodStartDate)}} - {{formatDate(paycheck.payPeriodEndDate)}}</i></td>
                  <td>Payed On <i>{{ formatDate(paycheck.paycheckDate)}}</i></td>
                </tr>
                <tr>
                  <td class="title">Gross Income:</td>
                  <td class="money"><b>${{ paycheck.grossIncome }}</b></td>
                </tr>
                <tr>
                  <td class="title">Net Income:</td>
                  <td class="money">${{ paycheck.netIncome }}</td>
                </tr>
                <tr>
                  <td class="title">Taxes:</td>
                  <td class="money">${{ paycheck.taxAmount }}</td>
                </tr>
                <tr>
                  <td class="title">Savings:</td>
                  <td class="money">${{ paycheck.savings }}</td>
                </tr>
                <tr>
                  <td class="title">Investments:</td>
                  <td class="money">${{ paycheck.investments }}</td>
                </tr>
                <tr>
                  <td class="title">Tithe:</td>
                  <td class="money">${{ paycheck.tithe }}</td>
                </tr>
                <tr>
                  <td class="title">Remaining Income:</td>
                  <td class="money"><b>${{ paycheck.remainingIncome }}</b></td>
                </tr>
              </tbody>
            </table>
          </div>
    </div>
  </div>
</template>
 
 <script>
 import { computed } from 'vue'
 import { AppState } from '../AppState'
 
 export default {
   setup() {
     return {
       paycheck: computed(() => AppState.activePaycheck),
       formatDate(dateString) {
         const date = new Date(dateString)
         const month = date.getMonth() + 1
         const day = date.getDate()
         const year = date.getFullYear().toString().slice(-2)
         return `${month < 10 ? '0' : ''}${month}/${day < 10 ? '0' : ''}${day}/${year}`
       }
     }
   },
 }
 </script>
 
 <style scoped>
 /* Desktop Styling */
 @media only screen and (min-width: 768px) {
  .container-fluid{
    padding-right: 16vw;
    padding-left: 16vw;
  }
 }
 /* Mobile Styling */
 @media only screen and (max-width: 768px) {
  .container-fluid{
    padding-right: 5vw;
    padding-left: 5vw;
  }
 }

 .note{
  background-color: brown;
  border-radius: 4px;
  padding: 0;
  margin: 0;
 }

.container-fluid {
  margin-top: 5vh;
}
 .row{
  height: 80vh;
  border-radius: 5px;
 }
 .test{
  border: 1px solid blue;
 }
 .tbody{
  width: auto;
 }
 .title{
  font-size: 18px;
 }
 .money{
  font-size: 18px;
  /* font-weight: bold; */
 }
 .edit{
  text-align: right;
  width:auto;
 }
 .selectable:hover{
  cursor: pointer;
}
 
 </style>
 