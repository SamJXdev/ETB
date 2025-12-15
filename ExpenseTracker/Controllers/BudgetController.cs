using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.DTOs;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _service;

        public BudgetController(IBudgetService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SetMonthlyBudget([FromBody] SetBudgetDto dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }

            var budget = new Budget
            {
                Month = dto.Month,
                Year = dto.Year,
                Limit = dto.Limit,
                UserId = userId
            };
            return Ok(_service.SetMonthlyBudget(budget));
        }

        [HttpGet("Month/{Month:int}/{Year:int}")]
        public IActionResult GetMonthlyBudget(int Month, int Year)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid token");
            }
            return Ok(_service.GetMonthlyBudget(Month, Year, userId));
        }
    }
}