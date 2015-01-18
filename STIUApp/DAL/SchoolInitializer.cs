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
                new Course{CourseID="ITE221", Title="Programming 1", Credits= 3, Faculty = "IT"},
                new Course{CourseID="ITE222", Title="Programming 2", Credits= 3, Prereq = "ITE221", Faculty = "IT"},
                new Course{CourseID="ITE223", Title="Programming 3", Credits= 3, Prereq = "ITE223", Faculty = "IT"},
                new Course{CourseID="ITE101", Title="Introduction to Information Systems", Credits= 3, Faculty = "IT"},
                new Course{CourseID="ITE120", Title="Website Construction and Management", Credits= 3, Faculty = "IT"},
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            //Create a new enrollment List with some enrollment data
            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID = 212210006, CourseID="ITE221", Taken = true},
                new Enrollment{StudentID = 212210006, CourseID="ITE222", Taken = true},
                new Enrollment{StudentID = 212210006, CourseID="ITE223", Taken = false},
                new Enrollment{StudentID = 212210006, CourseID="ITE101", Taken = true},
                new Enrollment{StudentID = 212210006, CourseID="ITE120", Taken = false},

                new Enrollment{StudentID = 212210003, CourseID="ITE221", Taken = true},
                new Enrollment{StudentID = 212210003, CourseID="ITE222", Taken = true},
                new Enrollment{StudentID = 212210003, CourseID="ITE223", Taken = false},
                new Enrollment{StudentID = 212210003, CourseID="ITE101", Taken = true},
                new Enrollment{StudentID = 212210003, CourseID="ITE120", Taken = true},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();





        }
        
    }
}