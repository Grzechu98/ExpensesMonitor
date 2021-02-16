using ExpensesMonitor.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ExpensesMonitor.SharedLibrary.Data.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<ExpenseModel>> GetExpenses(Func<ExpenseModel, bool> condition);
        Task<ExpenseModel> GetExpense(int id);
        Task InsertExpense(ExpenseModel expense);
        Task UpdateExpense(ExpenseModel expense);
        Task RemoveExpense(ExpenseModel expense);
    }
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly MainContext _context;

        public ExpenseRepository(MainContext context)
        {
            _context = context;
        }
        public async Task<ExpenseModel> GetExpense(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpenses(Func<ExpenseModel, bool> condition)
        {
            return await Task.FromResult(_context.Expenses.Where(condition).ToList());
        }

        public async Task InsertExpense(ExpenseModel expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveExpense(ExpenseModel expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpense(ExpenseModel expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
    }
}
