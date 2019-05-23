using System;
using System.Collections.Generic;
namespace StudentSQL.Models
{
    class Cohort
    {
        public string cohortName { get; set; }
        public List<Student> StudentList = new List<Student>();
        public List<Instructor> InstructorList = new List<Instructor>();

    }
}