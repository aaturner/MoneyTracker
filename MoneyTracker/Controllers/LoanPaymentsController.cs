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
    public class LoanPaymentsController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: LoanPayments
        public ActionResult Index()
        {
            return View(db.LoanPayments.ToList());
        }

        // GET: LoanPayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanPayment loanPayment = db.LoanPayments.Find(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // GET: LoanPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoansId,Amount,PostDate")] LoanPayment loanPayment)
        {
            if (ModelState.IsValid)
            {
                db.LoanPayments.Add(loanPayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loanPayment);
        }

        // GET: LoanPayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanPayment loanPayment = db.LoanPayments.Find(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // POST: LoanPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoansId,Amount,PostDate")] LoanPayment loanPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanPayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanPayment);
        }

        // GET: LoanPayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanPayment loanPayment = db.LoanPayments.Find(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // POST: LoanPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanPayment loanPayment = db.LoanPayments.Find(id);
            db.LoanPayments.Remove(loanPayment);
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
