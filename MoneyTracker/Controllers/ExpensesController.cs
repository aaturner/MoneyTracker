using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTracker.DAL;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models;

namespace MoneyTracker.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: Expenses
        public ActionResult Index()
        {
            var allocations = db.Allocations.OfType<Expense>();
            return View(allocations.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = (Expense)db.Allocations.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name").OrderBy(x => x.Text);
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Amount,AccountId,ExpenseCategoryId")] Expense expense,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                expense.Recurrence = recurrence;
                db.Allocations.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", expense.AccountId);
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name", expense.ExpenseCategoryId).OrderBy(x => x.Text);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = (Expense)db.Allocations.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", expense.AccountId);
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name", expense.ExpenseCategoryId).OrderBy(x => x.Text);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsMonthly,RecuranceDayNumber,RecuranceEndDate,Amount,AccountId,ExpenseCategoryId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", expense.AccountId);
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name", expense.ExpenseCategoryId).OrderBy(x => x.Text);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = (Expense)db.Allocations.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = (Expense)db.Allocations.Find(id);
            db.Allocations.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
