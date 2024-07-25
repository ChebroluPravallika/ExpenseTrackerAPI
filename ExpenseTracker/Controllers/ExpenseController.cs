using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        public readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<TransactionDTO>> GetAllTransactions()
        {
            var transactions = await expenseService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDTO transaction)
        {
            await expenseService.AddTransaction(transaction);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransaction(string transactionId)
        {
            await expenseService.DeleteTransaction(transactionId);
            return Ok();
        }
    }
}