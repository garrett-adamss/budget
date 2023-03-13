namespace budget.Services
{
    public class PaycheckSettingsService
    {
        private readonly PaycheckSettingsRepository _psRepo;

        public PaycheckSettingsService(PaycheckSettingsRepository psRepo)
        {
            _psRepo = psRepo;
        }

        private PaycheckSettings ConvertWholeNumberToDecimal(PaycheckSettings psData)
        {
            // Convert taxPercent to decimal
            decimal taxPercentDecimal = psData.taxPercent / 100m;
            psData.taxPercent = taxPercentDecimal;

            // Convert tithePercent to decimal
            decimal tithePercentDecimal = psData.tithePercent / 100m;
            psData.tithePercent = tithePercentDecimal;

            // Convert savingsPercent to decimal
            decimal savingsPercentDecimal = psData.savingsPercent / 100m;
            psData.savingsPercent = savingsPercentDecimal;

            //Convert incomePercent to decimal 
            decimal investmentsPercentDecimal = psData.investmentsPercent / 100m;
            psData.investmentsPercent = investmentsPercentDecimal;

            return (psData);
        }
        
        internal PaycheckSettings Create(PaycheckSettings psData, Account account)
        {
            PaycheckSettings ps = _psRepo.GetByAccountId(account.Id);
            if (ps != null)
            {
                throw new Exception("you can't create more than one set of settings");
            }
            psData = ConvertWholeNumberToDecimal(psData);
            return _psRepo.Create(psData);
        }

        internal PaycheckSettings GetByAccountId(string accountId)
        {
            PaycheckSettings ps = _psRepo.GetByAccountId(accountId);
            if (ps == null)
            {
                throw new Exception("No settings by that account id");
            }
            return ps;
        }

        internal PaycheckSettings Update(PaycheckSettings psData, Account account)
        {
            PaycheckSettings original = GetByAccountId(account.Id);
            if (original.AccountId != account.Id)
            {
                throw new Exception("You are not the creator of these settings");
            }
            original.savingsPercent = psData.savingsPercent != default ? psData.savingsPercent : original.savingsPercent;
            original.taxPercent = psData.taxPercent != default ? psData.taxPercent : original.taxPercent;
            original.tithePercent = psData.tithePercent != default ? psData.tithePercent : original.tithePercent;

            original = ConvertWholeNumberToDecimal(original);
            return _psRepo.Update(original);
        }

        internal string Delete(int id, Account account)
        {
            PaycheckSettings original = GetByAccountId(account.Id);
            if (original.AccountId != account.Id)
            {
                throw new Exception("Cannot delete, you are not the owner");
            }
            _psRepo.Delete(id);
            return "settings have been deleted";
        }

    }
}