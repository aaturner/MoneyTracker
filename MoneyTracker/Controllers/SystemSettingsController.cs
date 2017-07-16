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
    public class SystemSettingsController : Controller
    {
        private PrimaryContext db = new PrimaryContext();

        // GET: SystemSettings
        public ActionResult Index()
        {
            return View(db.SystemSettings.ToList());
        }

        // GET: SystemSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // GET: SystemSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Setting,SettingValue,SettingDate")] SystemSetting systemSetting)
        {
            if (ModelState.IsValid)
            {
                db.SystemSettings.Add(systemSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemSetting);
        }

        // GET: SystemSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // POST: SystemSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Setting,SettingValue,SettingDate")] SystemSetting systemSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemSetting);
        }

        // GET: SystemSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            if (systemSetting == null)
            {
                return HttpNotFound();
            }
            return View(systemSetting);
        }

        // POST: SystemSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemSetting systemSetting = db.SystemSettings.Find(id);
            db.SystemSettings.Remove(systemSetting);
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
