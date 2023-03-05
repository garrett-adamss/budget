namespace budget.Services
{
    public class PaycheckSettingsService
    {
        private readonly PaycheckSettingsRepository _psRepo;

        public PaycheckSettingsService(PaycheckSettingsRepository psRepo)
        {
            _psRepo = psRepo;
        }
        
        internal PaycheckSettings Create (PaycheckSettings psData, Account account)
        {
            PaycheckSettings ps = _psRepo.GetByAccountId(account.Id);
            if (ps != null)
            {
                throw new Exception("you can't create more than one set of settings");
            }
            return _psRepo.Create(psData);
        }

        internal PaycheckSettings GetByAccountId(string accountId)
        {
            PaycheckSettings ps = _psRepo.GetByAccountId(accountId);
            if (ps == null)
            {
                throw new Exception ("No settings by that account id");
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

            return _psRepo.Update(original);
        }

        internal string Delete(int id, Account account)
        {
            PaycheckSettings original = GetByAccountId(account.Id);
            if(original.AccountId != account.Id)
            {
                throw new Exception("Cannot delete, you are not the owner");
            }
            _psRepo.Delete(id);
            return "settings have been deleted";
        }

    }
}