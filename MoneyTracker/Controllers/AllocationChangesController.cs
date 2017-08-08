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
    public class AllocationChangesController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: AllocationChanges
        public ActionResult Index()
        {
            var changeEvents = db.ChangeEvents.OfType<AllocationChange>(); ;
            return View(changeEvents.ToList());
        }

        // GET: AllocationChanges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocationChange allocationChange = (AllocationChange)db.ChangeEvents.Find(id);
            if (allocationChange == null)
            {
                return HttpNotFound();
            }
            return View(allocationChange);
        }

        // GET: AllocationChanges/Create
        public ActionResult Create()
        {
            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name");
            return View();
        }

        // POST: AllocationChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EffectiveDateTime,Amount,ChangeTypeEnum,Recurance,AllocationId")] AllocationChange allocationChange)
        {
            if (ModelState.IsValid)
            {
                db.ChangeEvents.Add(allocationChange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name", allocationChange.AllocationId);
            return View(allocationChange);
        }

        // GET: AllocationChanges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocationChange allocationChange = (AllocationChange)db.ChangeEvents.Find(id);
            if (allocationChange == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name", allocationChange.AllocationId);
            return View(allocationChange);
        }

        // POST: AllocationChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EffectiveDateTime,Amount,ChangeTypeEnum,Recurance,AllocationId")] AllocationChange allocationChange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocationChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllocationId = new SelectList(db.Allocations, "Id", "Name", allocationChange.AllocationId);
            return View(allocationChange);
        }

        // GET: AllocationChanges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocationChange allocationChange = (AllocationChange)db.ChangeEvents.Find(id);
            if (allocationChange == null)
            {
                return HttpNotFound();
            }
            return View(allocationChange);
        }

        // POST: AllocationChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocationChange allocationChange = (AllocationChange)db.ChangeEvents.Find(id);
            db.ChangeEvents.Remove(allocationChange);
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
