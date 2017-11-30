using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CplexConnect.Models;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.Web.UI.WebControls;
using System.IO;
using MoreLinq;

namespace CplexConnect.Controllers
{

    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedule
        public ActionResult Index(string sortBy, string sortBySemester)
        {
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
            //so sortbysemester goes to the current verison
            scheduleViewModel.SemesterNumber = 0;
            //checks if dropdown is triggered through jQuery
            if (sortBySemester != null)
            {
                scheduleViewModel.SemesterNumber = int.Parse(sortBySemester);
            }
            else
            {
                sortBySemester = "0";
            }


            //Tuple of View model and Input Data
            var tuple = new Tuple<ScheduleViewModel, InputData>(new ScheduleViewModel(), new InputData());
            //allowing user to sort courses by program
            List<Section> sectionList = db.Sections.ToList();
            List<Instructor> instructList = db.Instructors.OrderBy(a => a.FirstName).ToList();

            if (sortBy == "OM")
            {
                tuple.Item1.SectionList = sectionList.Where(s => s.Program == "OM").ToList();
            }
            else if (sortBy == "MIS")
            {
                tuple.Item1.SectionList = sectionList.Where(s => s.Program == "MIS").ToList();
            }
            else if (sortBy == "ST")
            {
                tuple.Item1.SectionList = sectionList.Where(s => s.Program == "ST").ToList();
            }
            else
            {
                sortBy = "All";
                tuple.Item1.SectionList = sectionList;
            }


            //semster sort that checks if everything is distinct for the semester ID we are looking for and then creates a dropdown from all of those
            var semDb = db.InputData.DistinctBy(x => x.Semester_Id).Distinct().ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var a in semDb)
            {
                if (a.SemesterName == null)
                {
                    items.Add(new SelectListItem() { Text = "Current", Value = a.Semester_Id.ToString() });
                }
                else
                    items.Add(new SelectListItem() { Text = a.SemesterName, Value = a.Semester_Id.ToString() });
            }

            //sort everything
            string sortByStart = sortBy.Substring(0, 1);
            string sortByEnd = sortBy.Substring(1, sortBy.Length - 1);

            ViewBag.SortTheSems = items;
            // Viewbags back to the View
            ViewBag.SortBySemester = sortBySemester;
            ViewBag.SortBy = sortByStart.ToUpper() + sortByEnd;

