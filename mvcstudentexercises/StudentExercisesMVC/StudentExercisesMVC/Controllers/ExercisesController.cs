using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace ExerciseExercisesMVC.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IConfiguration _config;

        public ExercisesController(IConfiguration config)
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
        // GET: Exercises
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.exerciseName,
                s.exerciseLanguage
            FROM Exercise s
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> Exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise Exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage")),

                        };

                        Exercises.Add(Exercise);
                    }

                    reader.Close();

                    return View(Exercises);
                }
            }
        }

        // GET: Exercises/Details/5

        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.exerciseName,
                s.exerciseLanguage
            FROM Exercise s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Exercise Exercise = null;
                    if (reader.Read())
                    {
                        Exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))
                        };
                    }

                    reader.Close();

                    return View(Exercise);
                }
            }
        }


        // GET: Exercises/Create
        public ActionResult Create()
        { return View(); }

        // POST: Exercises/Create
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

        // GET: Exercises/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.exerciseName,
                s.exerciseLanguage
            FROM Exercise s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Exercise Exercise = null;
                    if (reader.Read())
                    {
                        Exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage")),

                        };
                    }

                    reader.Close();

                    return View(Exercise);
                }
            }
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Exercise Exercise)
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
                        cmd.Parameters.Add(new SqlParameter("@ExerciseName", Exercise.exerciseName));
                        cmd.Parameters.Add(new SqlParameter("@ExerciseLanguage", Exercise.exerciseLanguage));

                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        Exercise = new Exercise();

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

        // GET: Exercises/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT s.Id,
                s.exerciseName,
                s.exerciseLanguage
            FROM Exercise s 
            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    Exercise Exercise = null;
                    if (reader.Read())
                    {
                        Exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage"))



                        };
                    }

                    reader.Close();

                    return View(Exercise);
                }
            }
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
                        return RedirectToAction(nameof(Index));
                    }
                    throw new Exception("No rows affected");
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