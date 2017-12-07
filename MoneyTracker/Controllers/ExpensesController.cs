using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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
            var allocations = db.Allocations.OfType<Expense>().OrderBy(x => x.Name);
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
                if(recurrence != null) expense.Recurrence = recurrence;
                db.Allocations.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", expense.AccountId);
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name", expense.ExpenseCategoryId).OrderBy(x => x.Text);
            return View("~/Views/Budget/Index.cshtml");
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
            if (expense.Recurrence == null)
            {
                expense.Recurrence = new Recurrence()
                {
                    RecuranceStartDate = System.DateTime.Today,
                    RecurrenceFrequencyEnum = Models.Enums.RecurrenceEnum.Monthly
                };
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,AccountId,ExpenseCategoryId")] Expense expense,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                Allocation targetExpense = db.Allocations.Find(expense.Id);
                if (targetExpense.Recurrence == null)
                {
                    db.Recurrences.AddOrUpdate(recurrence);
                    db.SaveChanges();
                    expense.RecurrenceId = recurrence.Id;

                }
                else
                {
                    recurrence.Id = targetExpense.Recurrence.Id;
                    expense.RecurrenceId = targetExpense.RecurrenceId;
                    db.Recurrences.AddOrUpdate(recurrence);
                }

                db.Allocations.AddOrUpdate(expense);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", expense.AccountId);
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories, "Id", "Name", expense.ExpenseCategoryId).OrderBy(x => x.Text);
            return View("~/Views/Budget/Index.cshtml");
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
            if (expense.Recurrence != null)
            {
                var recurrence = db.Recurrences.Find(expense.RecurrenceId);
                db.Recurrences.Remove(recurrence);
            }
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
