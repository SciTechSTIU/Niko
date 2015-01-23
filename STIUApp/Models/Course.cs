using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STIUApp.Models
{
    public class Course
    {
        //Let the user set the primary key instead of generating automatically
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CourseID { get; set; }

        public string Prereq { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Faculty { get; set; }
        //Course type: BC, MJ etc.
        public string Type { get; set; }

        //Number of unenrolled/enrolled/not taken
        public int Completed { get; set; }
        public int Enrolled { get; set; }
        public int NotTaken { get; set; }


        //Course entity can be related to any number of Enrollment entities
        public virtual ICollection<Enrollment> Enrollments { get; set; }



    }
}