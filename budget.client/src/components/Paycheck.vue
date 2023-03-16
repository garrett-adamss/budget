<template>
   <div class="row">
    <div class="col-10 offset-1 paycheck test d-flex justify-content-between align-items-center">
      <div class="column ps-4 pe-4">
        <!-- <i>Dates</i> -->
        <p>{{ formattedPayPeriod }}</p>
      </div>
      <div class="column ">
        <!-- <i>Payed On</i> -->
        <p>{{ formattedPaycheckDate }}</p>
      </div>
      <div class="column ">
        <!-- <i>Gross Income</i> -->
        <p>${{ paycheck?.grossIncome }}</p>
      </div>
      <div class="column ">
        <!-- <i>Net Income</i> -->
        <p>${{ paycheck?.netIncome }}</p>
      </div>
      <div class="column ">
        <!-- <i>Taxes</i> -->
        <p>${{ paycheck?.taxAmount }}</p>
      </div>
      <div class="column ">
        <!-- <i>Savings</i> -->
        <p>${{ paycheck?.savings }}</p>
      </div>
      <div class="column ">
        <!-- <i>Tithe</i> -->
        <p>${{ paycheck?.tithe }}</p>
      </div>
      <div class="column ">
        <!-- <i>Investments</i> -->
        <p>${{ paycheck?.investments }}</p>
      </div>
      <div class="column-end">
        <!-- <i>Remaining Total</i> -->
        <p>${{ paycheck?.remainingIncome }}</p>
      </div>
    </div>
  </div>
</template>
 
<script>
export default {
   props: {
      paycheck: {
         type: Object,
         required: true
      }
   },
   computed: {
      formattedPayPeriod() {
        if (!this.paycheck || !this.paycheck.payPeriodStartDate || !this.paycheck.payPeriodEndDate) {
          return '';
        }
  
        const startDate = new Date(this.paycheck.payPeriodStartDate);
        const endDate = new Date(this.paycheck.payPeriodEndDate);
        const options = { day: 'numeric', month: 'numeric', year: '2-digit' };
        const formattedStartDate = startDate.toLocaleDateString(undefined, options);
        const formattedEndDate = endDate.toLocaleDateString(undefined, options);
        return `${formattedStartDate} - ${formattedEndDate}`;
      },
    formattedPaycheckDate() {
        if (this.paycheck) {
        const date = new Date(this.paycheck.paycheckDate);
        const options = { day: 'numeric', month: 'numeric' };
       return date.toLocaleDateString(undefined, options);
       } else {
         return '';
        }
      }
   },
   setup(){
      return {

      }
   }
}
</script>
 
<style>
.test{
  /* border: .5px solid red; */
  border-radius: 4px;
}
.column{
  /* border: .5px solid red; */
  /* padding-top: .25em; */
  border-right: 1.5px solid gray;
  padding-left: 1vw;
  padding-right: 1.5vw;
}
.column-end{
  padding-left: 1vw;
  padding-right: 1.5vw;
}
i{

  /* border-bottom: 1.5px solid gray; */
}
p{
  /* padding-top: .5em; */
  /* margin: 0; */
}
.paycheck{
  border: 1px solid gray;
  border-radius: 6px;
  padding: 0;
  background-color: whitesmoke;
}
.row{
  /* border: 1px solid rgb(3, 199, 3); */
  margin-left:5vw;
  margin-right:5vw;
  margin-top: 1vh;
} 
</style>