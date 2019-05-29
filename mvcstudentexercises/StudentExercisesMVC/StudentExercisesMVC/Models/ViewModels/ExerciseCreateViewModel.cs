using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class ExerciseCreateViewModel
    {
        public Exercise exercise { get; set; }

        protected string _connectionString;

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public ExerciseCreateViewModel() { }

        public ExerciseCreateViewModel(string connectionString)
        {
            _connectionString = connectionString;
       }
    }
}