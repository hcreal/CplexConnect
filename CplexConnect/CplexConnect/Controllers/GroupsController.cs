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
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            List<Section> sectionList = new List<Section>();
            sectionList = db.Sections.ToList();
            Groups group = new Groups();
            group.SectionList = sectionList;


            return View(group);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Group")] Groups groups, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                // Create a List of sections
                List<Section> secFind = new List<Section>();
                //Form Collection to get from the dropdowns
                var a = form["SectionList"];
                //split from comma delimited string
                List<string> tempList = a.Split(',').ToList();
                //iterate through the list of strings
                foreach(var t in tempList)
                {
                    if (t != "")
                    {
                        //parse and add to list
                        int tt = int.Parse(t);
                        secFind.Add(db.Sections.Where(s => s.ID == tt).FirstOrDefault());
                    }
                }
                //iterate through list to add to model
                foreach (var s in secFind)
                {
                    groups.Courses += (s.Course + ", ");
                }
                db.Groups.Add(groups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groups);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //add to the model group to pass through
            List<Section> sectionList = new List<Section>();
            sectionList = db.Sections.ToList();
            Groups group = db.Groups.Find(id);
            group.SectionList = sectionList;


            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Group")] Groups groups, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                // Create a List of sections
                List<Section> secFind = new List<Section>();
                //Form Collection to get from the dropdowns
                var a = form["SectionList"];
                //split from comma delimited string
                List<string> tempList = a.Split(',').ToList();
                //iterate through the list of strings
                foreach (var t in tempList)
                {
                    if (t != "")
                    {
                        int tt = int.Parse(t);
                        secFind.Add(db.Sections.Where(s => s.ID == tt).FirstOrDefault());
                    }
                }

                foreach (var s in secFind)
                {
                    groups.Courses += (s.Course + ",");
                }
                db.Entry(groups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groups);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groups groups = db.Groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groups groups = db.Groups.Find(id);
            db.Groups.Remove(groups);
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
