// Create a custom type for Employee. An employee has the following properties.
// First name (string)
// Last name (string)
// Title (string)
// Start date (DateTime)
using System;

namespace employeelist{
    class Employee{
        public Employee(string firstNameParam, string lastNameParam){
            startDate=DateTime.Now;
            firstName=firstNameParam;
            lastName=lastNameParam;
        }
        public string firstName {get;}
        public string lastName {get;}
        public string title {get; set;}
        public DateTime startDate {get;}
    }

}