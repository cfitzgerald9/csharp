using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class StudentEditViewModel
    {
        protected string _connectionString;

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public List<SelectListItem> Cohorts { get; set; }

        public List<SelectListItem> Exercises { get; set; }

        public List<StudentExercise> assignedExercises { get; set; }
        public List<int> exerciseIds { get; set; }
        public Student Student { get; set; }


        public StudentEditViewModel() { }

        public StudentEditViewModel(string connectionString, int id)
        {
            _connectionString = connectionString;

            Exercises = GetAllExercises()
                .Select(exercise => new SelectListItem()
            {
                Text = exercise.exerciseName,
                Value = exercise.Id.ToString()

            })
            .ToList();
            assignedExercises = GetStudentExercisesForThisStudent(id);

            Exercises = GetAllExercises()
               .Select(exercise => new SelectListItem
               {
                   Text = exercise.exerciseName,
                   Value = exercise.Id.ToString(),
                   Selected = assignedExercises.Any(e => e.exerciseId == exercise.Id) ? true : false
               })
               .ToList();

        

        Cohorts = GetAllCohorts()
                .Select(cohort => new SelectListItem()
                {
                    Text = cohort.cohortName,
                    Value = cohort.Id.ToString()
                })
                .ToList();

            Cohorts.Insert(0, new SelectListItem
            {
                Text = "Choose a cohort",
                Value = "0"
            });

            Exercises.Insert(0, new SelectListItem
            {
                Text = "Assign an exercise",
                Value = "0"
            });

        }

        protected List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, exerciseName, exerciseLanguage FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage")),
                        });
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        private List<StudentExercise> GetStudentExercisesForThisStudent(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT Id, studentId, exerciseId " +
                                      $"FROM StudentExercise WHERE StudentId = {id}";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<StudentExercise> assignedExercises = new List<StudentExercise>();
                    while (reader.Read())
                    {
                        assignedExercises.Add(new StudentExercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            studentId = reader.GetInt32(reader.GetOrdinal("studentId")),
                            exerciseId = reader.GetInt32(reader.GetOrdinal("exerciseId")),

                        });
                    }

                    reader.Close();

                    return assignedExercises;
                }
            }
        }



        protected List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, cohortName FROM Cohort";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        cohorts.Add(new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            cohortName = reader.GetString(reader.GetOrdinal("cohortName")),
                        });
                    }

                    reader.Close();

                    return cohorts;
                }
            }
        }
    }
}