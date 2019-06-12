using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_Entity.Models.ViewModels
{
    public class StudentEditViewModel
    {
        public Student student {get; set;}
        public List<SelectListItem> CohortOptions { get; set; }

        public List<SelectListItem> ExerciseOptions { get; set; }
    }
}
