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
    public class SectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sections
        public ActionResult Index()
        {
            return View(db.Sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            //SEMESTER DROPDOWN
            List<SelectListItem> semesterDropDown = new List<SelectListItem>();
            semesterDropDown.Add(new SelectListItem { Text = "Semester", Value = "Semester", Disabled = true });
            semesterDropDown.Add(new SelectListItem { Text = "Fall", Value = "Fall" });
            semesterDropDown.Add(new SelectListItem { Text = "Spring", Value = "Spring" });
            semesterDropDown.Add(new SelectListItem { Text = "Summer", Value = "Summer" });

            //DEPARTMENT DROPDOWN
            List<SelectListItem> departmentDropDown = new List<SelectListItem>();
            departmentDropDown.Add(new SelectListItem { Text = "DEPT", Value = "DEPT", Disabled = true });
            departmentDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            departmentDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            departmentDropDown.Add(new SelectListItem { Text = "ST", Value = "ST" });
            departmentDropDown.Add(new SelectListItem { Text = "MBA", Value = "MBA" });
            departmentDropDown.Add(new SelectListItem { Text = "AC", Value = "AC" });
            departmentDropDown.Add(new SelectListItem { Text = "MGT", Value = "MGT" });
            departmentDropDown.Add(new SelectListItem { Text = "MKT", Value = "MKT" });
            departmentDropDown.Add(new SelectListItem { Text = "LGS", Value = "LGS" });
            departmentDropDown.Add(new SelectListItem { Text = "FI", Value = "FI" });
            departmentDropDown.Add(new SelectListItem { Text = "EC", Value = "EC" });

            //DEPARTMENT DROPDOWN
            List<SelectListItem> roomReqDropDown = new List<SelectListItem>();
            roomReqDropDown.Add(new SelectListItem { Text = "Requirements", Value = "Requirements", Disabled = true });
            roomReqDropDown.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            roomReqDropDown.Add(new SelectListItem { Text = "Capstone", Value = "Capstone" });
            roomReqDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
            roomReqDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });
            roomReqDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });

            //TIME RANGE DROPDOWN
            //List<SelectListItem> timeDropDown = new List<SelectListItem>();
            //timeDropDown.Add(new SelectListItem { Text = "Grad MW/TR, 75 min, 8 AM - 3:30 PM", Value = "1" });
            //timeDropDown.Add(new SelectListItem { Text = "Under Grad MW, 75 min, 2 PM - 3:30 PM", Value = "2" });
            //timeDropDown.Add(new SelectListItem { Text = "MWF Early, 50 min, 8 AM - 1 PM", Value = "3" });
            //timeDropDown.Add(new SelectListItem { Text = "Late, 75 min, 5- 6:30", Value = "4" });
            //timeDropDown.Add(new SelectListItem { Text = "Under Grad TR, 75 min, 8 AM - 3:30 PM", Value = "5" });
            //timeDropDown.Add(new SelectListItem { Text = "All", Value = "6" });

            ViewBag.SemesterDropDown = semesterDropDown;
            ViewBag.DepartmentDropDown = departmentDropDown;
            ViewBag.RoomReqDropDown = roomReqDropDown;
            //ViewBag.TimeDropDown = timeDropDown;
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Course,SectionNumbers,Semester,IsOnline,RoomReq,SectionCapacity,Program,TimeRange")] Section section, FormCollection form, string[] TimeRanges)
        {
            if (ModelState.IsValid)
            {
                //FormCollection from dropdown
                section.Semester = form["SemesterDropDown"];
                section.RoomReq = form["RoomReqDropDown"];
                section.Program = form["DepartmentDropDown"];
                //section.SectionNumbers = form["SectionNumbers"];

                //if statements for TimeRange combination. this is good:)
                int count = TimeRanges.Count();
                if (count == 1 && TimeRanges.Contains("1"))
                {
                    section.TimeRange = "1";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("2"))
                {
                    section.TimeRange = "2";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "3";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 1 && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "4";
                    ViewBag.TimeRangeDisplay = "Late, 75 min, 5- 6:30";
                }
                else if (count == 1 && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "5";
                    ViewBag.TimeRangeDisplay = "Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("6"))
                {
                    section.TimeRange = "6";
                    ViewBag.TimeRangeDisplay = "All";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("2"))
                {
                    section.TimeRange = "12";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "13";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "14";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "15";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "123";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "124";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "125";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "134";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "135";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "145";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "1234";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1235";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1245";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1345";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "23";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "24";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "25";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "234";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5 - 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "235";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "245";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5 - 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "2345";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5 - 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "34";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "35";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "345";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "45";
                    ViewBag.TimeRangeDisplay = "Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else 
                {
                    section.TimeRange = "6";
                    ViewBag.TimeRangeDisplay = "All";
                }
                //section.TimeRange = form["TimeDropDown"];
                //switch (section.TimeRange)
                //{
                //    case "1":
                //        ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM";
                //        break;
                //    case "2":
                //        ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM";
                //        break;
                //    case "3":
                //        ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM";
                //        break;
                //    case "4":
                //        ViewBag.TimeRangeDisplay = "Late, 75 min, 5- 6:30";
                //        break;
                //    case "5":
                //        ViewBag.TimeRangeDisplay = "Under Grad TR, 75 min, 8 AM - 3:30 PM";
                //        break;
                //    default:
                //        ViewBag.TimeRangeDisplay = "All";
                //        break;
                //}
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            //SEMESTER DROPDOWN
            List<SelectListItem> semesterDropDown = new List<SelectListItem>();
            semesterDropDown.Add(new SelectListItem { Text = "Semester", Value = "Semester", Disabled = true });
            semesterDropDown.Add(new SelectListItem { Text = "Fall", Value = "Fall" });
            semesterDropDown.Add(new SelectListItem { Text = "Spring", Value = "Spring" });
            semesterDropDown.Add(new SelectListItem { Text = "Summer", Value = "Summer" });

            //DEPARTMENT DROPDOWN
            List<SelectListItem> departmentDropDown = new List<SelectListItem>();
            departmentDropDown.Add(new SelectListItem { Text = "DEPT", Value = "DEPT", Disabled = true });
            departmentDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            departmentDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            departmentDropDown.Add(new SelectListItem { Text = "ST", Value = "ST" });
            departmentDropDown.Add(new SelectListItem { Text = "MBA", Value = "MBA" });
            departmentDropDown.Add(new SelectListItem { Text = "AC", Value = "AC" });
            departmentDropDown.Add(new SelectListItem { Text = "MGT", Value = "MGT" });
            departmentDropDown.Add(new SelectListItem { Text = "MKT", Value = "MKT" });
            departmentDropDown.Add(new SelectListItem { Text = "LGS", Value = "LGS" });
            departmentDropDown.Add(new SelectListItem { Text = "FI", Value = "FI" });
            departmentDropDown.Add(new SelectListItem { Text = "EC", Value = "EC" });

            //DEPARTMENT DROPDOWN
            List<SelectListItem> roomReqDropDown = new List<SelectListItem>();
            roomReqDropDown.Add(new SelectListItem { Text = "Requirements", Value = "Requirements", Disabled = true });
            roomReqDropDown.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            roomReqDropDown.Add(new SelectListItem { Text = "Capstone", Value = "Capstone" });
            roomReqDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
            roomReqDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });
            roomReqDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });


            //TIME RANGE DROPDOWN
            //List<SelectListItem> timeDropDown = new List<SelectListItem>();
            //timeDropDown.Add(new SelectListItem { Text = "Grad MW/TR, 75 min, 8 AM - 3:30 PM", Value = "1" });
            //timeDropDown.Add(new SelectListItem { Text = "Under Grad MW, 75 min, 2 PM - 3:30 PM", Value = "2" });
            //timeDropDown.Add(new SelectListItem { Text = "MWF Early, 50 min, 8 AM - 1 PM", Value = "3" });
            //timeDropDown.Add(new SelectListItem { Text = "Late, 75 min, 5- 6:30", Value = "4" });
            //timeDropDown.Add(new SelectListItem { Text = "Under Grad TR, 75 min, 8 AM - 3:30 PM", Value = "5" });
            //timeDropDown.Add(new SelectListItem { Text = "All", Value = "6" });

            
            

            ViewBag.SemesterDropDown = semesterDropDown;
            ViewBag.DepartmentDropDown = departmentDropDown;
            ViewBag.RoomReqDropDown = roomReqDropDown;
            //ViewBag.TimeDropDown = timeDropDown;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            //var check1 = "6";
            //if(section.TimeRange == MWF Early, 50 min, 8 AM - 1 PM)
            //{
            //   check="3"
            //}



            if (section == null)
            {
                return HttpNotFound();
            }

            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Course,Semester,IsOnline,RoomReq,SectionCapacity,Program,TimeRange")] Section section, FormCollection form, string[] TimeRanges)
        {
            if (ModelState.IsValid)
            {

                //formcollection for dropdown
                section.Semester = form["SemesterDropDown"];
                section.RoomReq = form["RoomReqDropDown"];
                section.Program = form["DepartmentDropDown"];

                //if statements for TimeRange combination
                int count = TimeRanges.Count();
                if (count == 1 && TimeRanges.Contains("1"))
                {
                    section.TimeRange = "1";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("2"))
                {
                    section.TimeRange = "2";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "3";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 1 && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "4";
                    ViewBag.TimeRangeDisplay = "Late, 75 min, 5- 6:30";
                }
                else if (count == 1 && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "5";
                    ViewBag.TimeRangeDisplay = "Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 1 && TimeRanges.Contains("6"))
                {
                    section.TimeRange = "6";
                    ViewBag.TimeRangeDisplay = "All";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("2"))
                {
                    section.TimeRange = "12";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "13";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "14";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("1") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "15";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "123";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "124";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "125";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "134";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "135";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("1") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "145";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "1234";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1235";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("2") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1245";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("1") && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "1345";
                    ViewBag.TimeRangeDisplay = "Grad MW/TR, 75 min, 8 AM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("3"))
                {
                    section.TimeRange = "23";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "24";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("2") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "25";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "234";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5 - 6:30";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "235";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("2") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "245";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR Late, 75 min, 5 - 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 4 && TimeRanges.Contains("2") && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "2345";
                    ViewBag.TimeRangeDisplay = "Under Grad MW, 75 min, 2 PM - 3:30 PM OR MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5 - 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("3") && TimeRanges.Contains("4"))
                {
                    section.TimeRange = "34";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30";
                }
                else if (count == 2 && TimeRanges.Contains("3") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "35";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 3 && TimeRanges.Contains("3") && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "345";
                    ViewBag.TimeRangeDisplay = "MWF Early, 50 min, 8 AM - 1 PM OR Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else if (count == 2 && TimeRanges.Contains("4") && TimeRanges.Contains("5"))
                {
                    section.TimeRange = "45";
                    ViewBag.TimeRangeDisplay = "Late, 75 min, 5- 6:30 OR Under Grad TR, 75 min, 8 AM - 3:30 PM";
                }
                else
                {
                    section.TimeRange = "6";
                    ViewBag.TimeRangeDisplay = "All";
                }

                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(section);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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
