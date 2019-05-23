using System;
using System.Collections.Generic;
using System.Linq;
namespace lightning

//restriction and filtering//
{
    class Program
    {
        static void Main(string[] args)
        {
//             # Doin' it the hard way

// Here's a list of the number of students in each cohort at NSS over the past year.
// ```
  List<int> cohortStudentCount = new List<int>()
            {
                25, 12, 28, 22, 11, 25, 27, 24, 19
            };
// ```
 int sum = 0;
 int count = 0;
 int largestCohort = 0;

// Using only a `foreach` loop and basic arithmetic:
foreach(int studentCount in cohortStudentCount){
    sum += studentCount;
    count ++;
    if(studentCount > largestCohort){
        largestCohort = studentCount;
    }
}
int smallestCohort = largestCohort;
foreach(int studentCount in cohortStudentCount){
    if(studentCount < smallestCohort){
        smallestCohort = studentCount;
    }
}
int average = sum/cohortStudentCount.Count;
Console.WriteLine(sum);
Console.WriteLine(count);
Console.WriteLine(largestCohort);
Console.WriteLine(smallestCohort);
// 1. Find the average number of students per cohort.
// 2. Find the total number of students who graduated in the past year.
// 3. How many students were in the biggest cohort?
// 4. How many students were in the smallest?

// Find the words in the collection that start with the letter 'L'
List<string> fruits = new List<string>() {"Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry"};

IEnumerable<string> LFruits = from fruit in fruits where fruit[0].ToString() =="L" select fruit;
foreach(string fruit in LFruits){
    Console.WriteLine(fruit);
}

// Which of the following numbers are multiples of 4 or 6
List<int> numbers = new List<int>()
{
    15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
};

IEnumerable<int> fourSixMultiples = numbers.Where(num => num % 4 == 0 || num % 6 == 0);
foreach(int four6 in fourSixMultiples){
    Console.WriteLine(four6);
}
        }
    }
}
