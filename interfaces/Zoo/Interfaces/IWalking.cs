using System;

namespace interfaces{
    public interface IWalking
    {
        void walk();
        string name {get;set;}
        int numberOfLegs {get; set;}
    }
}