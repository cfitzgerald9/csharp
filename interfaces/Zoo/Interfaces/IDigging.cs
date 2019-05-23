using System;

namespace interfaces{
    public interface IDigging
    {
        void dig();

        string name{get;set;}

        int maximumDepth {get; set;}

        int speed {get; set;}

        bool isInsect {get; set;}
    }
}