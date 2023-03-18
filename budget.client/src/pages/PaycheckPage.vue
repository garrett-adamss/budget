<template>
   <div class="container">
     <div class="row">
       <div class="col-12 col-md-6">
         <div class="card">
           <div class="card-body">
             <h5 class="card-title">Paycheck Information</h5>
             <div class="card-text">
               <div class="row">
                 <div class="col-6">Payed on:</div>
                 <div class="col-6">{{ formatDate(paycheck.paycheckDate) }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Pay Period:</div>
                 <div class="col-6">{{ formatDate(paycheck.payPeriodStartDate) }} - {{formatDate(paycheck.payPeriodEndDate) }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Gross Income:</div>
                 <div class="col-6">{{ paycheck.grossIncome }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Net Income:</div>
                 <div class="col-6">{{ paycheck.netIncome }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Taxes:</div>
                 <div class="col-6">{{ paycheck.taxAmount }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Savings:</div>
                 <div class="col-6">{{ paycheck.savings }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Tithe:</div>
                 <div class="col-6">{{ paycheck.tithe }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Investments:</div>
                 <div class="col-6">{{ paycheck.investments }}</div>
               </div>
               <div class="row">
                 <div class="col-6">Remaining Amount:</div>
                 <div class="col-6">{{ paycheck.remainingIncome }}</div>
               </div>
               <div v-if="!paycheck.details" class="row">
                 <div class="col-6">Details:</div>
                 <div class="col-6">Add details</div>
               </div>
               <div v-else class="row">
                 <div class="col-6">Details:</div>
                 <div class="col-6">{{ paycheck.details }}</div>
               </div>
             </div>
           </div>
         </div>
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
       },
     }
   },
 }
 </script>
 
 <style>
 .container {
   padding: 20px;
 }
 
 .card {
   border: none;
   border-radius: 10px;
   box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
   background-color: #fff;
   padding: 20px;
 }
 
 .card-title {
   font-size: 24px;
   font-weight: bold;
   margin-bottom: 20px;
 }
 
 .card-text {
   font-size: 18px;
 }
 
 .row {
   margin-bottom: 10px;
 }
 
 .col-6 {
   font-weight: bold;
 }
 
 .col-12 {
   margin-bottom: 20px;
 }
 </style>
 