            //TempData to go to post if needed
            tuple.Item1.ModelToList = db.InputData.ToList();
            tuple.Item1.InstructorList = instructList;
            TempData["sortBy"] = sortBy;
            TempData["sortBySemester"] = sortBySemester;
            return View(tuple);
        }



        [HttpPost]
        public ActionResult Index(ScheduleViewModel scheduleViewModel, FormCollection form, string sortBy, string sortBySemester)
        {
            //Tempdata
            sortBy = TempData["sortBy"].ToString();
            sortBySemester = TempData["sortBySemester"].ToString();
            // finds number from tempdata
            scheduleViewModel.SemesterNumber = int.Parse(sortBySemester);
            //creates list of everything
            List<Section> sectionList = db.Sections.ToList();
            List<Instructor> instructList = db.Instructors.OrderBy(a => a.FirstName).ToList();
            List<Tuple<Section, Instructor>> combinedList = new List<Tuple<Section, Instructor>>();
            scheduleViewModel.CombinedList = combinedList;
            //creates ability to sort by program
            if (sortBy == "OM")
            {
                scheduleViewModel.SectionList = sectionList.Where(s => s.Program == "OM").ToList();
            }
            else if (sortBy == "MIS")
            {
                scheduleViewModel.SectionList = sectionList.Where(s => s.Program == "MIS").ToList();
            }
            else if (sortBy == "ST")
            {
                scheduleViewModel.SectionList = sectionList.Where(s => s.Program == "ST").ToList();
            }
            else
            {
                sortBy = "All";
                scheduleViewModel.SectionList = sectionList;
            }


            scheduleViewModel.InstructorList = instructList;
            //List of inputdata
            List<InputData> inList = new List<InputData>();
            //List of strings of info from inList
            List<string> test = new List<string>();
            var counter = scheduleViewModel.SectionList.Count();
            for (int i = 1; i <= counter; i++)
            {
                //checking and getting each radio button
                var name = "radioCol" + i;
                name = form[name];
                if (name != null)
                {
                    //adding to the tuple
                    test.Add(name);
                    var tempName = int.Parse(name);
                    var newTup = new Tuple<Section, Instructor>(scheduleViewModel.SectionList.Where(s => s.ID == i).FirstOrDefault(), scheduleViewModel.InstructorList.Where(k => k.ID == tempName).FirstOrDefault());
                    combinedList.Add(newTup);
                }
                //Break when Null
                else
                    break;
            }


            scheduleViewModel.CombinedList = combinedList;
            //find everyclassID and add it to the list
            List<int> classIdList = db.InputData.Select(a => a.Course_Id).ToList();
            var inputList = db.InputData.ToList();
            //Iterate through combined list that is added from enough
            foreach (var a in combinedList)
            {
                InputData input = new InputData();
                input.Course_Id = a.Item1.ID;
                input.Instructor_Id = a.Item2.ID;
                input.CourseName = a.Item1.Course;
                //checks if list is null
                if (inputList.Count == 0)
                {
                    db.InputData.Add(input);
                    db.SaveChanges();

                }
                else
                {
                    //checks if the course has not already been added
                    if (!classIdList.Contains(input.Course_Id))
                    {
                        db.InputData.Add(input);
                        db.SaveChanges();
                    }
                    else
                    {
                        //checks if the course has not already been added
                        var removeItem = db.InputData.Where(c => c.Course_Id == input.Course_Id).FirstOrDefault();
                        input.Semester_Id = removeItem.Semester_Id;
                        db.InputData.Remove(removeItem);
                        db.InputData.Add(input);
                        db.SaveChanges();
                    }
                }
            }


            var inputListPost = db.InputData.ToList();
            var tList = db.Instructors.ToList();


            foreach (var t in tList)
            {
                var temp = inputListPost.Where(a => a.Instructor_Id == t.ID).ToList();
                t.AssignCourseLoad = temp.Count();
                db.SaveChanges();

            }
            //db.Schedules.Add(scheduleViewModel);
            return View("SaveIndex", scheduleViewModel);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Export()
        { 

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Export(FormCollection form)
        {
            //Creates a new semester. formcollection
            var name = form["SemesterName"];
            int counter = 0;
            //find the largest semester 
            var counterFinder = db.InputData.OrderByDescending(s => s.Semester_Id).First();
            counter = counterFinder.Semester_Id;
            //adds 
            counter++;
            InputData input = new InputData();
            List<InputData> inputList = db.InputData.ToList();

            foreach (var i in inputList)
            {
                //Change the name and Id if it is zero which is the current save
                var semChange = db.InputData.Where(x => x.Semester_Id == 0).ToList();
                foreach (var s in semChange)
                {
                    s.Semester_Id = counter;
                    s.SemesterName = name;
                    db.SaveChanges();

                }
            }

            //reset assigned course load
            var assignedReset = db.Instructors.ToList();
            foreach(var r in assignedReset)
            {
                r.AssignCourseLoad = 0;
                db.SaveChanges();
            }


            return View("../Home/Index");
            //var sections = new System.Data.DataTable("Sections");
            //sections.Columns.Add("Program", typeof(string));
            //sections.Columns.Add("Course ID", typeof(string));
            //sections.Columns.Add("Instructor", typeof(string));
            //sections.Columns.Add("Attribute", typeof(string));
            //sections.Columns.Add("Notes", typeof(string));

            //var allSections = db.Sections.ToList();

            //foreach (Section s in allSections)
            //{
            //    sections.Rows.Add(s.Program, s.Course, s.RoomReq);
            //}

            //var rooms = new System.Data.DataTable("Rooms");
            //rooms.Columns.Add("Building", typeof(string));
            //rooms.Columns.Add("Room Number", typeof(string));
            //rooms.Columns.Add("Capacity", typeof(string));
            //rooms.Columns.Add("Attribute", typeof(string));
            //rooms.Columns.Add("BuildingID", typeof(string));

            //var allRooms = db.Rooms.ToList();

            //foreach (Room s in allRooms)
            //{
            //    rooms.Rows.Add(s.Building, s.RoomID, s.RoomCapacity, s.RoomAttribute, s.RoomID);
            //}

            //var Capstone = new System.Data.DataTable("Capstone");
            //Capstone.Columns.Add("ID", typeof(string));
            ////Capstone.Columns.Add("Program", typeof(string));
            ////Capstone.Columns.Add("Course", typeof(string));
            //Capstone.Columns.Add("Room Req", typeof(string));
            //Capstone.Columns.Add("BuildingID", typeof(string));

            //var allCapstone = db.Rooms.ToList();

            //foreach (Room s in allRooms)
            //{
            //    rooms.Rows.Add(s.ID, s.RoomAttribute, s.RoomID);
            //}

            //var input = new System.Data.DataTable("S_i");
            ////input.Columns.Add("First Name", typeof(string));
            //input.Columns.Add("Instructor", typeof(string));
            //input.Columns.Add("Course ID", typeof(string));
            //input.Columns.Add("Attribute", typeof(string));
            //input.Columns.Add("Notes", typeof(string));

            //var allinputs = db.InputData.ToList();

            //foreach (InputData i in allinputs)
            //{
            //    input.Rows.Add(i.Instructor_Id, i.Course_Id, i.CourseName);
            //}

            //var workbook = new XLWorkbook();
            //workbook.Worksheets.Add(input);
            //workbook.Worksheets.Add(rooms);
            //workbook.Worksheets.Add(Capstone);
            //workbook.Worksheets.Add(sections);

            ////var grid = new System.Web.UI.WebControls.GridView();
            ////grid.DataSource = sections;
            ////grid.DataBind();
            ////grid.DataSource = Capstone;
            ////grid.DataBind();
            ////grid.DataSource = rooms;
            ////grid.DataBind();
            ////grid.DataSource = input;
            ////grid.DataBind();

            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("d") + ".xls");
            //Response.ContentType = "application/ms-excel";

            //Response.Charset = "";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            ////grid.RenderControl(htw);

            //using (MemoryStream MyMemoryStream = new MemoryStream())
            //{
            //    workbook.SaveAs(MyMemoryStream);
            //    MyMemoryStream.WriteTo(Response.OutputStream);
            //    Response.Flush();
            //    Response.End();
            //    return View();

        }
    }
}