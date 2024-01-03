using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BuckSaver.WEBAPI.Controllers
{
    [ApiController()]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        /// <summary>
        /// Get transactions by userId
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<List<GetTransactions>>> Get (int userId)
        {
            return Ok(await _transactionService.GetTransactionsByUserId(userId));
        }

        /// <summary>
        /// Top up user balance
        /// </summary>
        [HttpPost("topUp")]
        public async Task<ActionResult> TopUp([FromBody] TopUp topUp)
        {
            return Ok(await _transactionService.TopUp(topUp));
        }

        /// <summary>
        /// Transfer funds
        /// </summary>
        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] Transfer transfer)
        {
            await _transactionService.Transfer(transfer);
            return Ok();
        }
    }
}
