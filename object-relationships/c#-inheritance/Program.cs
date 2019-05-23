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
            Joey.sayHi();

            Joey.SubmitMaintenanceRequest("The sink is broken");

            Employee David = new Employee("Master of the Universe");
            David.SubmitMaintenanceRequest("The door is also broken");


            // ASSOCIATION

            // Composition- buildings and rooms
            Building ChaseBuilding = new Building();
            Room Hackery = new Room();
            Hackery.Building = ChaseBuilding;

            // Aggregation- companies and employees || companies and customers
            Company AwesomeDoorCompany = new Company();
            AwesomeDoorCompany.EmployeeList.Add(David);
            David.Company = AwesomeDoorCompany;



        }
    }
}
