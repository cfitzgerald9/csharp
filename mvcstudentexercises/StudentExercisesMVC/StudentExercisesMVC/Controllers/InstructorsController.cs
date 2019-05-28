using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace InstructorExercisesMVC.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IConfiguration _config;

        public InstructorsController(IConfiguration config)
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
        // GET: Instructors
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
            FROM Instructor s
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> Instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        Instructor Instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };

                        Instructors.Add(Instructor);
                    }

                    reader.Close();

                    return View(Instructors);
                }
            }
        }

        // GET: Instructors/Details/5

        public ActionResult Details(int id)
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
            FROM Instructor s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Instructor Instructor = null;
                    if (reader.Read())
                    {
                        Instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };
                    }

                    reader.Close();

                    return View(Instructor);
                }
            }
        }


        // GET: Instructors/Create
        public ActionResult Create()
        { return View(); }

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int id)
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
            FROM Instructor s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Instructor Instructor = null;
                    if (reader.Read())
                    {
                        Instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId"))
                        };
                    }

                    reader.Close();

                    return View(Instructor);
                }
            }
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Instructor Instructor)
        {

            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Instructor
                                            SET firstName = @firstName,
                                                lastName = @lastName,
                                                slackHandle = @slackHandle,
                                                cohortId = @cohortId
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@firstName", Instructor.firstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", Instructor.lastName));
                        cmd.Parameters.Add(new SqlParameter("@slackHandle", Instructor.slackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohortId", Instructor.cohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        Instructor = new Instructor();

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch (Exception)
            {
                if (!InstructorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // GET: Instructors/Delete/5
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
            FROM Instructor s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Instructor Instructor = null;
                    if (reader.Read())
                    {
                        Instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("slackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohortId"))
                        };
                    }

                    reader.Close();

                    return View(Instructor);
                }
            }
        }

        // POST: Instructors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Instructor WHERE id = @id";
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

        private bool InstructorExists(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, firstName, lastName, slackHandle, cohortId
                        FROM Instructor
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
        }
    }
}