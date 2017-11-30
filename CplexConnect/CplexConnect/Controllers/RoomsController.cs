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
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            Buildings building = new Buildings();
            var buildingList = db.Buildings.ToList();
            Room room = new Room();
            room.BuildingList = buildingList;


            //BUILDING DROPDOWN
            //List<SelectListItem> buildingDropDown = new List<SelectListItem>();
            //buildingDropDown.Add(new SelectListItem { Text = "Building", Value = "Building", Disabled = true });
            //buildingDropDown.Add(new SelectListItem { Text = "Bidgood", Value = "Bidgood" });
            //buildingDropDown.Add(new SelectListItem { Text = "Alston", Value = "Alston" });
            //buildingDropDown.Add(new SelectListItem { Text = "Bashinsky Lab", Value = "Bashinsky Lab" });

            //ATTRIBUTE DROPDOWN
            List<SelectListItem> roomAttrDropDown = new List<SelectListItem>();
            roomAttrDropDown.Add(new SelectListItem { Text = "Attribute", Value = "Attribute", Disabled = true });
            roomAttrDropDown.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });


            ViewBag.RoomDropDown = roomAttrDropDown;
            //ViewBag.BuildingDropDown = buildingDropDown;
            ViewBag.ErrorMessage = "";


            return View(room);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoomID,RoomCapacity,RoomAttribute,Building")] Room room, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                //Setting error messages as null to call them later and reset them after retry
                ViewBag.ErrorMessage = ""; 
                var temp = "";
                //form collection for dropdowns
                room.RoomAttribute = form["RoomDropDown"];
                room.Building = form["BuildingDropDown"];
                //checking to make sure that the Building ID coresponds to the correct building, i.e. BD 471 is in bidgood
                temp = room.RoomID.Substring(0, 2);
                if (temp == "BD")
                {
                    temp = "Bidgood"; 
                }
                else if (temp == "AB")
                {
                    temp = "Alston"; 
                }
                else if (temp == "BL")
                {
                    temp = "Bashinsky"; 
                }
                if (temp == room.Building)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              else
                {
                    ViewBag.ErrorMessage = "Incorrect building for ID";
                    //return View(room); 
                }
                return RedirectToAction("Create");
                
            }


            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
           
            //ATTRIBUTE DROPDOWN
            List<SelectListItem> roomAttrDropDown = new List<SelectListItem>();
            roomAttrDropDown.Add(new SelectListItem { Text = "Attribute", Value = "Attribute", Disabled = true });
            roomAttrDropDown.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Lab", Value = "Lab" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Mass Lecture", Value = "Mass Lecture" });
            roomAttrDropDown.Add(new SelectListItem { Text = "Computer", Value = "Computer" });

            ////BUILDING DROPDOWN
            //List<SelectListItem> buildingDropDown = new List<SelectListItem>();
            //buildingDropDown.Add(new SelectListItem { Text = "Building", Value = "Building", Disabled = true });
            //buildingDropDown.Add(new SelectListItem { Text = "Bidgood", Value = "Bidgood" });
            //buildingDropDown.Add(new SelectListItem { Text = "Alston", Value = "Alston" });
            //buildingDropDown.Add(new SelectListItem { Text = "Bashinsky Lab", Value = "Bashinsky Lab" });

            //adding builings to the virtual list of buildings in room
            Buildings building = new Buildings();
            var buildingList = db.Buildings.ToList();
            Room room = new Room();
            room = db.Rooms.Find(id);
            room.BuildingList = buildingList;


            ViewBag.RoomDropDown = roomAttrDropDown;
            //ViewBag.BuildingDropDown = buildingDropDown;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            if (room == null)
            {
                return HttpNotFound();
            }



            return View(room);
        }
        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoomID,RoomCapacity,RoomAttribute,Building")] Room room, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                //formcollection from dropdowns
                room.RoomAttribute = form["RoomDropDown"];
                room.Building = form["BuildingList"];
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public ActionResult NewBuilding()
        {
            
            return View("NewBuilding");
        }

        [HttpPost]
        public ActionResult NewBuilding(FormCollection form)
        {
            Room building = new Room();
            building.Building = form["Building"];
            db.Rooms.Add(building);
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
