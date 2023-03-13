<template>
  <!-- <div class="row">
    <div class="col-10 offset-1 paycheck test d-flex justify-content-between align-items-center">
      <div class="column ps-4 pe-4">
        <i>Dates</i>
        <p>1/1 - 1/15</p>
      </div>
      <div class="column ">
        <i>Payed On</i>
        <p>1/14</p>
      </div>
      <div class="column ">
        <i>Gross Income</i>
        <p>$3828.34</p>
      </div>
      <div class="column ">
        <i>Net Income</i>
        <p>$2989.51</p>
      </div>
      <div class="column ">
        <i>Taxes</i>
        <p>$942.13</p>
      </div>
      <div class="column ">
        <i>Savings</i>
        <p>$295.10</p>
      </div>
      <div class="column ">
        <i>Tithe</i>
        <p>$295.10</p>
      </div>
      <div class="column ">
        <i>Investments</i>
        <p>$295.10</p>
      </div>
      <div class="column-end">
        <i>Remaining Total</i>
        <p>$295.10</p>
      </div>
    </div>
  </div> -->
  <div>
    
  </div>
</template>

<script>
import Paycheck from '../components/Paycheck.vue'
export default {
    props: {
      paycheck: {
         type: Object,
         required: true
      }
   },
  setup() {
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
  /* padding-left: 1vw;
  padding-right: 1.5vw; */
}
.column-end{
  /* padding-left: 1vw;
  padding-right: 1.5vw; */
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
  border-radius: 5px;
  padding: 0;
}
.row{
  /* border: 1px solid rgb(3, 199, 3); */
  margin-left:5vw;
  margin-right:5vw;
  margin-top: 8vh;
}
</style>