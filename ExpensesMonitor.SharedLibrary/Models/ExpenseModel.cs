using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesMonitor.SharedLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
