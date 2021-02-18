﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesMonitor.SharedLibrary.Models
{
    public static class TimePeriod
    {
        public static DateTime Week { get { return DateTime.Now.AddDays(-7); } }
        public static DateTime Month { get { return DateTime.Now.AddMonths(-1); } }
        public static DateTime Year { get { return DateTime.Now.AddYears(-1); } }
        public static DateTime AllTime { get { return DateTime.Now; } }
    }
}
