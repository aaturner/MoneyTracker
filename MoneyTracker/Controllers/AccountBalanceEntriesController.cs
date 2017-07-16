using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTracker.DAL;
using MoneyTracker.Models;

namespace MoneyTracker.Controllers
{
    [Authorize]
    public class AccountBalanceEntriesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: AccountBalanceEntries
        public ActionResult Index()
        {
            var accountBalanceEntries = db.AccountBalanceEntries.Include(a => a.Account);
            return View(accountBalanceEntries.ToList());
        }

        // GET: AccountBalanceEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBalanceEntry accountBalanceEntry = db.AccountBalanceEntries.Find(id);
            if (accountBalanceEntry == null)
            {
                return HttpNotFound();
            }
            return View(accountBalanceEntry);
        }

        // GET: AccountBalanceEntries/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            return View();
        }

        // POST: AccountBalanceEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Amount,AccountId")] AccountBalanceEntry accountBalanceEntry)
        {
            if (ModelState.IsValid)
            {
                db.AccountBalanceEntries.Add(accountBalanceEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountBalanceEntry.AccountId);
            return View(accountBalanceEntry);
        }

        // GET: AccountBalanceEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBalanceEntry accountBalanceEntry = db.AccountBalanceEntries.Find(id);
            if (accountBalanceEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountBalanceEntry.AccountId);
            return View(accountBalanceEntry);
        }

        // POST: AccountBalanceEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Amount,AccountId")] AccountBalanceEntry accountBalanceEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountBalanceEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountBalanceEntry.AccountId);
            return View(accountBalanceEntry);
        }

        // GET: AccountBalanceEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountBalanceEntry accountBalanceEntry = db.AccountBalanceEntries.Find(id);
            if (accountBalanceEntry == null)
            {
                return HttpNotFound();
            }
            return View(accountBalanceEntry);
        }

        // POST: AccountBalanceEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountBalanceEntry accountBalanceEntry = db.AccountBalanceEntries.Find(id);
            db.AccountBalanceEntries.Remove(accountBalanceEntry);
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
