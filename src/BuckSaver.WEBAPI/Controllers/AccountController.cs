using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BuckSaver.WEBAPI.Controllers
{
    [ApiController()]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Create an account
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CreateAccount>> CreateAccount([FromBody] CreateAccount createAccount)
        {
            var createdAccount = await _accountService.CreateAccount(createAccount);
            return Ok(createdAccount);
        }
    }
}
