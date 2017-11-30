using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CplexConnect
{
    public class Class1
    {
    }

    // GET: Rooms/Create
    public ActionResult Create()
    {
        //BUILDING DROPDOWN
        List<SelectListItem> buildingDropDown = new List<SelectListItem>();
        buildingDropDown.Add(new SelectListItem { Text = "Building", Value = "Building", Disabled = true });
        buildingDropDown.Add(new SelectListItem { Text = "Bidgood", Value = "Bidgood" });
        buildingDropDown.Add(new SelectListItem { Text = "Alston", Value = "Alston" });
        buildingDropDown.Add(new SelectListItem { Text = "Bashinsky Lab", Value = "Bashinsky Lab" });

        //ATTRIBUTE DROPDOWN
        List<SelectListItem> roomAttrDropDown = new List<SelectListItem>();
        roomAttrDropDown.Add(new SelectListItem { Text = "Attribute", Value = "Attribute", Disabled = true });
        roomAttrDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
        roomAttrDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });
        roomAttrDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });


        ViewBag.RoomDropDown = roomAttrDropDown;
        ViewBag.BuildingDropDown = buildingDropDown;


        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,RoomId,RoomCapacity,Building,RoomAttribute")] Room room, FormCollection form)
    {
        if (ModelState.IsValid)
        {
            db.Rooms.Add(room);
            room.RoomAttribute = form["RoomDropDown"];
            room.Building = form["BuildingDropDown"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        return View(room);
    }


    // GET: Rooms/Edit/5
    public ActionResult Edit(int? id)
    {
        //ATTRIBUTE DROPDOWN
        List<SelectListItem> roomAttrDropDown = new List<SelectListItem>();
        roomAttrDropDown.Add(new SelectListItem { Text = "Attribute", Value = "Attribute", Disabled = true });
        roomAttrDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
        roomAttrDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });
        roomAttrDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });

        //BUILDING DROPDOWN
        List<SelectListItem> buildingDropDown = new List<SelectListItem>();
        buildingDropDown.Add(new SelectListItem { Text = "Building", Value = "Building", Disabled = true });
        buildingDropDown.Add(new SelectListItem { Text = "Bidgood", Value = "Bidgood" });
        buildingDropDown.Add(new SelectListItem { Text = "Alston", Value = "Alston" });
        buildingDropDown.Add(new SelectListItem { Text = "Bashinsky Lab", Value = "Bashinsky Lab" });



        ViewBag.RoomDropDown = roomAttrDropDown;
        ViewBag.BuildingDropDown = buildingDropDown;
    }


    // POST: Rooms/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,RoomId,RoomCapacity,Building,RoomAttribute")] Room room, FormCollection form)
    {
        if (ModelState.IsValid)
        {
            db.Entry(room).State = EntityState.Modified;
            room.RoomAttribute = form["RoomDropDown"];
            room.Building = form["BuildingDropDown"];
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(room);
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

        //DEPARTMENT DROPDOWN
        List<SelectListItem> roomReqDropDown = new List<SelectListItem>();
        roomReqDropDown.Add(new SelectListItem { Text = "Requirements", Value = "Requirements", Disabled = true });
        roomReqDropDown.Add(new SelectListItem { Text = "Capstone", Value = "Capstone" });
        roomReqDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
        roomReqDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });
        roomReqDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });

        ViewBag.SemsterDropDown = semesterDropDown;
        ViewBag.DepartmentDropDown = departmentDropDown;
        ViewBag.RoomReqDropDown = roomReqDropDown;

        return View();
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

        //DEPARTMENT DROPDOWN
        List<SelectListItem> roomReqDropDown = new List<SelectListItem>();
        roomReqDropDown.Add(new SelectListItem { Text = "Requirements", Value = "Requirements", Disabled = true });
        roomReqDropDown.Add(new SelectListItem { Text = "Capstone", Value = "Capstone" });
        roomReqDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
        roomReqDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });
        roomReqDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });

        ViewBag.SemsterDropDown = semesterDropDown;
        ViewBag.DepartmentDropDown = departmentDropDown;
        ViewBag.RoomReqDropDown = roomReqDropDown;



        @Html.DropDownList("BuildingDropDown", null, new { @class = "form-control" })