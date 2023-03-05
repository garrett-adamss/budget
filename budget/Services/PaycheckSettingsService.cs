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
            return _psRepo.Create(psData);
        }

        internal PaycheckSettings GetOne(int id1, object id2)
        {
            throw new NotImplementedException();
        }

        internal PaycheckSettings GetByAccountId(string accountId)
        {
            PaycheckSettings ps = _psRepo.GetByAccountId(accountId);
            if (ps == null)
            {
                throw new Exception ("No PaycheckSettings by that account Id");
            }
            return ps;
        }

        internal PaycheckSettings Update(PaycheckSettings psData, Account account)
        {
            throw new NotImplementedException();
        }
        // internal PaycheckSettings Update(PaycheckSettings newData, Account account)
        // {
        //     PaycheckSettings original = GetByAccountId(newData.Account)
        // }

    }
}