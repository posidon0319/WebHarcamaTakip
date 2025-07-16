using Microsoft.AspNetCore.Mvc;
using WebHarcamaTakip.Models;
using WebHarcamaTakip.Services;
using System;
using System.Linq;

namespace WebHarcamaTakip.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index(string? category, bool? income)
        {
            var expenses = ExpenseService.GetAll();
            if (!string.IsNullOrEmpty(category))
                expenses = expenses.Where(x => x.Category == category).ToList();
            if (income.HasValue)
                expenses = expenses.Where(x => x.IsIncome == income.Value).ToList();

            ViewBag.Total = expenses.Where(x => x.IsIncome).Sum(x => x.Amount) -
                            expenses.Where(x => !x.IsIncome).Sum(x => x.Amount);

            return View(expenses.OrderByDescending(x => x.Date));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            expense.Date = DateTime.Now;
            ExpenseService.Add(expense);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            ExpenseService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
