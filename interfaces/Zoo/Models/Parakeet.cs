using System;

namespace interfaces{

    class Parakeet : IFlying
    {
        public int maximumHeight {get; set;}
        public int speed {get;set;}
        public bool hasFeathers {get;set;}

        public string name{get;set;}

        public void fly()
        {
            Console.WriteLine("The gross parakeet is flying!");
        }
    }
}