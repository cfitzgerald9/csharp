using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using StudentSQL.Models;

namespace StudentSQL.Data
{
    class Repository{
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Server = localhost\\SQLEXPRESS; Database = StudentExercise; Trusted_Connection = True";
                return new SqlConnection(_connectionString);
            }
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
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);
                        int exerciseNameColumnPosition = reader.GetOrdinal("exerciseName");
                        string exerciseNameValue = reader.GetString(exerciseNameColumnPosition);
                        int exerciseLanguageColumnPosition = reader.GetOrdinal("exerciseLanguage");
                        string exerciseLanguageValue = reader.GetString(exerciseLanguageColumnPosition);

                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            exerciseName = exerciseNameValue,
                            exerciseLanguage = exerciseLanguageValue
                        };

                        exercises.Add(exercise);
                    }
                    reader.Close();
                    return exercises;
                }
            }
        }

        public List<Exercise> GetAllExercisesByLanguage(string exerciseLanguage)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT e.Id, e.exerciseName,
                                           FROM Exercise e 
                                          WHERE e.exerciseLanguage = @exerciseLanguage";
                    cmd.Parameters.Add(new SqlParameter("@exerciseLanguage", exerciseLanguage));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            exerciseName = reader.GetString(reader.GetOrdinal("exerciseName")),
                            exerciseLanguage = reader.GetString(reader.GetOrdinal("exerciseLanguage")),
                           
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }


    }
}
