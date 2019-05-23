using System;

namespace interfaces{
    public interface IFlying
    {
        void fly();

        int maximumHeight {get; set;}

        int speed {get; set;}

        string name{get;set;}

        bool hasFeathers {get; set;}
    }
}