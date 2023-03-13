namespace budget.Repositories
{
    public class PaycheckSettings
    {
        public int Id { get; set;}
        public string AccountId { get; set;}
        public Account Account { get; set;}
        public decimal taxPercent { get; set;}
        public decimal savingsPercent { get; set;}
        public decimal tithePercent { get; set;}
        public decimal investmentsPercent { get; set;}
    }
}