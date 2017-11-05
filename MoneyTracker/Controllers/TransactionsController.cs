using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTracker.AppModels;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Utilities;


namespace MoneyTracker.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: Transactions
        public ActionResult Index(int selectedMonth = 0, int selectedYear = 0)
        {
            TransactionCenter transactionCenter = new TransactionCenter();
            
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem()
            {
                Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + "(" + x + ")",
                Value = x.ToString()
            }), "Value", "Text");
            ViewBag.Years = new SelectList(Enumerable.Range(General.GetFirstTransactionYear(),
                General.YearsToDisplay()).Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString()
            }), "Value", "Text");

            transactionCenter.SelectedMonth = selectedMonth == 0 ? DateTime.Now.Month : selectedMonth;
            transactionCenter.SelectedYear = selectedYear == 0 ? DateTime.Now.Year : selectedYear;


            transactionCenter.TransactionList = db.Transactions.Where(t => t.TransactionDate.Month == transactionCenter.SelectedMonth
                                                          && t.TransactionDate.Year == transactionCenter.SelectedYear).ToList();
            transactionCenter.TransactionList = transactionCenter.TransactionList.OrderByDescending(x => x.TransactionDate).ToList();
            //var transactions = db.Transactions.Include(t => t.Account).Include(t => t.Allocation);
            return View(transactionCenter);
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name").OrderBy(x => x.Text);
            ViewBag.AllocationId =  new SelectList(db.Allocations, "Id", "Name").OrderBy(x => x.Text);
            
            Transaction trans = new Transaction();
            trans.EnteredDate = DateTime.Now;
            trans.TransactionDate = DateTime.Now;
            return View(trans);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransactionType,TransactionDate,EnteredDate,Description,Amount,AccountId,AllocationId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name", transaction.AllocationId).OrderBy(x => x.Text);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            var allocations = new SelectList(db.Allocations, "Id", "Name", transaction.AllocationId).OrderBy(x => x.Text);
            ViewBag.AllocationId = allocations;
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransactionType,TransactionDate,EnteredDate,Description,Amount,AccountId,AllocationId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", transaction.AccountId);
            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name", transaction.AllocationId).OrderBy(x => x.Text);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
