using System;
using System.Collections.Generic;

namespace interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            FlowerShop myShop = new FlowerShop();
            List<Rose> bouqetOfRoses = myShop.createRoseBouqet();
            bouqetOfRoses.ForEach(rose => Console.WriteLine(rose.species));

            Bat Barty = new Bat(){
                name="Barty",
                maximumHeight= 100,
                speed=30,
                hasFeathers=false,
                isVampire=true,
            };
            Barty.fly();
            Penguin Penny = new Penguin(){
                name="Penny",
                isMammal=false,
                isSaltWater = true,
                maxDepth = 50
            };
            Penny.swim();
            Penny.walk();
        }
    }
}
