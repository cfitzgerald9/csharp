using System;

namespace interfaces{

    class Terrapin : ISwimming, IWalking
    {
        public int maxDepth { get; set;}
        public string name { get; set;}
        public bool isMammal { get; set;}
        public bool isSaltWater { get; set;}
        public int numberOfLegs { get;set;}

        public void swim()
        {
            Console.WriteLine($"{name} the glorified turtle is swimming around.");
        }

        public void walk()
        {
             Console.WriteLine($"{name} the glorified turtle is walking around...slowly.");
        }
    }
}