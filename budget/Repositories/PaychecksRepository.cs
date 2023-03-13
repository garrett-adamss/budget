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
            (accountId, paycheckDate, grossIncome, taxAmount, netAmount, savings, tithe, payPeriodStartDate, payPeriodEndDate, remainingIncome, details, investments)
            VALUES
            (@accountId, @paycheckDate, @grossIncome, @taxAmount, @netAmount, @savings, @tithe, @payPeriodStartDate, @payPeriodEndDate, @remainingIncome, @details, @investments)
            LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, paycheckData);
            paycheckData.Id = id;
            return paycheckData;
        }

        internal List<Paycheck> GetAllByAccountId(string userId)
        {
            string sql = @"
                SELECT
                p.*,
                a.*
                FROM paycheck p 
                JOIN accounts a ON p.accountId = a.id
                WHERE a.id = @userId
                GROUP BY (p.id)
            ";
            return _db.Query<Paycheck, Account, Paycheck>(sql, (paycheck, account) =>
            {
                paycheck.Account = account;
                return paycheck;
            }, new { userId }).ToList();
        }

        internal Paycheck GetOne(object id)
        {
            string sql = @"
                SELECT
                p.*,
                a.*
                FROM paycheck p
                JOIN accounts a ON p.accountId = a.id
                WHERE p.id = @id
            ";
            return _db.Query<Paycheck, Account, Paycheck>(sql, (paycheck, account)=>
            {
                paycheck.Account = account;
                return paycheck;
            }, new { id }).FirstOrDefault();
        }

        internal Paycheck Update(Paycheck newData)
        {
            string sql = @"
            UPDATE paycheck SET
            paycheckDate = @paycheckDate,
            grossIncome = @grossIncome,
            taxAmount = @taxAmount,
            netAmount = @netAmount,
            savings = @savings,
            tithe = @tithe,
            payPeriodStartDate = @payPeriodStartDate,
            payPeriodEndDate = @payPeriodEndDate,
            remainingIncome = @remainingIncome,
            details = @details,
            investments = @investments
            WHERE id = @id;
            ";
            _db.Execute(sql, newData);
            return newData;
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM paycheck
            WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}