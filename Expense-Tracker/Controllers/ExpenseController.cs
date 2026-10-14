using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController (ExpenseService ExpServ) : ControllerBase
    {
        private readonly ExpenseService _expenseService = ExpServ;

        [HttpGet]
        public ActionResult GetExpenses()
        {
            return Ok(_expenseService.GetExpenses());
        }

        [HttpGet("{id}")]
        public ActionResult GetExpense(int Id)
        {
            var expense = _expenseService.GetExpenseById(Id);

            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public ActionResult PostExpense(Expense expense) 
        {
            _expenseService.AddExpense(expense);

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }
    }
}
