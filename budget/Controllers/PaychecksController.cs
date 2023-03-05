namespace budget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaychecksController : ControllerBase
    {
        private readonly PaychecksService _paychecksService;
        private readonly Auth0Provider _auth0Provider;

        public PaychecksController(PaychecksService paychecksService, Auth0Provider auth0Provider)
        {
            _paychecksService = paychecksService;
            
            _auth0Provider = auth0Provider;
        }

        //Functions start here
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
        public async Task<ActionResult<List<Paycheck>>> GetAllByAccountId()
        {
            try
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                List<Paycheck> paychecks = _paychecksService.GetAllByAccountId(user);
                return Ok(paychecks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paycheck>> GetOne (int id)
        {
            try 
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                Paycheck paycheck = _paychecksService.GetOne(id, user?.Id);
                return Ok(paycheck);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Paycheck>> Update(int id, [FromBody] Paycheck PaycheckData)
        {
            try 
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                PaycheckData.Id = id;
                Paycheck Paycheck = _paychecksService.Update(PaycheckData, user);
                return Ok(Paycheck);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try 
            {
                Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _paychecksService.Delete(id, user);
                return Ok(message);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}