namespace budget.Models;

public class Paycheck
{
    public int Id { get; set;}
    public string AccountId { get; set;}    
    public Account Account { get; set; }
    public DateTime PaycheckDate { get; set; }
    public DateTime PayPeriodStartDate { get; set; }
    public DateTime PayPeriodEndDate { get; set; }
    public decimal GrossIncome { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetIncome { get; set; }
    public decimal Savings { get; set; }
    public decimal Tithe { get; set; }
    public decimal RemainingIncome { get; set;}
    public string Details { get; set; }
}
