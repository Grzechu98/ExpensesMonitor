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

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }
    }
}
