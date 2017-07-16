using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoneyTracker.DAL;
using MoneyTracker.Models.ChangeEvents;

namespace MoneyTracker.Controllers
{
    [Authorize]
    public class AccountChangesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: AccountChanges
        public ActionResult Index()
        {
            var changeEvents = db.ChangeEvents.OfType<AccountChange>();
            return View(changeEvents.ToList());
        }

        // GET: AccountChanges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountChange accountChange = (AccountChange)db.ChangeEvents.Find(id);
            if (accountChange == null)
            {
                return HttpNotFound();
            }
            return View(accountChange);
        }

        // GET: AccountChanges/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            return View();
        }

        // POST: AccountChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EffectiveDateTime,Amount,ChangeTypeEnum,AccountId")] AccountChange accountChange)
        {
            if (ModelState.IsValid)
            {
                db.ChangeEvents.Add(accountChange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountChange.AccountId);
            return View(accountChange);
        }

        // GET: AccountChanges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountChange accountChange = (AccountChange)db.ChangeEvents.Find(id);
            if (accountChange == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountChange.AccountId);
            return View(accountChange);
        }

        // POST: AccountChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EffectiveDateTime,Amount,ChangeTypeEnum,AccountId")] AccountChange accountChange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", accountChange.AccountId);
            return View(accountChange);
        }

        // GET: AccountChanges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountChange accountChange = (AccountChange)db.ChangeEvents.Find(id);
            if (accountChange == null)
            {
                return HttpNotFound();
            }
            return View(accountChange);
        }

        // POST: AccountChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountChange accountChange = (AccountChange)db.ChangeEvents.Find(id);
            db.ChangeEvents.Remove(accountChange);
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
