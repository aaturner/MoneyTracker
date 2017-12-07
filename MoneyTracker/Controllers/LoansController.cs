using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: Loans
        public ActionResult Index()
        {
            var allocations = db.Allocations.OfType<Loan>().OrderBy(x => x.Name);
            return View(allocations.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = (Loan)db.Allocations.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name").OrderBy(x => x.Text);
            ViewBag.LoanAccountId = new SelectList(db.Accounts, "Id", "Name").OrderBy(x => x.Text);
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsMonthly,Amount,AccountId,LoanAccountId")] Loan loan,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                if(recurrence != null) loan.Recurrence = recurrence;
                db.Allocations.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", loan.AccountId).OrderBy(x => x.Text);
            ViewBag.LoanAccountId = new SelectList(db.Accounts, "Id", "Name", loan.LoanAccountId).OrderBy(x => x.Text);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = (Loan)db.Allocations.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            if (loan.Recurrence == null)
            {
                loan.Recurrence = new Recurrence()
                {
                    RecuranceStartDate = System.DateTime.Today,
                    RecurrenceFrequencyEnum = Models.Enums.RecurrenceEnum.Monthly
                };
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", loan.AccountId).OrderBy(x => x.Text);
            ViewBag.LoanAccountId = new SelectList(db.Accounts, "Id", "Name", loan.LoanAccountId).OrderBy(x => x.Text);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsMonthly,Amount,AccountId")] Loan loan,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                Allocation targetLoan = db.Allocations.Find(loan.Id);
                if (targetLoan.Recurrence == null)
                {
                    db.Recurrences.AddOrUpdate(recurrence);
                    db.SaveChanges();
                    loan.RecurrenceId = recurrence.Id;

                }
                else
                {
                    recurrence.Id = targetLoan.Recurrence.Id;
                    loan.RecurrenceId = targetLoan.RecurrenceId;
                    db.Recurrences.AddOrUpdate(recurrence);
                }

                db.Allocations.AddOrUpdate(loan);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", loan.AccountId).OrderBy(x => x.Text);
            ViewBag.LoanAccountId = new SelectList(db.Accounts, "Id", "Name", loan.LoanAccountId).OrderBy(x => x.Text);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = (Loan)db.Allocations.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = (Loan)db.Allocations.Find(id);
            if (loan.Recurrence != null)
            {
                var recurrence = db.Recurrences.Find(loan.RecurrenceId);
                db.Recurrences.Remove(recurrence);
            }
            db.Allocations.Remove(loan);
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
