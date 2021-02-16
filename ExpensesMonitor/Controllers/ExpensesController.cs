using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpensesMonitor.SharedLibrary.Data;
using ExpensesMonitor.SharedLibrary.Models;
using ExpensesMonitor.SharedLibrary.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ExpensesMonitor.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;

        public ExpensesController(IExpenseRepository repository,ICategoryRepository categoryRepository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetExpenses(e => e.UserId == _userManager.GetUserId(User)));
        }

        // GET: Expenses/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetCategories(), "Id", "CategoryName");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,CategoryId")] ExpenseModel expenseModel)
        {
            expenseModel.CreationDate = DateTime.Now;
            expenseModel.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
               await _repository.InsertExpense(expenseModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetCategories(), "Id", "CategoryName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseModel = await _repository.GetExpense(e => e.UserId == _userManager.GetUserId(User) && e.Id == id);
            if (expenseModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetCategories(), "Id", "CategoryName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,CreationDate,UserId,CategoryId")] ExpenseModel expenseModel)
        {
            if (id != expenseModel.Id)
            {
                return NotFound();
            }
          
            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateExpense(expenseModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ExpenseModelExists(expenseModel.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetCategories(), "Id", "CategoryName", expenseModel.CategoryId);
            return View(expenseModel);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseModel = await _repository.GetExpense(e => e.UserId == _userManager.GetUserId(User) && e.Id == id);
            if (expenseModel == null)
            {
                return NotFound();
            }

            return View(expenseModel);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseModel = await _repository.GetExpense(e => e.UserId == _userManager.GetUserId(User) && e.Id == id);
            await _repository.RemoveExpense(expenseModel);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExpenseModelExists(int id)
        {
            var expenseModel = await _repository.GetExpense(e => e.UserId == _userManager.GetUserId(User) && e.Id == id);
            return (expenseModel != null);
        }
    }
}
