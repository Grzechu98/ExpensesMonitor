using ExpensesMonitor.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExpensesMonitor.SharedLibrary.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategory(int id);
        Task InsertCategory(CategoryModel category);
        Task UpdateCategory(CategoryModel category);
        Task RemoveCategory(CategoryModel category);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MainContext _context;

        public CategoryRepository(MainContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task InsertCategory(CategoryModel category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCategory(CategoryModel category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(CategoryModel category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
