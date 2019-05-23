using System;
using System.Collections.Generic;

namespace lists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> planetList = new List<string>(){"Mercury", "Mars"};
            planetList.Add("Jupiter");
            planetList.Add("Saturn");
            List<string> otherPlanetList = new List<string>(){"Uranus", "Neptune"};
            planetList.AddRange(otherPlanetList);
            planetList.Insert(1, "Earth");
            planetList.Insert(1, "Venus");
            planetList.Add("Pluto");

           string[] rockyPlanets = planetList.GetRange(0, 4).ToArray();
           Console.WriteLine("These are the rocky planets:");
        foreach( string planet in rockyPlanets )
        {
            Console.WriteLine($"{planet}");
        }
        planetList.Remove("Pluto");
         Console.WriteLine("These are all the planets:");
        planetList.ForEach(planet => Console.WriteLine(planet));

Random random = new Random();
List<int> numbers = new List<int> {
    random.Next(6),
    random.Next(6),
    random.Next(6),
    random.Next(6),
    random.Next(6),
    random.Next(6),
};

for (int i=0; i<numbers.Count; i++) {
   if(i == numbers[i]){
       Console.WriteLine($"The number list contains {numbers[i]}");
   }
   else{
       Console.WriteLine($"The number list does not contain {i}");
   }

}




        }
    }
}
