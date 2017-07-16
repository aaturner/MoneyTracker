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
using MoneyTracker.Models.Allocations;

namespace MoneyTracker.Controllers
{
    //TODO: Creating new Income Change Events does not enter an IncomeSourceId to the Db
    [Authorize]
    public class IncomeSourcesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: IncomeSources
        public ActionResult Index()
        {
            return View(db.IncomeSources.ToList());
        }

        // GET: IncomeSources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeSource incomeSource = db.IncomeSources.Find(id);
            if (incomeSource == null)
            {
                return HttpNotFound();
            }
            return View(incomeSource);
        }

        // GET: IncomeSources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomeSources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Amount,EffectiveDate,ChangeType,IncomeSourceId")] IncomeSource incomeSource)
        {
            if (ModelState.IsValid)
            {
                db.IncomeSources.Add(incomeSource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incomeSource);
        }

        // GET: IncomeSources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeSource incomeSource = db.IncomeSources.Find(id);
            if (incomeSource == null)
            {
                return HttpNotFound();
            }
            return View(incomeSource);
        }

        // POST: IncomeSources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] IncomeSource incomeSource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incomeSource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incomeSource);
        }

        // GET: IncomeSources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeSource incomeSource = db.IncomeSources.Find(id);
            if (incomeSource == null)
            {
                return HttpNotFound();
            }
            return View(incomeSource);
        }

        // POST: IncomeSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncomeSource incomeSource = db.IncomeSources.Find(id);
            db.IncomeSources.Remove(incomeSource);
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
