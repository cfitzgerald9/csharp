using System;
using System.Collections.Generic;
namespace studentexercises{
    class Student{
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string slackHandle {get; set;}
        public Cohort currentCohort { get; set; }
        public List<Exercise> ExerciseList = new List<Exercise>();
    }

}