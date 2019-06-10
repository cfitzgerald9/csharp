using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentExercises_Entity.Models.ViewModels
{
    public class CohortStudentInstructorViewModel
    {
        public Cohort cohort { get; set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }

        private string _connectionString;

      
    }
}
