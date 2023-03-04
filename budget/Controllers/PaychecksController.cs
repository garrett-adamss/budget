namespace budget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaychecksController : ControllerBase
    {
        private readonly PaychecksService _paychecksService;
        private readonly Auth0Provider _auth0Provider;

        public PaychecksController(PaychecksService paychecksService)
        {
            _paychecksService = paychecksService;
        }

        //Functions start here
        // public Paycheck CalculatePaycheck(Paycheck paycheck)
        // {
        //     // Calculate taxes
        //     decimal taxPercent = paycheck.Account.TaxPercent / 100;
        //     decimal taxAmount = paycheck.GrossIncome * taxPercent;
        //     paycheck.TaxAmount = Math.Round(taxAmount, 2);

        //     // Calculate net income
        //     decimal netIncome = paycheck.GrossIncome - paycheck.TaxAmount;
        //     paycheck.NetIncome = Math.Round(netIncome, 2);

        //     // Calculate tithe
        //     decimal tithePercent = 0.1M;
        //     decimal titheAmount = paycheck.NetIncome * tithePercent;
        //     paycheck.Tithe = Math.Round(titheAmount, 2);

        //     // Calculate savings
        //     decimal savingsPercent = paycheck.Account.savingsPercent / 100;
        //     decimal savingsAmount = paycheck.NetIncome * savingsPercent;
        //     paycheck.Savings = Math.Round(savingsAmount, 2);

        //     // Calculate remaining income
        //     decimal remainingIncome = paycheck.NetIncome - paycheck.Tithe - paycheck.Savings;
        //     paycheck.remainingIncome = Math.Round(remainingIncome, 2);

        //     return paycheck;
        // }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Paycheck>> Create([FromBody] Paycheck paycheckData)
        {
            try
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                paycheckData.AccountId = user.Id;
                Paycheck paycheck = _paychecksService.Create(paycheckData, user);
                paycheck.Account = user;
                return Ok(paycheck);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Paycheck>> GetAll()
        {
            try
            {
                List<Paycheck> paychecks = _paychecksService.GetAll();
                return Ok(paychecks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}