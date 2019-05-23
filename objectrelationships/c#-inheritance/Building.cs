using System;
using System.Collections.Generic;

namespace c__inheritance{
    abstract class Building
    {
        public List<Room> RoomList {get; set;} = new List<Room>();

        public void addRoom(Room roomToAdd){
            this.RoomList.Add(roomToAdd);
            roomToAdd.Building = this;
        }
        public abstract void totalArea();
        public abstract void getAverageArea();
   public string type {get; set;}
        }


    }
