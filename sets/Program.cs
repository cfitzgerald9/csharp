using System;
using System.Collections.Generic;

namespace sets
{
    class Program
    {
        static void Main(string[] args)
        {

HashSet<string> showroom = new HashSet<string>();
showroom.Add("Ford Mustang");
showroom.Add("Toyota Camry");
showroom.Add("Pontiac G5");
showroom.Add("Ford Crown Victoria");




    Console.WriteLine($"{showroom.Count}");
    showroom.Add("Ford Crown Victoria");
    Console.WriteLine($"{showroom.Count}");


HashSet<string> usedLot = new HashSet<string>();
usedLot.Add("Ford Fiesta");
usedLot.Add("Mazda S5");
showroom.UnionWith(usedLot);
Console.WriteLine($"{showroom.Count}");
showroom.Remove("Ford Fiesta");
Console.WriteLine($"{showroom.Count}");
HashSet<string> junkyard = new HashSet<string>();
junkyard.Add("Toyota Camry");
junkyard.Add("Pontiac G5");
junkyard.Add("Chevy Cobalt");
junkyard.Add("Toyota Tundra");
HashSet<string> clone = new HashSet<string>(showroom);
clone.IntersectWith(junkyard);
showroom.UnionWith(junkyard);
showroom.Remove("Toyota Tundra");
foreach(string item in showroom){
    Console.WriteLine(item);
}
        }
    }
}
