namespace budget.Repositories
{
    public class PaycheckSettingsRepository
    {
        private readonly IDbConnection _db;

        public PaycheckSettingsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal PaycheckSettings Create(PaycheckSettings paycheckSettingData)
        {
            string sql = @"
            INSERT INTO paycheck_settings
            (accountId, taxPercent, savingsPercent, tithePercent, investmentsPercent)
            VALUES
            (@accountId, @taxPercent, @savingsPercent, @tithePercent, @investmentsPercent)
            ";
            int id = _db.ExecuteScalar<int>(sql, paycheckSettingData);
            paycheckSettingData.Id = id;
            return paycheckSettingData;
        }

        internal PaycheckSettings GetByAccountId(string accountId){
            string sql= @"
            SELECT
            ps.*,
            a.*
            FROM paycheck_settings ps
            JOIN accounts a ON ps.accountId = a.id
            WHERE a.id = @accountId
            ";
            return _db.Query<PaycheckSettings, Account, PaycheckSettings>(sql, (paycheckSetting, profile)=>
            {
                paycheckSetting.Account = profile;
                return paycheckSetting;
            }, new { accountId }, splitOn: "id").FirstOrDefault();
        }

        internal PaycheckSettings Update(PaycheckSettings newData)
        {
            string sql = @"
            UPDATE paycheck_settings SET
            accountId = @accountId,
            taxPercent = @taxPercent,
            savingsPercent = @savingsPercent,
            tithePercent = @tithePercent,
            investmentsPercent = @investmentsPercent
            WHERE id = @id;
            ";
            _db.Execute(sql, newData);
            return newData;
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM paycheck_settings
            WHERE id = @id
            ";
            _db.Execute(sql, new { id });
        }
    }
}