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
    public class TimeRangesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeRanges
        public ActionResult Index()
        {
            TimeRange t = new TimeRange();
            List<TimeRange> tr = new List<TimeRange>();
            List<Periods> periodList = new List<Periods>();
            List<Periods> prettyP = new List<Periods>();
            periodList = db.Periods.ToList();
            t.PeriodsList = periodList;
            tr = db.TimeRanges.ToList();


            //List<string> tempCourse = new List<string>();
            //foreach (var cs in tr)
            //{
            //    if (cs.Periods != null)
            //    {
            //        List<string> coursesSplit = cs.Periods.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            //        foreach (var c in coursesSplit)
            //        {
            //            foreach (var p in periodList)
            //            {
            //                if (c == p.ID.ToString())
            //                {
            //                    tempCourse.Add(p.Period);
            //                }
            //            }
            //        }
            //    }
            //}

           // ViewBag.PeriodList = tempCourse;

            return View(tr);
        }

        // GET: TimeRanges/Details/5
        public ActionResult Details(int? id)
        {
            TimeRange tr = new TimeRange();
            List<Periods> periodList = new List<Periods>();
            periodList = db.Periods.ToList();
            tr.PeriodsList = periodList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRange TimeRange = db.TimeRanges.Find(id);
            if (TimeRange == null)
            {
                return HttpNotFound();
            }
            return View(TimeRange);
        }

        // GET: TimeRanges/Create
        public ActionResult Create()
        {
            TimeRange tr = new TimeRange();
            List<Periods> periodList = new List<Periods>();
            periodList = db.Periods.ToList();
            tr.PeriodsList = periodList;

            return View(tr);
        }

        // POST: TimeRanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Range")] TimeRange TimeRange, FormCollection form)
        {
            //TIME RANGE DROPDOWN
            List<SelectListItem> timeDropDown = new List<SelectListItem>();
            timeDropDown.Add(new SelectListItem { Text = "Grad MW/TR, 75 min, 8 AM - 3:30 PM", Value = "1", Disabled = true });
            timeDropDown.Add(new SelectListItem { Text = "Under Grad MW, 75 min, 2 PM - 3:30 PM", Value = "2" });
            timeDropDown.Add(new SelectListItem { Text = "MWF Early, 50 min, 8 AM - 1 PM", Value = "3" });
            timeDropDown.Add(new SelectListItem { Text = "Late, 75 min, 5- 6:30", Value = "4" });
            timeDropDown.Add(new SelectListItem { Text = "Under Grad TR, 75 min, 8 AM - 3:30 PM", Value = "5" });

            List<string> checkList = new List<string>();
            List<Periods> periodList = new List<Periods>();
            periodList = db.Periods.ToList();
            int counter = periodList.Count();
            for (int i = 1; i <= counter; i++)
            {
                var checkName = i.ToString();
                checkName = form[checkName];
                if (checkName != null)
                {
                    checkList.Add(checkName);
                }
            }

            foreach (var r in checkList)
            {
                foreach (var t in periodList)
                {
                    if (r == t.ID.ToString())
                    {
                        TimeRange.Periods += t.Period + " , ";
                    }
                }
            }


            if (ModelState.IsValid)
            {
                db.TimeRanges.Add(TimeRange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(TimeRange);


        }

        // GET: TimeRanges/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Periods> periodList = new List<Periods>();
            periodList = db.Periods.ToList();

            //TIME RANGE DROPDOWN
            List<SelectListItem> timeDropDown = new List<SelectListItem>();
            timeDropDown.Add(new SelectListItem { Text = "Grad MW/TR, 75 min, 8 AM - 3:30 PM", Value = "1", Disabled = true });
            timeDropDown.Add(new SelectListItem { Text = "Under Grad MW, 75 min, 2 PM - 3:30 PM", Value = "2" });
            timeDropDown.Add(new SelectListItem { Text = "MWF Early, 50 min, 8 AM - 1 PM", Value = "3" });
            timeDropDown.Add(new SelectListItem { Text = "Late, 75 min, 5- 6:30", Value = "4" });
            timeDropDown.Add(new SelectListItem { Text = "Under Grad TR, 75 min, 8 AM - 3:30 PM", Value = "5" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRange TimeRange = db.TimeRanges.Find(id);
            TimeRange.PeriodsList = periodList;
            if (TimeRange == null)
            {
                return HttpNotFound();
            }
            return View(TimeRange);

        }

        // POST: TimeRanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Range")] TimeRange TimeRange, FormCollection form)
        {

            List<string> checkList = new List<string>();
            List<Periods> periodList = new List<Periods>();
            periodList = db.Periods.ToList();
            TimeRange.PeriodsList = periodList;
            int counter = periodList.Count();
            for (int i = 1; i <= counter; i++)
            {
                var checkName = i.ToString();
                checkName = form[checkName];
                if (checkName != null)
                {
                    checkList.Add(checkName);
                }
            }

            foreach (var r in checkList)
            {
                foreach (var t in periodList)
                {
                    if (r == t.ID.ToString())
                    {
                        TimeRange.Periods += t.Period + " , ";
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(TimeRange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TimeRange);
        }

        // GET: TimeRanges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRange TimeRange = db.TimeRanges.Find(id);
            if (TimeRange == null)
            {
                return HttpNotFound();
            }
            return View(TimeRange);
        }

        // POST: TimeRanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeRange TimeRange = db.TimeRanges.Find(id);
            db.TimeRanges.Remove(TimeRange);
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
