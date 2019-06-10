using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercises_Entity.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Language { get; set; }
        public List<StudentExercise> StudentExercises { get; set; } = new List<StudentExercise>();
    }
}
