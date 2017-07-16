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
    public class LoanBalanceEntriesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: LoanBalanceEntries
        public ActionResult Index()
        {
            var loanBalanceEntries = db.LoanBalanceEntries.Include(l => l.Loan);
            return View(loanBalanceEntries.ToList());
        }

        // GET: LoanBalanceEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanBalanceEntry loanBalanceEntry = db.LoanBalanceEntries.Find(id);
            if (loanBalanceEntry == null)
            {
                return HttpNotFound();
            }
            return View(loanBalanceEntry);
        }

        // GET: LoanBalanceEntries/Create
        public ActionResult Create()
        {
            ViewBag.LoanId = new SelectList(db.Allocations.OfType<Loan>(), "Id", "Name");
            return View();
        }

        // POST: LoanBalanceEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Amount,LoanId")] LoanBalanceEntry loanBalanceEntry)
        {
            if (ModelState.IsValid)
            {
                db.LoanBalanceEntries.Add(loanBalanceEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanId = new SelectList(db.Allocations, "Id", "Name", loanBalanceEntry.LoanId);
            return View(loanBalanceEntry);
        }

        // GET: LoanBalanceEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanBalanceEntry loanBalanceEntry = db.LoanBalanceEntries.Find(id);
            if (loanBalanceEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanId = new SelectList(db.Allocations, "Id", "Name", loanBalanceEntry.LoanId);
            return View(loanBalanceEntry);
        }

        // POST: LoanBalanceEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Amount,LoanId")] LoanBalanceEntry loanBalanceEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanBalanceEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanId = new SelectList(db.Allocations, "Id", "Name", loanBalanceEntry.LoanId);
            return View(loanBalanceEntry);
        }

        // GET: LoanBalanceEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanBalanceEntry loanBalanceEntry = db.LoanBalanceEntries.Find(id);
            if (loanBalanceEntry == null)
            {
                return HttpNotFound();
            }
            return View(loanBalanceEntry);
        }

        // POST: LoanBalanceEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanBalanceEntry loanBalanceEntry = db.LoanBalanceEntries.Find(id);
            db.LoanBalanceEntries.Remove(loanBalanceEntry);
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
