using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using ExpensesMonitor.SharedLibrary.Data.Repositories;
using ExpensesMonitor.SharedLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpensesMonitor.Services
{
    public class ChartHelper
    {
        public CategoryModel Category { get; set; }
        public decimal Amount { get; set; }
    }
    public interface IChartService
    {
        Task<Chart> GenerateBasicExpensesChart(string userId);
        Task<Chart> GenerateExpensesChartForPeriodOfTime(DateTime period, string userId);
    }
    public class ChartService : IChartService
    {
        private readonly IExpenseRepository _repository;


        public ChartService(IExpenseRepository repository)
        {
            _repository = repository;
        }
        public async Task<Chart> GenerateBasicExpensesChart(string userId)
        {
            var toDisplay = await GetAmountPerCategory(TimePeriod.Month, userId);
            var amountperCategory = new List<double?>();
            
            var chart = new Chart { Type = Enums.ChartType.Bar };
            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();
           
            foreach (var item in toDisplay)
            {
                data.Labels.Add(item.Category.CategoryName);
                amountperCategory.Add(decimal.ToDouble(item.Amount));
            }

            var dataset = new BarDataset
            {
                Label = "Expenses per Category",
                Data = amountperCategory,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 1),
                },
            };

            data.Datasets = new List<Dataset> { dataset };

            chart.Data = data;

            chart.Options = await GetOptions();

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12,
                        Top = 10,
                        Bottom = 10
                    }
                }
            };

            return chart;
        }

        public async Task<Chart> GenerateExpensesChartForPeriodOfTime(DateTime period, string userId)
        {
            throw new NotImplementedException();
        }

        private async Task<List<ChartHelper>> GetAmountPerCategory(DateTime period, string userId)
        {
            var expenses = await _repository.GetExpenses(e => e.UserId == userId && e.CreationDate >= period);
            List<ChartHelper> result = expenses.GroupBy(e => e.Category).Select(t => new ChartHelper
            {
                Category = t.Key,
                Amount = t.Sum(ee => ee.Amount)
            }).ToList();

            return result;
        }

        private async Task<Options> GetOptions()
        {
            var options = new Options
            {
                Scales = new Scales()
            };

            var scales = new Scales
            {
                YAxes = new List<Scale>
                {
                    new CartesianScale
                    {
                        Ticks = new CartesianLinearTick
                        {
                            BeginAtZero = true
                        }
                    }
                },
                XAxes = new List<Scale>
                {
                    new BarScale
                    {
                        BarPercentage = 0.5,
                        BarThickness = 25,
                        MaxBarThickness = 28,
                        MinBarLength = 2,
                        GridLines = new GridLine()
                        {
                            OffsetGridLines = true
                        }
                    }
                }
            };

            options.Scales = scales;
            
            return await Task.FromResult(options);
        }
    }
}
