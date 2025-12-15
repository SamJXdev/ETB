using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IExpenseService
    {
        Expense CreateExpense(Expense expense);
        List<Expense> GetAllExpenses(int userId);
        Expense GetExpensesById(int id, int userId);
        List<Expense> GetExpensesByMonth(int month, int year, int userId);
        List<Expense> GetExpensesByCategory(string category, int userId);
        decimal GetCategoryTotals(string category, int userId);
        Expense UpdateExpense(int id, Expense updatedExpense, int userId);
        bool DeleteExpense(int id, int userId);
    }
}