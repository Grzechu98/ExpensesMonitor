using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpensesMonitor.SharedLibrary.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
