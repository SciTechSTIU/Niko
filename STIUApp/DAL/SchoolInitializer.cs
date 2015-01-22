using STIUApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STIUApp.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        //Seed function to create test data
        protected override void Seed(SchoolContext context)
        {
            //Create a new student List with some example students
            var students = new List<Student>
            {
                new Student{
                    StudentID = 212210006,
                    FirstName = "Niko",
                    LastName = "Kallio",
                    Major = "IT"
                },
                new Student{
                    StudentID = 212210003,
                    FirstName = "Maaya",
                    LastName = "Matsumoto",
                    Major = "IT"
                }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();


            //Create a new course List with some example courses
            var courses = new List<Course>
            {
                new Course{CourseID="ITE221", Title="Programming 1", Credits= 3, Faculty = "IT", Type = "Basic Core"},
                new Course{CourseID="ITE222", Title="Programming 2", Credits= 3, Prereq = "ITE221", Faculty = "IT", Type ="Major Requisite"},
                new Course{CourseID="ITE101", Title="Introduction to Information Technology", Credits= 3, Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="ITE120", Title="Web Development 1", Credits= 3, Faculty="IT" ,Type = "Basic Core"},
                new Course{CourseID="BUS206", Title="Principles of Management", Credits= 3, Faculty="IT",Type = "Basic Core"},
                new Course{CourseID="ENG223", Title="Presentation Skills", Credits= 3, Faculty="IT" ,Type = "Basic Core"},
                new Course{CourseID="ITE201", Title="IT Service Desk & Incident Management", Credits= 3, Prereq="ITE101" ,Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="ITE231", Title="Fundamentals Of System Administration", Credits= 3, Prereq="ITE221" ,Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="ITE235", Title="Mobile Applicaion Architecture", Credits= 3, Prereq="ITE101", Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="LIB213", Title="Critical Thinking", Credits= 3, Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="LIB214", Title="Creative Thinking", Credits= 3, Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="SCI111", Title="Physic for Everyday life", Credits= 3, Faculty = "IT", Type="Basic Core"},
                new Course{CourseID="ITE220", Title="Web Development 2", Credits= 3, Prereq="ITE120" ,Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE254", Title="Mobile UX", Credits= 3, Prereq="ITE221,ITE235", Faculty = "IT", Type="Major Requisite"}, 
                new Course{CourseID="ITE321", Title="System Analysis,Design & Implementation ", Credits= 3, Prereq="ITE222", Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE331", Title="Multimedia Technology", Credits= 3, Prereq ="ITE220", Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE337", Title="Content Management Systems", Credits= 3, Prereq="ITE321" ,Faculty = "IT", Type="Major Requisite"},            
                new Course{CourseID="ITE420", Title="Information Security", Credits= 3, Prereq="ITE475" ,Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE442", Title="Database Management System", Credits= 3, Prereq="ITE321" ,Faculty = "IT", Type="Major Requisite"},    
                new Course{CourseID="ITE475", Title="Data Communication and Networking", Credits= 3, Prereq="ITE231" ,Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE479", Title="IT Planning and Project Management", Credits= 3, Prereq="120 Cr+" ,Faculty = "IT", Type="Major Requisite"},
                new Course{CourseID="ITE299", Title="Service Desk Internship", Credits= 3, Prereq="ITE201, 45cr" ,Faculty = "IT", Type="Internship"},
                new Course{CourseID="ITE499", Title="Professional Internship", Credits= 3, Prereq="Course Complete" ,Faculty = "IT", Type="Internship"},
                new Course{CourseID="ITE240", Title="Principle of Operating System", Credits= 3, Prereq="ITE231" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE340", Title="E-commerce System & Infrastructure", Credits= 3, Prereq="BUS206" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE342", Title="Mobile Apps Development 1", Credits= 3, Prereq="ITE222" ,Faculty = "IT", Type="Major Electives"},
                new Course{CourseID="ITE343", Title="Mobile Apps Development 2", Credits= 3, Prereq="ITE222" ,Faculty = "IT", Type="Major Electives"},
                new Course{CourseID="ITE351", Title="IT Capacity Planning", Credits= 3, Prereq="231" ,Faculty = "IT",Type="Major Electives"},
                new Course{CourseID="ITE353", Title="Mobile Commerce & Computing", Credits= 3, Prereq="ITE235,ITE475" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE390", Title="Business Intelligence Application", Credits= 3, Prereq="STA101, BUS206" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE443", Title="Data Warehouse Systems & Data Mining", Credits= 3, Prereq="ITE442" ,Faculty = "IT", Type="Elective"},
                new Course{CourseID="ITE445", Title="Mobile Games Development", Credits= 3, Prereq="ITE220" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE448", Title="Current Topics in Mobile Computing", Credits= 3, Prereq="ITE235" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE449", Title="Current Topics in Infrastrucuture Management", Credits= 3, Prereq="ITE231" ,Faculty = "IT", Type="Major Elective"},            
                new Course{CourseID="ITE451", Title="Cloud Computing", Credits= 3, Prereq="ITE442, ITE475" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE452", Title="Big Data", Credits= 3, Prereq="ITE442, ITE475" ,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE480", Title="Special Project", Credits= 3,Faculty = "IT", Type="Major Elective"},
                new Course{CourseID="ITE485", Title="System and Network Administration", Credits= 3, Prereq="ITE475" ,Faculty = "IT", Type="Major Elective"},
                            
            
            
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            //Create a new enrollment List with some enrollment data
            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID = 212210006, CourseID="ITE221", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210006, CourseID="ITE222", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210006, CourseID="ITE223", Taken = false, Status = "Not Taken"},
                new Enrollment{StudentID = 212210006, CourseID="ITE101", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210006, CourseID="ITE120", Taken = false, Status = "Enrolled"},

                new Enrollment{StudentID = 212210003, CourseID="ITE221", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210003, CourseID="ITE222", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210003, CourseID="ITE223", Taken = false, Status = "Enrolled"},
                new Enrollment{StudentID = 212210003, CourseID="ITE101", Taken = true, Status = "Completed"},
                new Enrollment{StudentID = 212210003, CourseID="ITE120", Taken = true, Status = "Not Taken"},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();





        }
        
    }
}