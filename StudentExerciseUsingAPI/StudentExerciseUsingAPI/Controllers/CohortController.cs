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
    public class CohortController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CohortController(IConfiguration config)
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
        public async Task<IActionResult> Get(string q)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string command = @"SELECT c.Id AS 'Cohort Id',
                    c.cohortName AS 'Cohort Name',
                    s.Id AS 'Student Id',
                    s.firstName AS 'Student First Name',
                    s.lastName AS 'Student Last Name',
                    s.slackHandle AS 'Student Slack Handle',
                    i.Id AS 'Instructor Id',
                    i.firstName AS 'Instructor First Name',
                    i.LastName AS 'Instructor Last Name',
                    i.slackHandle AS 'Instructor Slack Handle'
                    FROM Cohort c 
                    JOIN Instructor i ON c.Id = i.cohortId
                    JOIN Student s ON c.Id = s.cohortId";
                    if (q != null)
                    {
                        command += $" WHERE c.cohortName LIKE '{q}'";
                    }
                    cmd.CommandText = command;
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> Cohorts = new List<Cohort>();

                    while (reader.Read())
                    {
                        Cohort currentCohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Cohort Id")),
                            cohortName = reader.GetString(reader.GetOrdinal("Cohort Name"))
                        };
                        Student currentStudent = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Student Id")),
                            firstName = reader.GetString(reader.GetOrdinal("Student First Name")),
                            lastName = reader.GetString(reader.GetOrdinal("Student Last Name")),
                            slackHandle = reader.GetString(reader.GetOrdinal("Student Slack Handle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("Cohort Id"))
                        };

                        Instructor currentInstructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Instructor Id")),
                            firstName = reader.GetString(reader.GetOrdinal("Instructor First Name")),
                            lastName = reader.GetString(reader.GetOrdinal("Instructor Last Name")),
                            slackHandle = reader.GetString(reader.GetOrdinal("Instructor Slack Handle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("Cohort Id"))

                        };
                        if (Cohorts.Any(c => c.Id == currentCohort.Id))
                        {
                            
                            Cohort cohortToReference = Cohorts.Where(c => c.Id == currentCohort.Id).FirstOrDefault();

                            if (!cohortToReference.StudentList.Any(s => s.Id == currentStudent.Id))
                            {
                                cohortToReference.StudentList.Add(currentStudent);
                            }

                            if (!cohortToReference.InstructorList.Any(i => i.Id == currentInstructor.Id))
                            {
                                cohortToReference.InstructorList.Add(currentInstructor);
                            }
                        }
                        else
                        {
                            
                            currentCohort.StudentList.Add(currentStudent);
                            currentCohort.InstructorList.Add(currentInstructor);
                            Cohorts.Add(currentCohort);
                        }

                    }
                    return Ok(Cohorts);
                }
            }
        }

        [HttpGet("{id}", Name = "GetCohort")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, cohortName FROM Cohort 
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Cohort cohort = null;

                    if (reader.Read())
                    {
                        cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            cohortName = reader.GetString(reader.GetOrdinal("cohortName"))                    
                        };
                    }
                    reader.Close();

                    return Ok(cohort);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cohort cohort)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Cohort (cohortName)
                                        OUTPUT INSERTED.Id
                                        VALUES (@cohortName)";
                    cmd.Parameters.Add(new SqlParameter("@cohortName", cohort.cohortName));

                    int newId = (int)cmd.ExecuteScalar();
                    cohort.Id = newId;
                    return CreatedAtRoute("GetCohort", new { id = newId }, cohort);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Cohort cohort)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Cohort
                                            SET cohortName = @cohortName
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@cohortName", cohort.cohortName));
                        cmd.Parameters.Add(new SqlParameter("@id", cohort.Id));


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
                if (!CohortExists(id))
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
                        cmd.CommandText = @"DELETE FROM Cohort WHERE id = @id";
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
                if (!CohortExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CohortExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, cohortName
                        FROM Cohort
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}
