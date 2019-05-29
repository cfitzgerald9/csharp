using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StudentExerciseMVC.Models.ViewModels
{
    public class InstructorEditViewModel
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
        public Instructor Instructor { get; set; }

        public InstructorEditViewModel() { }

        public InstructorEditViewModel(string connectionString)
        {
                _connectionString = connectionString;

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