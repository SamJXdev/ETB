using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Services;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.DTOs;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateExpense([FromBody] CreateExpenseDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }
            var expense = new Expense
            {
                Title = dto.Title,
                Amount = dto.Amount,
                Category = dto.Category,
                Date = dto.Date,
                Notes = dto.Notes,
                UserId = userId
            };
            return Ok(_service.CreateExpense(expense));
        }
        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }
            return Ok(_service.GetAllExpenses(userId));
        }

        [HttpGet("id/{id:int}")]
        public IActionResult GetExpensesById(int id)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.GetExpensesById(id, userId));
        }

        [HttpGet("Month/{Month:int}/{Year:int}")]
        public IActionResult GetExpensesByMonth(int Month, int Year)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.GetExpensesByMonth(Month, Year, userId));
        }

        [HttpGet("category/{category}")]
        public IActionResult GetExpenseByCategory(string category)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.GetExpensesByCategory(category, userId));
        }

        [HttpGet("category/{category}/total")]
        public IActionResult GetCategoryTotals(string category)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.GetCategoryTotals(category, userId));
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateExpense(int id, [FromBody] Expense updatedExpense)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.UpdateExpense(id, updatedExpense, userId));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteExpense(int id)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            return Ok(_service.DeleteExpense(id, userId));
        }
    }
}