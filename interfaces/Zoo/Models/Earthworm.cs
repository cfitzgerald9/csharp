using System;

namespace interfaces{

    class Earthworm : IDigging
    {
        public string name{get;set;}
        public int maximumDepth { get; set;}
        public int speed { get; set;}
        public bool isInsect { get; set;}

        public void dig()
        {
            Console.WriteLine($"{name} is just kind of wormin' around.");
        }
    }
}