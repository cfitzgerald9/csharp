using System;
using System.Collections.Generic;

namespace c__inheritance
{
    class Company
    {
        public string Name { get; set; }

        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

        public List<Customer> CustomerList { get; set; } = new List<Customer>();
    }
}