﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

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
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            slackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };
                    }

                    reader.Close();

                    return View(student);
                }
            }
        }


        // GET: Students/Create
        public ActionResult Create()
        { return View(); }

        // POST: Students/Create
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

        // GET: Students/Edit/5
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

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
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
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        student = new Student();

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