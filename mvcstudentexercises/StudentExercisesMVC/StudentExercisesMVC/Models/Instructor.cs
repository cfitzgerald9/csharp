using System;
using System.Collections.Generic;
namespace StudentExercisesMVC.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string slackHandle { get; set; }
        public int cohortId { get; set; }
        public Cohort currentCohort { get; set; }

        public void giveExercise(Student studentParam, Exercise exerciseParam)
        {
            studentParam.exerciseList.Add(exerciseParam);
        }
    }
}
