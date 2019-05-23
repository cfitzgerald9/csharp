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
    public class ExerciseController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ExerciseController(IConfiguration config)
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
                   

                    if (include == "student")
                    {
               
                        command = $@"SELECT e.Id AS 'Exercise Id', e.exerciseName, e.exerciseLanguage, s.Id AS 'Student Id', 
                        s.firstName AS 'Student First Name', 
                        s.lastName AS 'Student Last Name',
                        s.slackHandle AS 'Slack Handle'
                        FROM Exercise e JOIN StudentExercise se ON e.Id = se.exerciseId 
                        JOIN Student s ON se.studentId=s.Id";

                    }
                    else
                    {
                        command = $"SELECT e.Id AS 'Exercise Id', e.exerciseName, e.exerciseLanguage FROM Exercise e"; 
                    }
                    if (q != null)
                    {
                        command += $" WHERE e.exerciseName LIKE '{q}' OR e.exerciseLanguage LIKE '{q}'";
                    }

                    cmd.CommandText = command;
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();

                        while (reader.Read())
                    {

                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Exercise Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))

                        };

                        if (include == "student")
                        {
                            Student currentStudent = new Student
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Student Id")),
                                firstName = reader.GetString(reader.GetOrdinal("Student First Name")),
                                lastName = reader.GetString(reader.GetOrdinal("Student Last Name")),
                                slackHandle = reader.GetString(reader.GetOrdinal("Slack Handle"))
                            };

                            if (exercises.Any(e => e.Id == exercise.Id))
                            {
                                Exercise thisExercise = exercises.Where(e => e.Id == exercise.Id).FirstOrDefault();
                                thisExercise.studentList.Add(currentStudent);
                            }
                            else
                            {
                                exercise.studentList.Add(currentStudent);
                                exercises.Add(exercise);

                            }

                        }
                        else
                        {
                            exercises.Add(exercise);

                        }

                    }
                    reader.Close();

                        return Ok(exercises);
                    }
                }
            }
        

        [HttpGet("{id}", Name = "GetExercise")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            Id, exerciseName, exerciseLanguage
                        FROM Exercise
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;

                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))
                        };
                    }
                    reader.Close();

                    return Ok(exercise);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Exercise (exerciseName, exerciseLanguage)
                                        OUTPUT INSERTED.Id
                                        VALUES (@exerciseName, @exerciseLanguage)";
                    cmd.Parameters.Add(new SqlParameter("@exerciseName", exercise.exerciseName));
                    cmd.Parameters.Add(new SqlParameter("@exerciseLanguage", exercise.exerciseLanguage));

                    int newId = (int)cmd.ExecuteScalar();
                    exercise.Id = newId;
                    return CreatedAtRoute("GetExercise", new { id = newId }, exercise);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Exercise exercise)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Exercise
                                            SET exerciseName = @exerciseName,
                                                exerciseLanguage = @exerciseLanguage
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@exerciseName", exercise.exerciseName));
                        cmd.Parameters.Add(new SqlParameter("@exerciseLanguage", exercise.exerciseLanguage));
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
                if (!ExerciseExists(id))
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
                        cmd.CommandText = @"DELETE FROM Exercise WHERE id = @id";
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
                if (!ExerciseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ExerciseExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, exerciseName, exerciseLanguage
                        FROM Exercise
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}

