// Date founded (DateTime)
// Company name (string)
// Employees (List<Employee>)
using System;
using System.Collections.Generic;
namespace employeelist{
    class Company{
        public Company(string nameParam){
            name = nameParam;
            foundedOn = DateTime.Now;
        }
        public DateTime foundedOn{get;}
        public string name {get;}

       public List<Employee> employeelist = new List<Employee>();

        public void listEmployees(){
            Console.WriteLine($"{this.name} employees:");
            foreach(Employee employee in this.employeelist){
                Console.WriteLine($"{employee.firstName} {employee.lastName}");
            }
        }
    }
}