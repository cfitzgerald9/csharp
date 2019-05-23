using System;

namespace interfaces{

    class Bat : IFlying
    {
        public int maximumHeight {get; set;}
        public int speed { get; set;}
        public bool hasFeathers {get; set;}

        public bool isVampire{get; set;}

        public string name {get; set;}

        public void fly()
        {
            Console.WriteLine($"{name} the bat is flying!");
        }
    }
}