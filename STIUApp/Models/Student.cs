using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STIUApp.Models
{
    public class Student
    {
        
        //public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; 


        //Foreign key: enrollment (every student can have many enrollments)
        public virtual ICollection<Enrollment> Enrollments { get; set; }


    }
}