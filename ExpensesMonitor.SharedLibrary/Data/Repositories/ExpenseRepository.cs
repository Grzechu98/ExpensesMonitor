using ExpensesMonitor.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExpensesMonitor.SharedLibrary.Data.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<ExpenseModel>> GetExpenses(Func<ExpenseModel, bool> condition);
        Task<ExpenseModel> GetExpense(Func<ExpenseModel, bool> condition);
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
        public async Task<ExpenseModel> GetExpense(Func<ExpenseModel, bool> condition)
        {
            return await Task.FromResult(_context.Expenses.Where(condition).FirstOrDefault());
        }

        public async Task<IEnumerable<ExpenseModel>> GetExpenses(Func<ExpenseModel, bool> condition)
        {
            var result = await Task.FromResult(_context.Expenses.Include(e => e.Category).Where(condition).ToList());
            return result.OrderByDescending(e => e.CreationDate);
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
