using System;
using System.Collections.Generic;

namespace c__inheritance{
    class Employee: Person{

        public Employee(string jobTitleParam){
            StartDate = DateTime.Now;
            string JobTitle = jobTitleParam;
        }

        public string JobTitle{get;set;}

        public DateTime StartDate {get; set;}

        public override void SubmitMaintenanceRequest(Maintenance problemToBeFixed)
        {
            Console.WriteLine($"An employee submitted a maintenance request: {problemToBeFixed.description}");
        }

        public List<Maintenance> maintenanceList = new List <Maintenance>();
         public void constructBuilding(Building buildingToBuild)
        {
            Console.WriteLine($"{this.FirstName} is building a(n) {buildingToBuild.type}");
        }
    }
}