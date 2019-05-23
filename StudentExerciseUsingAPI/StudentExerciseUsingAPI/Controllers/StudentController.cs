using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using StudentExerciseUsingAPI.Models;
using Microsoft.AspNetCore.Http;

namespace StudentExerciseUsingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
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

        [HttpGet]
        public async Task<IActionResult> Get(string include, string q)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string command = "";
                    if (include == "exercise")
                    {
                        command = @"SELECT s.Id AS 'Student Id', s.firstName, s.lastName, s.slackHandle, s.cohortId, 
                                            c.Id AS 'Cohort Id', c.cohortName AS 'Cohort Name', e.Id AS 'Exercise Id',
                                            e.exerciseName, e.exerciseLanguage 
                                            FROM studentExercise se 
                                            JOIN Exercise e on se.exerciseId=e.Id 
                                            JOIN Student s on se.studentId=s.Id 
                                            JOIN Cohort c on s.cohortId = c.Id;";
                    }
                    else
                    {
                        command = @"SELECT s.Id AS 'Student Id', s.firstName, s.lastName, 
                        s.slackHandle, s.cohortId,
                        c.cohortName AS 'Cohort Name',
                        c.Id AS 'Cohort Id'
                        FROM Student s 
                        JOIN Cohort c ON s.cohortId = c.Id"; 
                    }
                    if (q != null)
                    {
                        command += $" WHERE s.firstName LIKE '{q}' OR s.lastName LIKE '{q}' OR s.slackHandle LIKE '{q}'";
                    }
                    cmd.CommandText = command;
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        Student currentStudent = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Student Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                            currentCohort = new Cohort()
                            {

                                Id = reader.GetInt32(reader.GetOrdinal("Cohort Id")),
                                cohortName = reader.GetString(reader.GetOrdinal("Cohort Name")),

                            }
                        };

                        if (include == "exercise")

                        {
                            Exercise currentExercise = new Exercise
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Exercise Id")),
                                exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                                exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))

                            };


              
                            if (students.Any(s => s.Id == currentStudent.Id))
                            {
                                Student thisStudent = students.Where(s => s.Id == currentStudent.Id).FirstOrDefault();
                                thisStudent.exerciseList.Add(currentExercise);
                            }
                            else
                            {
                                currentStudent.exerciseList.Add(currentExercise);
                                students.Add(currentStudent);

                            }

                        }
                        else
                        {
                            students.Add(currentStudent);
                        }

                    }
                    reader.Close();

                    return Ok(students);
                }
            }
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, firstName, lastName, slackHandle, cohortId FROM Student
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
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                        };
                    
                    }
                    reader.Close();

                    return Ok(student);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Student (firstName, lastName, slackHandle, cohortId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@firstName, @lastName, @slackHandle, @cohortId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", student.firstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", student.lastName));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", student.slackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", student.cohortId));

                    int newId = (int)cmd.ExecuteScalar();
                    student.Id = newId;
                    return CreatedAtRoute("GetStudent", new { id = newId }, student);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Student student)
        {
            try
            {
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
                        cmd.Parameters.Add(new SqlParameter("@firstName", student.firstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", student.lastName));
                        cmd.Parameters.Add(new SqlParameter("@slackHandle", student.slackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohortId", student.cohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", student.Id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
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
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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
