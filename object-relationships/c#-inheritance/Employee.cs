using System;

namespace c__inheritance{
    class Employee: Person{

        public Employee(string jobTitleParam){
            StartDate = DateTime.Now;
            JobTitle = jobTitleParam;
        }

        public DateTime StartDate {get; set;}

        public string JobTitle {get; set;}

        

        public override void SubmitMaintenanceRequest(string problemToBeFixed)
        {
            Console.WriteLine($"An employee submitted a maintenance request: {problemToBeFixed}");
        }
    }
}