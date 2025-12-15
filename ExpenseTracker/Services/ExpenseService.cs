using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;
        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public Expense CreateExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return expense;
        }

        public List<Expense> GetAllExpenses(int userId)
        {
            return _context.Expenses
                .Where(e => e.UserId == userId)
                .ToList();
        }

        public Expense GetExpensesById(int id, int userId)
        {
            var existing = _context.Expenses
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);

            return existing;
        }

        public List<Expense> GetExpensesByMonth(int month, int year, int userId)
        {
            var list = _context.Expenses
                .Where(e => e.Date.Month == month &&
                           e.Date.Year == year &&
                           e.UserId == userId)
                .ToList();
            return list;
        }

        public List<Expense> GetExpensesByCategory(string category, int userId)
        {
            var list = _context.Expenses
                .Where(e => e.Category == category && e.UserId == userId)
                .ToList();
            return list;
        }

        public decimal GetCategoryTotals(string category, int userId)
        {
            return _context.Expenses
                .Where(e => e.Category == category && e.UserId == userId)
                .Sum(e => e.Amount);
        }

        public Expense UpdateExpense(int id, Expense updatedExpense, int userId)
        {
            var existing = _context.Expenses
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);

            if (existing == null) return null;

            existing.Title = updatedExpense.Title;
            existing.Amount = updatedExpense.Amount;
            existing.Category = updatedExpense.Category;
            existing.Date = updatedExpense.Date;
            existing.Notes = updatedExpense.Notes;

            _context.SaveChanges();
            return existing;
        }

        public bool DeleteExpense(int id, int userId)
        {
            var existing = _context.Expenses
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);

            if (existing == null) return false;

            _context.Expenses.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}