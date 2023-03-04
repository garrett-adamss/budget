namespace budget.Repositories
{
    public class PaychecksRepository
    {
        private readonly IDbConnection _db;

        public PaychecksRepository(IDbConnection db)
        {
            _db = db;
        }

        //CRUD functions start here

        internal Paycheck Create(Paycheck paycheckData)
        {
            string sql=@"
            INSERT INTO paycheck
            (accountId, paycheckDate, grossIncome, taxAmount, netAmount, savings, tithe, payPeriodStartDate, payPeriodEndDate, remainingIncome)
            VALUES
            (@accountId, @paycheckDate, @grossIncome, @taxAmount, @netAmount, @savings, @tithe, @payPeriodStartDate, @payPeriodEndDate, @remainingIncome)
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, paycheckData);
            paycheckData.Id = id;
            return paycheckData;
        }

        internal List<Paycheck> GetAll()
        {
            string sql = @"
                SELECT
                p.*,
                a.*
                FROM paycheck p 
                JOIN accounts a ON p.creator.id = a.id 
                GROUP BY (p.id)
            ";
            return _db.Query<Paycheck, Account, Paycheck>(sql, (paycheck, account) =>
            {
                paycheck.Account = account;
                return paycheck;
            }).ToList();
        }
    }
}