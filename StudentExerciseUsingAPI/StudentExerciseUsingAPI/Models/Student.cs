using System;
using System.Collections.Generic;
namespace StudentExerciseUsingAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string slackHandle { get; set; }

        public int cohortId { get; set; }
        public Cohort currentCohort { get; set; }
        public List<Exercise> exerciseList = new List<Exercise>();
    }

}