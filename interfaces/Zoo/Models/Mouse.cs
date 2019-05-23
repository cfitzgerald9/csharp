using System;

namespace interfaces{

    class Mouse : IWalking, IDigging
    {
        public string name { get;set;}
        public int maximumDepth { get;set;}
        public int speed { get;set;}
        public bool isInsect { get;set;}
        public int numberOfLegs { get;set;}

        public void dig()
        {
           Console.WriteLine($"{name} is digging a hole in a cereal box.");
        }

        public void walk()
        {
           Console.WriteLine($"{name} is scurrying around.");
        }
    }
}