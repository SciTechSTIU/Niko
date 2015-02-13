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
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MajorSortParm = sortOrder == "Major" ? "major_desc" : "Major";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            var students = from s in db.Students
                           select s;

            //Search if search box is not empty
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.LastName.ToUpper().Contains(searchString.ToUpper()) //Search by last name
                    ||
                    s.FirstName.ToUpper().Contains(searchString.ToUpper()) //Search by first name
                    ||
                    s.StudentID.ToString().Contains(searchString) //Search by student id
                    ||
                    s.Major.ToUpper().Contains(searchString.ToUpper()) //Search by major
                    );

            }


            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Major":
                    students = students.OrderBy(s => s.Major);
                    break;
                case "major_desc":
                    students = students.OrderByDescending(s => s.Major);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }



        // GET: Student/Details/5
        public ActionResult Details(int? StudentID)
        {
            if (StudentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(StudentID);
           

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        // GET: Student/Create
        public ActionResult Create()
        {
            

            return View();
        }

        //Maya: add student enrollments

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Major,Minor,Type")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                //to create the new Gen Ed

                if (student.Major == "IT")
                {
                    var enrollments = new List<Enrollment>
                    {
                        new Enrollment{StudentID = student.StudentID, CourseID="ENG101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="ENG102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="ENG103", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="MAT102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="STA101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SCI105", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="MIS103", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="LIB299", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="BUS299", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="ART101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="GEO101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="HIS101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="THA101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="HIS102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="HIS103", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="HIS104", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="MUS101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="PHI101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="ATH101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="ECO101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="POL101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="PSY101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="PSY102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC101", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC102", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC103", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="POL103", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC104", Taken = false, Status = "Not Taken"},
                        new Enrollment{StudentID = student.StudentID, CourseID="SOC105", Taken = false, Status = "Not Taken"},
                    };

                }

                //Check new student major
                if (student.Major == "IT")
                {
                    //Create a new enrollment List with some enrollment data for IT Major
                    var enrollments = new List<Enrollment> 
                    {
                         //Add the enrollments for an IT student
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE221", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE222", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE120", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="BUS206", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ENG223", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE201", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE231", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE235", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="LIB213", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="LIB214", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SCI111", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE220", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE254", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE321", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE337", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE420", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE442", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE475", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE479", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE299", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE499", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE240", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE340", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE342", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE343", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE351", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE353", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE390", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE443", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE445", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE448", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE449", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE451", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE452", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE480", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ITE485", Taken = false, Status = "Not Taken"},

                         //Add Gen Ed courses
                         new Enrollment{StudentID = student.StudentID, CourseID="ENG101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ENG102", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ENG103", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MAT102", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="STA101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SCI105", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MIS103", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ART101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="GEO101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="HIS101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="THA101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="HIS102", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="HIS103", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="HIS104", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MUS101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="PHI101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ATH101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="ECO101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="POL104", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="PSY101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="PSY102", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SOC101", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SOC102", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SOC103", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="POL103", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SOC104", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="SOC105", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="BUS299", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="LIB299", Taken = false, Status = "Not Taken"},

                    };

                    enrollments.ForEach(s => db.Enrollments.Add(s));
                    db.SaveChanges();
                }
      

                //Check student minor
                if (student.Minor == "CMD")
                {
                    var enrollments = new List<Enrollment>
                    {
                        //Add enrollments to cmd coursers
                         new Enrollment{StudentID = student.StudentID, CourseID="CMD201", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="CMD202", Taken = false, Status ="Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="CMD211", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="CMD221", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="CMD303", Taken = false, Status = "Not Taken" },
                    };
                    enrollments.ForEach(s => db.Enrollments.Add(s));
                    db.SaveChanges();
                }

                //Check student minor
                if (student.Minor == "MKT")
                {
                    var enrollments = new List<Enrollment>
                    {
                        //Add enrollments to marketing courses
                         new Enrollment{StudentID = student.StudentID, CourseID="MKT213", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MKT333", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MKT345", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MKT451", Taken = false, Status = "Not Taken"},
                         new Enrollment{StudentID = student.StudentID, CourseID="MKT470", Taken = false, Status = "Not Taken"},


                    };
                    enrollments.ForEach(s => db.Enrollments.Add(s));
                    db.SaveChanges();
                }


                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? StudentID)
        {
            if (StudentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(StudentID);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }



        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,FirstName,LastName,Major,Minor,Enrollments, Status")] Student student, Enrollment enrollment, string CourseID, int? StudentID, string Status)
        {
            
            if (ModelState.IsValid)
            {

                

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                UpdateEnrollment(CourseID, student.StudentID, Status);

                //var enrollmentsToUpdate = db.Enrollments.Where(i => i.StudentID == student.StudentID);

                //foreach (var item in enrollmentsToUpdate)
                //{
                //    if (item.StudentID == student.StudentID)
                //    {
                //        db.Entry(student).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}



                //var enrollmentUpdate = db.Enrollments.Include(i => i.Course.Enrollments).Where(i => i.StudentID == student.StudentID);

                //db.Entry(enrollmentUpdate).State = EntityState.Modified;
                //db.SaveChanges();


                //foreach(var item in enrollmentUpdate) {
                //    db.Entry(enrollment).State = EntityState.Modified;
                //    db.SaveChanges();
                //} 

                return RedirectToAction("Edit", "Student", new { StudentID = student.StudentID });
            }
            return View(student);
        }











        [HttpPost]
        public ActionResult UpdateEnrollment(string CourseID, int StudentID, string Status)
        {


            Student student = db.Students.Find(StudentID);

           if (student.Enrollments != null)
           {

                foreach (var item in student.Enrollments)
                {
                //var enrollments = new List<Enrollment>(db.Enrollments.Where(i => i.StudentID == student.StudentID));
                //var en2 = student.Enrollments.ToList();

                    if (item != null && item.CourseID == CourseID)
                    {
                    //ModelState.Remove("Status");
                    
                        item.Status = Status;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
           }

           return RedirectToAction("Edit", "Student", new { StudentID = StudentID });

           //RedirectToRoute("Student/Edit", new { StudentID = StudentID });
        }







        //public void UpdateEnrollments(string[] enrollmentlist, Student student) 
        //{
        //    var changedEnrollments = new HashSet<string>(enrollmentlist);
        //    var studentEnrollments = new HashSet<int>(student.Enrollments.Select(c => c.EnrollmentID));

        //    var studentToUpdate = student;


        //    foreach (var enrollment in db.Enrollments)
        //    {
        //        if (studentEnrollments.Contains(enrollment.EnrollmentID))
        //        {

        //        }
        //    }
        //}



        //public void UpdateEnrolledCourses(string[] enrollmentlist, Student studentToUpdate)
        //{
        //    var selectedCoursesHS = new HashSet<string>(enrollmentlist);
        //    var studentCourses = new HashSet<int>
        //        (studentToUpdate.Enrollments.Select(c => c.EnrollmentID));

        //    foreach (var enrollment in db.Enrollments)
        //    {
        //        if (selectedCoursesHS.Contains(enrollment.CourseID))
        //        {
                    
        //        }
        //    }

        //} 




        // GET: Student/Delete/5
        public ActionResult Delete(int? StudentID)
        {
            if (StudentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(StudentID);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int StudentID)
        {
            Student student = db.Students.Find(StudentID);
            db.Students.Remove(student);
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
