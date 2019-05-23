using System;
using System.Collections.Generic;
using System.Linq;

namespace c__inheritance{
    class Apartment: Building{
       public int numberOfUnits {get; set;}

        public override void totalArea(){
            int sum = 0;
              foreach(Room room in RoomList){
                    sum+= room.area;
                }
                Console.WriteLine($"This apartment has a total area of {sum}");
            }
            public override void getAverageArea(){
                int sum = 0;
                foreach(Room room in RoomList){
                    sum+= room.area;
                }
                int test = sum / RoomList.Count();
                Console.WriteLine($"this apartment has an average room area of: {test}");
    }
}
}