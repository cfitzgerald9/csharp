using System;

namespace interfaces{
    public interface ISwimming
    {
        void swim();
        int maxDepth {get; set;}

        string name{get;set;}
        bool isMammal{get; set;}
        bool isSaltWater {get; set;}
    }
}