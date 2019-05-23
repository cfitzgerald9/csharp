using System;
using System.Collections.Generic;
using System.Linq;

namespace studentexercises
{
    class Program
    {
        static void Main(string[] args)
        {

            Exercise Planets = new Exercise();
            Planets.name= "Planets and probes";
            Planets.language="C#";

            Exercise Nutshell = new Exercise();
            Nutshell.name= "Nutshell";
            Nutshell.language="Javascript";

            Exercise WonderTwins = new Exercise();
            WonderTwins.name= "Wonder twins/magic mirror";
            WonderTwins.language="Javascript";

            Exercise Python101 = new Exercise();
            Python101.name= "Python101";
            Python101.language="Python";

            Cohort One = new Cohort();
           One.name="Cohort 1";


           Cohort Two = new Cohort();
           Two.name="Cohort 2";

           Cohort Three = new Cohort();
           Three.name="Cohort 3";

            Student Connor = new Student();
            Connor.firstName="Connor";
            Connor.lastName="FitzGerald";
            Connor.slackHandle="Connor.9";
            Connor.currentCohort=Two;
            Two.StudentList.Add(Connor);

             Student Sable = new Student();
            Sable.firstName="Sable";
            Sable.lastName="Bowen";
            Sable.slackHandle="Sable.7";
            Sable.currentCohort=One;
            One.StudentList.Add(Sable);

             Student Sydney = new Student();
            Sydney.firstName="Sydney";
            Sydney.lastName="Waits";
            Sydney.slackHandle="Sydney.8";
            Sydney.currentCohort=Two;
            Two.StudentList.Add(Sydney);

            Student Bobby = new Student();
            Bobby.firstName="Bobby";
            Bobby.lastName="Fitzpatrick";
            Bobby.slackHandle="BobbyFp";
            Bobby.currentCohort=Three;
            Three.StudentList.Add(Bobby);


            Instructor Josh = new Instructor();
                Josh.firstName="Josh";
                Josh.lastName="Joseph";
                Josh.slackHandle="ColonelJosh";
                Josh.currentCohort=Three;
                Three.InstructorList.Add(Josh);

            Instructor Kim = new Instructor();
                Kim.firstName="Kim";
                Kim.lastName="Preece";
                Kim.slackHandle="Kim10likeBen10";
                Kim.currentCohort=Two;
                Two.InstructorList.Add(Kim);

            Instructor Jordan = new Instructor();
                Jordan.firstName="Jordan";
                Jordan.lastName="Castelloe";
                Jordan.slackHandle="NashvillesFinestButNotThePolice";
                Jordan.currentCohort=One;
                One.InstructorList.Add(Jordan);
                Jordan.giveExercise(Connor, Planets);
            List<Instructor> InstructorList = new List<Instructor>(){
                Jordan,
                Josh,
                Kim
            };
            List<Student> StudentList = new List<Student>(){
                Connor,
                Bobby,
                Sydney,
                Sable
            };
            List<Cohort> CohortList = new List<Cohort>(){
                One,
                Two
            };
            List<Exercise> ExerciseList = new List<Exercise>(){
                Planets,
                Nutshell,
                Python101,
                WonderTwins
            };





            Console.WriteLine($"{One.name} students:");
            One.StudentList.ForEach(studentlist => Console.WriteLine($"{studentlist.firstName} {studentlist.lastName}  || slack: {studentlist.slackHandle}"));
             Console.WriteLine($"{One.name} Instructors:");
            One.InstructorList.ForEach(list => Console.WriteLine($"{list.firstName} {list.lastName}  || Slack: {list.slackHandle}"));
            Connor.ExerciseList.ForEach(list => Console.WriteLine($"{Connor.firstName}'s exercises: {list.name} {list.language}"));
           IEnumerable<Exercise> exercisequery =
    ExerciseList.Where(exercise => exercise.language == "Javascript");
Console.WriteLine("These are the Javascript exercises");
foreach(Exercise exercise in exercisequery)
{
    Console.WriteLine(exercise.name);
};
         IEnumerable<Student> studentQueryOne =
    StudentList.Where(student => student.currentCohort == One);
Console.WriteLine($"These are the students in Cohort One:");
foreach(Student student in studentQueryOne)
{
    Console.WriteLine($"{student.firstName} {student.lastName}");
};

        IEnumerable<Instructor> instructorQueryTwo =
    InstructorList.Where(instructor => instructor.currentCohort == Two);
Console.WriteLine($"These are the Instructors in Cohort Two:");
foreach(Instructor instructor in instructorQueryTwo)
{
    Console.WriteLine($"{instructor.firstName} {instructor.lastName}");
};




            // Console.WriteLine($"{Two.name} students:");
            // Two.StudentList.ForEach(studentlist => Console.WriteLine($"{studentlist.firstName} {studentlist.lastName}|| slack: {studentlist.slackHandle}"));
            // Console.WriteLine($"{Three.name} students:");
            // Three.StudentList.ForEach(studentlist => Console.WriteLine($"{studentlist.firstName} {studentlist.lastName}|| slack:{studentlist.slackHandle}"));



        }
    }
}
