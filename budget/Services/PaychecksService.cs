namespace budget.Services
{
    public class PaychecksService
    {
        private readonly PaychecksRepository _paycheckRepo;
        private readonly PaycheckSettingsService _psService;


        public PaychecksService(PaychecksRepository paychecksRepo, PaycheckSettingsService ps)
        {
            _paycheckRepo = paychecksRepo;
            _psService = ps;
        }

        private Paycheck CalculatePaycheck(Paycheck paycheck)
        {
            // Get paycheck_settings for the user
            PaycheckSettings ps = _psService.GetByAccountId(paycheck.AccountId);

            // Calculate taxes
            decimal taxPercent = ps.taxPercent / 100;
            decimal taxAmount = paycheck.GrossIncome * taxPercent;
            paycheck.TaxAmount = Math.Round(taxAmount, 2);

            // Calculate net income
            decimal netIncome = paycheck.GrossIncome - paycheck.TaxAmount;
            paycheck.NetIncome = Math.Round(netIncome, 2);

            // Calculate tithe
            decimal tithePercent = ps.tithePercent / 100;
            decimal titheAmount = paycheck.NetIncome * tithePercent;
            paycheck.Tithe = Math.Round(titheAmount, 2);

            // Calculate savings
            decimal savingsPercent = ps.savingsPercent / 100;
            decimal savingsAmount = paycheck.NetIncome * savingsPercent;
            paycheck.Savings = Math.Round(savingsAmount, 2);

            // Calculate investments
            decimal investmentsPercent = ps.investmentsPercent / 100;
            decimal investmentsAmount = paycheck.NetIncome * investmentsPercent;
            paycheck.Investments = Math.Round(investmentsAmount, 2);

            // Calculate remaining income
            decimal remainingIncome = paycheck.NetIncome - paycheck.Tithe - paycheck.Savings - paycheck.Investments;
            paycheck.RemainingIncome = Math.Round(remainingIncome, 2);

            return paycheck;
        }

        internal Paycheck Create(Paycheck paycheckData, Account user)
        {
            paycheckData = CalculatePaycheck(paycheckData);
            return _paycheckRepo.Create(paycheckData);
        }

        internal List<Paycheck> GetAllByAccountId(Account user)
        {
            if(user == null)
            {
                throw new Exception("User cannot be null");
            }
            if(string.IsNullOrEmpty(user.Id))
            {
                throw new Exception("User id cannot be null or empty");
            }
            return _paycheckRepo.GetAllByAccountId(user.Id);
        }

        internal Paycheck GetOne(int id, string userId)
        {
            Paycheck paycheck = _paycheckRepo.GetOne(id);
            if (paycheck == null)
            {
                throw new Exception("No paycheck by that id");
            }
            return paycheck;
        }

        internal Paycheck Update(Paycheck newData, Account user)
        {
            Paycheck original = GetOne(newData.Id, user.Id);
            if (original.AccountId != user.Id)
            {
                throw new Exception("you are not the creator of this paycheck");
            }
            original.PaycheckDate = newData.PaycheckDate !=null ? newData.PaycheckDate : original.PaycheckDate;
            original.GrossIncome = newData.GrossIncome != default ? newData.GrossIncome : original.GrossIncome;
            original.TaxAmount = newData.TaxAmount != default ? newData.TaxAmount : original.TaxAmount;
            original.NetIncome = newData.NetIncome != default ? newData.NetIncome : original.NetIncome;
            original.Savings = newData.Savings != default ? newData.Savings : original.Savings;
            original.Tithe = newData.Tithe != default ? newData.Tithe : original.Tithe;
            original.PayPeriodStartDate = newData.PayPeriodStartDate != null ? newData.PayPeriodStartDate : original.PayPeriodStartDate;
            original.PayPeriodEndDate = newData.PayPeriodEndDate != null ? newData.PayPeriodEndDate : original.PayPeriodEndDate;
            original.RemainingIncome = newData.RemainingIncome != default ? newData.RemainingIncome : original.RemainingIncome;
            original.Details = newData.Details ?? original.Details;

            original = CalculatePaycheck(original);
            return _paycheckRepo.Update(original);
        }

        internal string Delete(int id, Account user)
        {
            Paycheck original = GetOne(id, user.Id);
            if(original.AccountId != user.Id)
            {
                throw new Exception ("Cannot delete this Paycheck");
            }
            _paycheckRepo.Delete(id);
            return $"Paycheck from {original.PayPeriodStartDate} - {original.PayPeriodEndDate} deleted";
        }

    }
}