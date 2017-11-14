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
    public class SavingsInvestmentsController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: SavingsInvestments
        public ActionResult Index()
        {
            var allocations = db.Allocations.OfType<SavingsInvestment>();
            return View(allocations.ToList());
        }

        // GET: SavingsInvestments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInvestment savingsInvestment = (SavingsInvestment)db.Allocations.Find(id);
            if (savingsInvestment == null)
            {
                return HttpNotFound();
            }
            return View(savingsInvestment);
        }

        // GET: SavingsInvestments/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name");
            return View();
        }

        // POST: SavingsInvestments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,AllocationType,Amount,AccountId,Institution,Notes,Apr,CurrentValue,DestinationAccountId")] SavingsInvestment savingsInvestment,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                if (recurrence != null) savingsInvestment.Recurrence = recurrence;
                db.Allocations.Add(savingsInvestment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.AccountId);
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.DestinationAccountId).OrderBy(x=>x.Text);
            return View(savingsInvestment);
        }

        // GET: SavingsInvestments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInvestment savingsInvestment = (SavingsInvestment)db.Allocations.Find(id);
            if (savingsInvestment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.AccountId);
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.DestinationAccountId);
            return View(savingsInvestment);
        }

        // POST: SavingsInvestments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,AllocationType,Amount,AccountId,Institution,Notes,Apr,CurrentValue,DestinationAccountId")] SavingsInvestment savingsInvestment,
            [Bind(Include = "RecurrenceFrequencyEnum, RecuranceStartDate, RecuranceEndDate, RecuranceDayNumber")] Recurrence recurrence)
        {
            if (ModelState.IsValid)
            {
                Allocation targetSI = db.Allocations.Find(savingsInvestment.Id);
                if (targetSI.Recurrence == null)
                {
                    db.Recurrences.AddOrUpdate(recurrence);
                    db.SaveChanges();
                    savingsInvestment.RecurrenceId = recurrence.Id;

                }
                else
                {
                    recurrence.Id = targetSI.Recurrence.Id;
                    savingsInvestment.RecurrenceId = targetSI.RecurrenceId;
                    db.Recurrences.AddOrUpdate(recurrence);
                }

                db.Allocations.AddOrUpdate(savingsInvestment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.AccountId);
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.DestinationAccountId).OrderBy(x =>x.Text);
            return View(savingsInvestment);
        }

        // GET: SavingsInvestments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingsInvestment savingsInvestment = (SavingsInvestment)db.Allocations.Find(id);
            if (savingsInvestment == null)
            {
                return HttpNotFound();
            }
            return View(savingsInvestment);
        }

        // POST: SavingsInvestments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavingsInvestment savingsInvestment = (SavingsInvestment)db.Allocations.Find(id);
            if (savingsInvestment.Recurrence != null)
            {
                var recurrence = db.Recurrences.Find(savingsInvestment.RecurrenceId);
                db.Recurrences.Remove(recurrence);
            }
            db.Allocations.Remove(savingsInvestment);

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
