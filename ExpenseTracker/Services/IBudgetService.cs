using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IBudgetService
    {
        Budget SetMonthlyBudget(Budget budget);
        Budget GetMonthlyBudget(int month, int year, int userId);
    }
}