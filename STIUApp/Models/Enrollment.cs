using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STIUApp.Models
{
    public class Enrollment
    {
        //Primary key Enrollment ID
        public int EnrollmentID { get; set; }

        //Foreign Key Course ID
        public String CourseID { get; set; }

        //Foreign Key Student ID
        public int StudentID { get; set; }


        public bool Taken { get; set; }
        public string Status { get; set; }



        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}