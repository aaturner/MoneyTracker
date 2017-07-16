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
    public class IncomesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: Incomes
        public ActionResult Index()
        {
            var allocations = db.Allocations.OfType<Income>();
            return View(allocations.ToList());
        }

        // GET: Incomes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = (Income)db.Allocations.Find(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // GET: Incomes/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "Name");
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FullName");
            return View();
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,IsMonthly,RecuranceDayNumber,RecuranceEndDate,Amount,AccountId,PersonId,IncomeSourceId")] Income income)
        {
            if (ModelState.IsValid)
            {
                db.Allocations.Add(income);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", income.AccountId);
            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "Name", income.IncomeSourceId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FullName", income.PersonId);
            return View(income);
        }

        // GET: Incomes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = (Income)db.Allocations.Find(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", income.AccountId);
            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "Name", income.IncomeSourceId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FullName", income.PersonId);
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,IsMonthly,RecuranceDayNumber,RecuranceEndDate,Amount,AccountId,PersonId,IncomeSourceId")] Income income)
        {
            if (ModelState.IsValid)
            {
                db.Entry(income).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", income.AccountId);
            ViewBag.IncomeSourceId = new SelectList(db.IncomeSources, "Id", "Name", income.IncomeSourceId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FullName", income.PersonId);
            return View(income);
        }

        // GET: Incomes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = (Income)db.Allocations.Find(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Income income = (Income)db.Allocations.Find(id);
            db.Allocations.Remove(income);
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
