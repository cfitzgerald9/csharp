using System;
using System.Collections.Generic;
using System.Linq;

namespace c__inheritance{
    class House: Building{
        House(int numberOfBedroomsParam){
            int numberOfBedrooms=numberOfBedroomsParam;
            type = "house";


    }
public override void totalArea(){
            int sum = 0;
              foreach(Room room in RoomList){
                    sum+= room.area;
                }
                Console.WriteLine($"This house has a total area of {sum}");
            }
            public override void getAverageArea(){
                int sum = 0;
                foreach(Room room in RoomList){
                    sum+= room.area;
                }
                int test = sum / RoomList.Count();
                Console.WriteLine($"This house has an average room area of: {test}");
    }
    }
}