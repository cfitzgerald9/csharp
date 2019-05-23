using System;

namespace employeelist
{
    class Program
    {
        static void Main(string[] args)
        {
            Company Amazon = new Company("Amazon");
            Employee Ricky = new Employee("Ricky", "Bobby");
            Employee Tricky = new Employee("Tricky", "Sobby");
            Employee Vicky = new Employee("Vicky", "Hobby");
            Amazon.employeelist.Add(Vicky);
            Amazon.employeelist.Add(Ricky);
            Amazon.employeelist.Add(Tricky);
            Amazon.listEmployees();

        }
    }
}
