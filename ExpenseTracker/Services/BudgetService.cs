using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context)
        {
            _context = context;
        }

        public Budget SetMonthlyBudget(Budget budget)
        {
            var existing = _context.Budgets.FirstOrDefault(b =>
                b.Month == budget.Month &&
                b.Year == budget.Year &&
                b.UserId == budget.UserId);

            if (existing == null)
            {
                _context.Budgets.Add(budget);
            }
            else
            {
                existing.Limit = budget.Limit;
                budget = existing;
            }
            _context.SaveChanges();
            return budget;
        }

        public Budget GetMonthlyBudget(int month, int year, int userId)
        {
            var existing = _context.Budgets.FirstOrDefault(b =>
                b.Month == month &&
                b.Year == year &&
                b.UserId == userId);

            return existing;
        }
    }
}