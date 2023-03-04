namespace budget.Services
{
    public class PaychecksService
    {
        private readonly PaychecksRepository _paycheckRepo;

        public PaychecksService(PaychecksRepository paychecksRepo)
        {
            _paycheckRepo = paychecksRepo;
        }

        internal Paycheck Create(Paycheck paycheckData, Account user)
        {
            return _paycheckRepo.Create(paycheckData);
        }

        internal List<Paycheck> GetAll()
        {
            return _paycheckRepo.GetAll();
        }
    }
}