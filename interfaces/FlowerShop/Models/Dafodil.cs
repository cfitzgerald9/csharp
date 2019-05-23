using System;
namespace interfaces{
    class Dafodil : IMothersDayFlower, IWeddingFlower {
        public string name{get; set;}
        public string color {get; set;}
        public string species {get; set;}
        public int stemLength {get; set;}
        public bool smellsGood {get; set;}

    }
}