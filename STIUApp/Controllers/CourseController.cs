using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STIUApp.DAL;
using STIUApp.Models;
using PagedList;

namespace STIUApp.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Course
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            UpdateCourseEnrolled(db);

            ViewBag.NotTakenSortParm = String.IsNullOrEmpty(sortOrder) ? "nottaken_desc" : "";
            ViewBag.NotTakenSortParm = sortOrder == "NotTaken" ? "nottaken_desc" : "NotTaken";
            ViewBag.CompletedSortParm = sortOrder == "Completed" ? "completed_desc" : "Completed";
            ViewBag.EnrolledSortParm = sortOrder == "Enrolled" ? "enrolled_desc" : "Enrolled";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var courses = from s in db.Courses
                           select s;

            //Search if search box is not empty
            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s =>
                    s.Title.ToUpper().Contains(searchString.ToUpper())
                    ||
                    s.CourseID.ToUpper().Contains(searchString.ToUpper()) 
                    ||
                    s.Type.ToString().Contains(searchString) 
                    ||
                    s.Faculty.ToUpper().Contains(searchString.ToUpper())
                    );

            }

            switch (sortOrder)
            {   
                case "Completed":
                    courses = courses.OrderBy(s => s.Completed);
                    break;
                case "completed_desc":
                    courses = courses.OrderByDescending(s => s.Completed);
                    break;
                case "Enrolled":
                    courses = courses.OrderBy(s => s.Enrolled);
                    break;
                case "enrolled_desc":
                    courses = courses.OrderByDescending(s => s.Enrolled);
                    break;
                case "NotTaken":
                    courses = courses.OrderBy(s => s.NotTaken);
                    break;
                case "nottaken_desc":
                    courses = courses.OrderByDescending(s => s.NotTaken);
                    break;
                case "Type":
                    courses = courses.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    courses = courses.OrderByDescending(s => s.Type);
                    break;
                default:
                    courses = courses.OrderByDescending(s => s.NotTaken);
                    break;
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(courses.ToPagedList(pageNumber, pageSize));
        }


        //Update the number of enrolled/unenrolled/not taken for the course

        public void UpdateCourseEnrolled(SchoolContext db)
        {
            //Get list of all the courses
            ICollection<Course> AllCourses = db.Courses.ToList();
            int c=0, e=0, nt=0;

            try
            {
                //For every course get the number of enrolled from the enrollments entities 
                foreach (var course in AllCourses)
                {
                    c = course.Enrollments.Count(s => s.Status == "Completed");
                    course.Completed = c;
                    db.SaveChanges();

                    e = course.Enrollments.Count(s => s.Status == "Enrolled");
                    course.Enrolled = e;
                    db.SaveChanges();

                    nt = course.Enrollments.Count(s => s.Status == "Not Taken");
                    course.NotTaken = nt;
                    db.SaveChanges();
                } 
            } 

            catch (Exception ex) 
                {
                    Console.WriteLine(ex.InnerException);
                }
        }

        // GET: Course/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }



        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Prereq,Title,Credits,Faculty")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Prereq,Title,Credits,Faculty")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        // GET: Course/credit
        public ActionResult creditnumber()
        {
            List<int> number = new List<int>();
           number.Add(1);
           number.Add(3);
           number.Add(6);
           SelectList credit = new SelectList(number);
           ViewData["credit"] = credit;
           return View();

        }
    }
}
