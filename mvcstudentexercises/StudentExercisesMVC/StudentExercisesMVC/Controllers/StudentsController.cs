using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;

namespace StudentExercisesMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;

        public StudentsController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Students
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.firstName,
                s.lastName,
                s.slackHandle,
                s.cohortId
            FROM Student s
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };

                        students.Add(student);
                    }

                    reader.Close();

                    return View(students);
                }
            }
        }
        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT s.id as 'studentId', 
                                        s.firstName, s.lastName, s.slackHandle, 
                                        s.cohortId, c.cohortName, e.id AS 'exerciseId', 
                                        e.exerciseName, e.exerciseLanguage FROM studentExercise se 
                                        JOIN Exercise e on se.exerciseId = e.id 
                                        JOIN Student s on se.studentId = s.id 
                                        JOIN Cohort c on s.cohortId = c.id WHERE s.id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Student studentToDisplay = null;
                    int counter = 0;

                    while (reader.Read())
                    {
                        if (counter < 1)
                        {
                            Student student = new Student
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("studentId")),
                                firstName = reader.GetString(reader.GetOrdinal("firstName")),
                                lastName = reader.GetString(reader.GetOrdinal("lastName")),
                                slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                                cohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                currentCohort = new Cohort()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                    cohortName = reader.GetString(reader.GetOrdinal("cohortName"))
                                }
                            };
                            studentToDisplay = student;
                            counter++;
                        };
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("exerciseId")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))
                        };

                        if (!studentToDisplay.exerciseList.Any(e => e.Id == exercise.Id))
                        {
                            studentToDisplay.exerciseList.Add(exercise);
                        }
                    }
                    reader.Close();
                    return View(studentToDisplay);
                }
            }
        }
        // GET: Students/Create
        public ActionResult Create()
        {
            StudentCreateViewModel studentViewModel = new StudentCreateViewModel(_config.GetConnectionString("DefaultConnection"));
            return View(studentViewModel);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentCreateViewModel model)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Student
                ( firstName, lastName, slackHandle, cohortId )
                VALUES
                ( @firstName, @lastName, @slackHandle, @cohortId )";
                    cmd.Parameters.Add(new SqlParameter("@firstName", model.student.firstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", model.student.lastName));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", model.student.slackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", model.student.cohortId));
                    cmd.ExecuteNonQuery();

                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // GET: Students/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id, StudentEditViewModel model)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id as 'studentId', s.firstName, s.lastName, 
                                        s.slackHandle, s.cohortId, c.cohortName
                                        FROM student s JOIN cohort c on s.cohortId = c.id 
                                        WHERE s.id=@id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    Student thisStudent = null;
                    if (reader.Read())
                    {
                        thisStudent = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("studentId")),
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };
                    }

                    StudentEditViewModel viewModel = new StudentEditViewModel(_config.GetConnectionString("DefaultConnection"), id);

                    viewModel.Student = thisStudent;

                    return View(viewModel);
                }
            }
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student Student, Exercise Exercise)
        {
           
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO StudentExercise
                                            (exerciseId, studentId) 
                                            VALUES(@exerciseId, @studentId)";
                        cmd.Parameters.Add(new SqlParameter("@studentId", id));
                        cmd.Parameters.Add(new SqlParameter("@exerciseId", Exercise.Id));
                        StudentExercise studentExercise = new StudentExercise();
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Student
                                            SET firstName = @firstName,
                                                lastName = @lastName,
                                                slackHandle = @slackHandle,
                                                cohortId = @cohortId
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@firstName", Student.firstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", Student.lastName));
                        cmd.Parameters.Add(new SqlParameter("@slackHandle", Student.slackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohortId", Student.cohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        Student = new Student();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        throw new Exception("No rows affected");
                    }
                }
            
        }



        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.firstName,
                s.lastName,
                s.slackHandle,
                s.cohortId
            FROM Student s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Student student = null;
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId"))
                        };
                    }

                    reader.Close();

                    return View(student);
                }
            }
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM StudentExercise WHERE studentId = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();

                }
            }
                using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Student WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    throw new Exception("No rows affected");
                }
            }
        }
    
         private bool StudentExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, firstName, lastName, slackHandle, cohortId
                        FROM Student
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}