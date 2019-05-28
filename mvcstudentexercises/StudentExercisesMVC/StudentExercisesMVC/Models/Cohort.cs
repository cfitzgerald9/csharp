using System;
using System.Collections.Generic;
namespace StudentExercisesMVC.Models
{
    public class Cohort
    {
        public int Id { get; set; }
        public string cohortName { get; set; }
        public List<Student> StudentList = new List<Student>();
        public List<Instructor> InstructorList = new List<Instructor>();

    }
}
