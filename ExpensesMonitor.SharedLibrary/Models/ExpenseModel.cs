using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesMonitor.SharedLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
