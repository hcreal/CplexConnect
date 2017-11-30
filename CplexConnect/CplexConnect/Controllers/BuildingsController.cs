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
    public class BuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buildings
        public ActionResult Index()
        {
            return View(db.Buildings.ToList());
        }

        //private ActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BuildingName")] Buildings buildings, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                
                db.Buildings.Add(buildings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buildings);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BuildingName")] Buildings buildings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buildings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buildings);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buildings buildings = db.Buildings.Find(id);
            if (buildings == null)
            {
                return HttpNotFound();
            }
            return View(buildings);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buildings buildings = db.Buildings.Find(id);
            db.Buildings.Remove(buildings);
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
