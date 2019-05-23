using System;

namespace interfaces{

    class Rattlesnake : IWalking
    {
        public string name { get;set;}
        public int numberOfLegs { get;set;}

        public void walk()
        {
            Console.WriteLine($"Although snakes can't walk, {name} is doing his best.");
        }
    }
}