
using ExpensesApp.Data;
using ExpensesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Syncfusion.EJ2.Linq;
using System.Transactions;

namespace ExpensesApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller

    {
        private readonly ExpensesAppDBContext _context;
        public DashboardController(ExpensesAppDBContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Models.Transaction> SelectedTransactions = await _context.Transactions
                 .Include(x => x.Category)
                 .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                 .ToListAsync();

            int TotalExpense = SelectedTransactions
                .Sum(i => i.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("c0");

            //chart

            ViewBag.DoughnutChartData = SelectedTransactions
                .GroupBy(i =>
                {
                    return i.Category.CategoryId;
                })
                .Select(k => new
                {   categoryTitle = k.First().Category.Title,  
                    amount = k.Sum(i => i.Amount),
                    formattedAmount = k.Sum(i => i.Amount).ToString("C0"),
                })
                .OrderByDescending(l =>l.amount)
                .ToList();

            //spline chart
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();
            ViewBag.SplineChartData = from day in Last7Days
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day,
                                          expense = expense ==null? 0:expense.expense,
                                      };


            //Recent Transaction
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();

            return View();
    }
    }
    public class SplineChartData
    {
        public string? day;
        public int expense;
    }

}

