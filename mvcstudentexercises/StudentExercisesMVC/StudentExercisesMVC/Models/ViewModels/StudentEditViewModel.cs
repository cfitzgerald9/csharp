using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Controllers;
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

        public List<SelectListItem> allExercises { get; set; } = new List<SelectListItem>();
        public List<int> selectedExercises { get; set; }
        public Student student { get; set; }


        public StudentEditViewModel() { }

        public StudentEditViewModel(string connectionString, int id)
        {
            _connectionString = connectionString;
            allExercises = GetAllExercises().Select(exercise => new SelectListItem()
            {
                Text = exercise.exerciseName,
                Value = exercise.Id.ToString()
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

            allExercises.Insert(0, new SelectListItem
            {
                Text = "Assign an exercise",
                Value = "0"
            });

        }
      public List<Exercise> GetAllExercises()
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
        public Student GetOneStudent(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            s.Id AS 'Student Id', s.firstName, s.lastName, s.slackHandle, s.cohortId,
                            c.cohortName
                        FROM Student s
                        JOIN Cohort c ON s.cohortId = c.Id
                        WHERE s.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Student Student = null;

                    if (reader.Read())
                    {
                        Student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Student Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                            currentCohort = new Cohort
                            {
                                cohortName = reader.GetString(reader.GetOrdinal("cohortName"))
                            }
                        };
                    }
                    reader.Close();

                    return Student;
                }
            }
        }

        public Student GetOneStudentWithExercises(int id)
        {
                Student student = GetOneStudent(id);   
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT e.Id, e.exerciseName, e.exerciseLanguage FROM Exercise e JOIN StudentExercise se ON e.Id=se.exerciseId WHERE se.studentId=@id";

                        cmd.Parameters.Add(new SqlParameter("@id", student.Id));
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            student.exerciseList.Add(new Exercise
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                exerciseName = reader.GetString(reader.GetOrdinal("Name")),
                                exerciseLanguage = reader.GetString(reader.GetOrdinal("Language")),
                            });
                        }
                        reader.Close();
                    return student;
                }
                
            }
     }
        



        public List<Cohort> GetAllCohorts()
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