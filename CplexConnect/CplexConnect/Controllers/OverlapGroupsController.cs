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
    public class OverlapGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OverlapGroups

        public ActionResult Index()
        {
            return View(db.OverlapGroups.ToList());
        }

        //  GET: OverlapGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OverlapGroups overlapGroups = db.OverlapGroups.Find(id);
            overlapGroups.SectionList = db.Sections.ToList();
            if (overlapGroups == null)
            {
                return HttpNotFound();
            }
            return View(overlapGroups);
        }

        //  GET: OverlapGroups/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Sections, "ID", "Course");
            //Overlap group setting virtual list to sections for dropdown
            OverlapGroups overlapGroups = new OverlapGroups();
            List<Section> sectionList = db.Sections.ToList();
            overlapGroups.SectionList = sectionList;

            return View(overlapGroups);
        }

        // POST: OverlapGroups/Create
        //  To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OverlapGroup,Sections")] OverlapGroups overlapGroups, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                //Creates list finds formcolletion then splits them at comma
                List<Section> secFind = new List<Section>();
                var a = form["SectionList"];
                List<string> tempList = a.Split(',').ToList();
                foreach (var t in tempList)
                {
                    if (t != "")
                    {
                        //temp list to ints
                        int tt = int.Parse(t);
                        secFind.Add(db.Sections.Where(s => s.ID == tt).FirstOrDefault());
                    }
                }
                //add to model 
                foreach (var s in secFind)
                {
                    overlapGroups.Sections += (s.Course + '-' + s.SectionNumbers + ",");
                }

                overlapGroups.SectionList = db.Sections.ToList();
                db.OverlapGroups.Add(overlapGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Sections, "ID", "Course", overlapGroups.ID);
            return View(overlapGroups);
        }

        // GET: OverlapGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            OverlapGroups overlapGroups = new OverlapGroups();
            overlapGroups = db.OverlapGroups.Find(id);
            overlapGroups.SectionList = db.Sections.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (overlapGroups == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Sections, "ID", "Course", overlapGroups.ID);

            return View(overlapGroups);
        }

        // POST: OverlapGroups/Edit/5
        //  To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OverlapGroup,Sections")] OverlapGroups overlapGroups, FormCollection form)
        {
            //creates list and gets formcollection then adds strings to list from comma del
            List<Section> secFind = new List<Section>();
            var a = form["SectionList"];
            List<string> tempList = a.Split(',').ToList();
            foreach (var t in tempList)
            {
                if (t != "")
                {
                    //parse to an int then add to temp list
                    int tt = int.Parse(t);
                    secFind.Add(db.Sections.Where(s => s.ID == tt).FirstOrDefault());
                }
            }
            //add section to model
            foreach (var s in secFind)
            {
                overlapGroups.Sections += (s.Course + '-' + s.SectionNumbers + ",");
            }

            overlapGroups.SectionList = db.Sections.ToList();

            if (ModelState.IsValid)
            {
                db.Entry(overlapGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Sections, "ID", "Course", overlapGroups.ID);
            return View(overlapGroups);
        }

        //GET: OverlapGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OverlapGroups overlapGroups = db.OverlapGroups.Find(id);
            if (overlapGroups == null)
            {
                return HttpNotFound();
            }
            return View(overlapGroups);
        }

        //POST: OverlapGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OverlapGroups overlapGroups = db.OverlapGroups.Find(id);
            db.OverlapGroups.Remove(overlapGroups);
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
