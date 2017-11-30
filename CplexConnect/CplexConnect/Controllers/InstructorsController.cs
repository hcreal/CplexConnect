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
    public class InstructorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructors
        public ActionResult Index()
        {
            var inputListPost = db.InputData.ToList();
            var tList = db.Instructors.ToList();


            foreach (var t in tList)
            {
                //linq to find instructor with same instructor to save assigned when paged loaded
                var temp = inputListPost.Where(a => a.Instructor_Id == t.ID).ToList();
                t.AssignCourseLoad = temp.Count();
                db.SaveChanges();

            }
            return View(db.Instructors.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {

            //tuple so we can pass this to the model
            var tuple = new Tuple<Instructor, InputData>(new Instructor(), new InputData());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //creates list of courses that instructor is assigned to 
            List<InputData> q = db.InputData.Where(c => c.Instructor_Id == id).ToList();
            ViewBag.ClassList = q;

            //retreives details about instructor, You have to do this with a tuple for some reason IDK
            Instructor instructor = db.Instructors.Find(id);
            tuple.Item1.ID = instructor.ID;
            tuple.Item1.FirstName = instructor.FirstName;
            tuple.Item1.LastName = instructor.LastName;
            tuple.Item1.InstructorID = instructor.InstructorID;
            tuple.Item1.IsClinical = instructor.IsClinical;
            tuple.Item1.MaxCourseLoad = instructor.MaxCourseLoad;
            tuple.Item1.PhoneNum = instructor.PhoneNum;
            tuple.Item1.PrimaryProgram = instructor.PrimaryProgram;
            tuple.Item1.AssignCourseLoad = q.Count;
            tuple.Item1.CourseDist = instructor.CourseDist;
            tuple.Item1.Email = instructor.Email;

            //retrieves details about courses taught
            InputData input = db.InputData.FirstOrDefault();
            tuple.Item2.ID = input.ID;
            tuple.Item2.Instructor_Id = input.Instructor_Id;
            tuple.Item2.Course_Id = input.Course_Id;
            tuple.Item2.CourseName = input.CourseName;


            if (tuple.Item1 == null)
            {
                return HttpNotFound();
            }
            return View(tuple);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            //course distribution drop down
            List<SelectListItem> courseDistDropDown = new List<SelectListItem>();
            courseDistDropDown.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown.Add(new SelectListItem { Text = "4", Value = "4" });

            //Course Distribution 2 drop down
            List<SelectListItem> courseDistDropDown2 = new List<SelectListItem>();
            courseDistDropDown2.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown2.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown2.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown2.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown2.Add(new SelectListItem { Text = "4", Value = "4" });

            //primary program drop down
            List<SelectListItem> primaryProgramDropDown = new List<SelectListItem>();
            primaryProgramDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "STATS", Value = "STATS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "AC", Value = "AC" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MGT", Value = "MGT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MKT", Value = "MKT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "LGS", Value = "LGS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "FI", Value = "FI" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "EC", Value = "EC" });


            //ViewBAg to See the dropdowns
            ViewBag.CourseDistDropDown = courseDistDropDown;
            ViewBag.CourseDistDropDown2 = courseDistDropDown2;
            ViewBag.PrimaryProgramDropDown = primaryProgramDropDown;

            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstructorID,FirstName,LastName,Email,PhoneNum,IsClinical,MaxCourseLoad,PrimaryProgram")] Instructor instructor, FormCollection form)
        {
            //primary program drop down
            List<SelectListItem> primaryProgramDropDown = new List<SelectListItem>();
            primaryProgramDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "STATS", Value = "STATS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "AC", Value = "AC" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MGT", Value = "MGT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MKT", Value = "MKT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "LGS", Value = "LGS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "FI", Value = "FI" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "EC", Value = "EC" });
            ViewBag.PrimaryProgramDropDown = primaryProgramDropDown;
            instructor.PrimaryProgram = form["PrimaryProgramDropDown"];

            //checks if all fields are complete before inputting instructor
            if (ModelState.IsValid)
            {

                instructor.CourseDist = 0;
                instructor.CourseDist2 = 0;
                instructor.AssignCourseLoad = 0;
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            //Course Distribution drop down
            List<SelectListItem> courseDistDropDown = new List<SelectListItem>();
            courseDistDropDown.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown.Add(new SelectListItem { Text = "4", Value = "4" });

            //Course Distribution 2 drop down
            List<SelectListItem> courseDistDropDown2 = new List<SelectListItem>();
            courseDistDropDown2.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown2.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown2.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown2.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown2.Add(new SelectListItem { Text = "4", Value = "4" });

            //primary program drop down
            List<SelectListItem> primaryProgramDropDown = new List<SelectListItem>();
            primaryProgramDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "STATS", Value = "STATS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "AC", Value = "AC" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MGT", Value = "MGT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MKT", Value = "MKT" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "LGS", Value = "LGS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "FI", Value = "FI" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "EC", Value = "EC" });

            var tuple = new Tuple<Instructor, InputData>(new Instructor(), new InputData());

            //retrieves list of courses taught by instructor
            List<InputData> q = db.InputData.Where(c => c.Instructor_Id == id).ToList();
            ViewBag.ClassList = q;

            //retrieves details about instructor
            Instructor instructor = db.Instructors.Find(id);
            tuple.Item1.ID = instructor.ID;
            tuple.Item1.FirstName = instructor.FirstName;
            tuple.Item1.LastName = instructor.LastName;
            tuple.Item1.InstructorID = instructor.InstructorID;
            tuple.Item1.IsClinical = instructor.IsClinical;
            tuple.Item1.MaxCourseLoad = instructor.MaxCourseLoad;
            tuple.Item1.PhoneNum = instructor.PhoneNum;
            tuple.Item1.PrimaryProgram = instructor.PrimaryProgram;
            tuple.Item1.AssignCourseLoad = q.Count;
            tuple.Item1.CourseDist = instructor.CourseDist;
            tuple.Item1.Email = instructor.Email;

            //retrieves list of courses taught by instructor 
            InputData input = db.InputData.FirstOrDefault();
            tuple.Item2.ID = input.ID;
            tuple.Item2.Instructor_Id = input.Instructor_Id;
            tuple.Item2.Course_Id = input.Course_Id;
            tuple.Item2.CourseName = input.CourseName;

            //ViewBag to see dropdown
            ViewBag.CourseDistDropDown = courseDistDropDown;
            ViewBag.CourseDistDropDown2 = courseDistDropDown2;
            ViewBag.PrimaryProgramDropDown = primaryProgramDropDown;

            //checking if instructor id has been called to edit 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(tuple);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstructorID,FirstName,LastName,Email,PhoneNum,IsClinical,MaxCourseLoad,PrimaryProgram,AssignCourseLoad,CourseDist")] Instructor instructor, FormCollection form)
        {

            //Course Distribution drop down
            List<SelectListItem> courseDistDropDown = new List<SelectListItem>();
            courseDistDropDown.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown.Add(new SelectListItem { Text = "4", Value = "4" });

            //Course Distribution 2 drop down
            List<SelectListItem> courseDistDropDown2 = new List<SelectListItem>();
            courseDistDropDown2.Add(new SelectListItem { Text = "0", Value = "0" });
            courseDistDropDown2.Add(new SelectListItem { Text = "1", Value = "1" });
            courseDistDropDown2.Add(new SelectListItem { Text = "2", Value = "2" });
            courseDistDropDown2.Add(new SelectListItem { Text = "3", Value = "3" });
            courseDistDropDown2.Add(new SelectListItem { Text = "4", Value = "4" });

            //primary program drop down
            List<SelectListItem> primaryProgramDropDown = new List<SelectListItem>();
            primaryProgramDropDown.Add(new SelectListItem { Text = "OM", Value = "OM" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "MIS", Value = "MIS" });
            primaryProgramDropDown.Add(new SelectListItem { Text = "STATS", Value = "STATS" });

            ViewBag.CourseDistDropDown = courseDistDropDown;
            ViewBag.CourseDistDropDown2 = courseDistDropDown2;
            ViewBag.PrimaryProgramDropDown = primaryProgramDropDown;

            //form colletion and save to db for course distribution
            instructor.PrimaryProgram = form["PrimaryProgramDropDown"];
            instructor.CourseDist = int.Parse(form["CourseDistDropDown"]);
            instructor.CourseDist2 = int.Parse(form["CourseDistDropDown2"]);
            db.Entry(instructor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

            //tuple repopulation
            var tuple = new Tuple<Instructor, InputData>(new Instructor(), new InputData());
            List<InputData> q = db.InputData.Where(c => c.Instructor_Id == instructor.ID).ToList();
            ViewBag.ClassList = q;
            //prepopulating the fields with the existing data on instructor
            Instructor instructorRepop = db.Instructors.Find(instructor.ID);
            tuple.Item1.ID = instructor.ID;
            tuple.Item1.FirstName = instructorRepop.FirstName;
            tuple.Item1.LastName = instructorRepop.LastName;
            tuple.Item1.InstructorID = instructorRepop.InstructorID;
            tuple.Item1.IsClinical = instructorRepop.IsClinical;
            tuple.Item1.MaxCourseLoad = instructorRepop.MaxCourseLoad;
            tuple.Item1.PhoneNum = instructorRepop.PhoneNum;
            tuple.Item1.PrimaryProgram = instructorRepop.PrimaryProgram;
            tuple.Item1.AssignCourseLoad = q.Count;
            tuple.Item1.CourseDist = instructorRepop.CourseDist;
            tuple.Item1.Email = instructorRepop.Email;


            InputData input = db.InputData.FirstOrDefault();
            tuple.Item2.ID = input.ID;
            tuple.Item2.Instructor_Id = input.Instructor_Id;
            tuple.Item2.Course_Id = input.Course_Id;
            tuple.Item2.CourseName = input.CourseName;

            return View(tuple);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if id of instructor to be deleted is inputted
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            //check if id cooresponds to instructor to delete
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
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
