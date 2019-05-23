using System;
using System.Collections.Generic;
namespace studentexercises{
    class Instructor{
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string slackHandle {get; set;}
        public Cohort currentCohort { get; set; }

     public void giveExercise(Student studentParam, Exercise exerciseParam){
         studentParam.ExerciseList.Add(exerciseParam);
        }
    }
}
