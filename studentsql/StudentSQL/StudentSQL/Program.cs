using System;
using System.Collections.Generic;
using StudentSQL.Data;
using StudentSQL.Models;

namespace StudentSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            List<Exercise> exercises= repository.GetAllExercises();
           
            PrintAllExercises("All Exercises", exercises);

            Pause();
            exercises = repository.GetAllExercisesByLanguage("Javascript");
            PrintAllExercises($"Exercises using Javascript", exercises);


        }

        public static void PrintAllExercises(string title, List<Exercise> exercises)
        {
            Console.WriteLine($"{title}");
            foreach (Exercise singleExercise in exercises)
            {
                Console.WriteLine($"{singleExercise.exerciseName} : {singleExercise.exerciseLanguage}");

            }
        }
        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
