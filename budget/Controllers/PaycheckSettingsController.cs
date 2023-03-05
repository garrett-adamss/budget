namespace budget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaycheckSettingsController : ControllerBase
    {
        private readonly PaycheckSettingsService _psService;
        private readonly Auth0Provider _auth0Provider;

        public PaycheckSettingsController(PaycheckSettingsService psService, Auth0Provider auth0Provider)
        {
            _psService = psService;
            _auth0Provider = auth0Provider;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PaycheckSettings>> Create([FromBody] PaycheckSettings psData)
        {
            try 
            {
              Account account = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
              psData.AccountId = account.Id;
              PaycheckSettings ps = _psService.Create(psData, account);
              ps.Account = account;
              return Ok(ps);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaycheckSettings>> GetByAccountId(string accountId)
        {
            try 
            {
                Account account = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                PaycheckSettings ps = _psService.GetByAccountId(accountId);
                return Ok(ps);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PaycheckSettings>> Update(int id, [FromBody] PaycheckSettings psData)
        {
            try 
            {
                Account account = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                psData.Id = id;
                PaycheckSettings ps = _psService.Update(psData, account);
                return Ok(ps); 
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
                Account account = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                string message = _psService.Delete(id, account);
                return Ok(message);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}