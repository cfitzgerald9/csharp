using System;
namespace interfaces{

    class Penguin : IWalking, ISwimming
    {
        public int numberOfLegs { get;set;}
        public int maxDepth { get; set; }
        public bool isMammal { get; set; }
        public bool isSaltWater { get; set; }

        public string name {get; set;}

        public void swim()
        {
            Console.WriteLine($"{name} the penguin is walking!");
        }

        public void walk()
        {
             Console.WriteLine($"{name} the penguin is swimming!");
        }
    }
}