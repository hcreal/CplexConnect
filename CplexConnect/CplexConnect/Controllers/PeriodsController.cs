using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CplexConnect.Models;

namespace CplexConnect.Controllers
{
    public class PeriodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Periods
        public ActionResult Index()
        {
            return View(db.Periods.ToList());
        }

        // GET: Periods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periods periods = db.Periods.Find(id);
            if (periods == null)
            {
                return HttpNotFound();
            }
            return View(periods);
        }

        // GET: Periods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Period_ID,Period")] Periods periods)
        {
            if (ModelState.IsValid)
            {
                db.Periods.Add(periods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periods);
        }

        // GET: Periods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periods periods = db.Periods.Find(id);
            if (periods == null)
            {
                return HttpNotFound();
            }
            return View(periods);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Period_ID,Period")] Periods periods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periods);
        }

        // GET: Periods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periods periods = db.Periods.Find(id);
            if (periods == null)
            {
                return HttpNotFound();
            }
            return View(periods);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periods periods = db.Periods.Find(id);
            db.Periods.Remove(periods);
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
