using System;

namespace c__inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            // INHERITANCE
            // Employee and Customer both inherit from the Person class

            Customer Joey = new Customer();


            Employee David = new Employee("Master of the Universe");
            David.FirstName = ("David");


            // ASSOCIATION

            // Composition- buildings and rooms
            Building ChaseBuilding = new Office("Chase Building");
            Room Hackery = new Room();

            Room Bank = new Room();
            Bank.width=100;
            Bank.length=200;
            Hackery.width=100;
            Hackery.length=100;
            ChaseBuilding.addRoom(Bank);
            ChaseBuilding.addRoom(Hackery);
            ChaseBuilding.totalArea();
            ChaseBuilding.getAverageArea();


            // Aggregation- companies and employees || companies and customers
            Company AwesomeDoorCompany = new Company();
            AwesomeDoorCompany.EmployeeList.Add(David);
            David.Company = AwesomeDoorCompany;
            David.constructBuilding(ChaseBuilding);

            Maintenance Rat = new Maintenance();
            Rat.description= "There's a massive rat in the bathroom that needs taken outside.";
            David.SubmitMaintenanceRequest(Rat);
            Supervisor Connor = new Supervisor("Manager");
            Connor.FirstName = "Connor";
            Connor.LastName = "FitzGerald";
            Connor.SubmitMaintenanceRequest(Rat, David);


        }
    }
}
