using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.DTOs
{
    public class SetBudgetDto
    {
        public int Month { get; set; }
        public int Year { get; set;}
        public decimal Limit { get; set; } 
    }
}