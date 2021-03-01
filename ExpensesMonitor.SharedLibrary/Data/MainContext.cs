using ExpensesMonitor.SharedLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesMonitor.SharedLibrary.Data
{
    public class MainContext : DbContext
    {
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseModel>().HasOne(e => e.Category).WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId).IsRequired();

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, CategoryName = "Bills"},
                new CategoryModel { Id = 2, CategoryName = "Fast foods" },
                new CategoryModel { Id = 3, CategoryName = "Shopping" },
                new CategoryModel { Id = 4, CategoryName = "Entertainment" },
                new CategoryModel { Id = 5, CategoryName = "Alcohol" },
                new CategoryModel { Id = 6, CategoryName = "Fuel" },
                new CategoryModel { Id = 7, CategoryName = "Cosmetics" },
                new CategoryModel { Id = 8, CategoryName = "Others" }
                );
        }
    }
}
