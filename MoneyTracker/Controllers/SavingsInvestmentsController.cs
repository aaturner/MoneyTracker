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
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsMonthly,RecuranceDayNumber,RecuranceEndDate,AllocationType,Amount,AccountId,Institution,Notes,Apr,CurrentValue,DestinationAccountId")] SavingsInvestment savingsInvestment)
        {
            if (ModelState.IsValid)
            {
                db.Allocations.Add(savingsInvestment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.AccountId);
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.DestinationAccountId);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsMonthly,RecuranceDayNumber,RecuranceEndDate,AllocationType,Amount,AccountId,Institution,Notes,Apr,CurrentValue,DestinationAccountId")] SavingsInvestment savingsInvestment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingsInvestment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.AccountId);
            ViewBag.DestinationAccountId = new SelectList(db.Accounts, "Id", "Name", savingsInvestment.DestinationAccountId);
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
