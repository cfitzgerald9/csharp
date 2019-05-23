using System;
using System.Collections.Generic;

namespace c__inheritance{
    class Supervisor: Employee
    {
        public Supervisor(string jobTitleParam) : base(jobTitleParam) => JobTitle = jobTitleParam;

        public void SubmitMaintenanceRequest(Maintenance problemToBeFixed, Employee assignedEmployeeYes)
        {
            problemToBeFixed.assignedEmployee = assignedEmployeeYes;
            assignedEmployeeYes.maintenanceList.Add(problemToBeFixed);
            Console.WriteLine($"A supervisor assigned a maintenance request to {assignedEmployeeYes.FirstName}: {problemToBeFixed.description}");
        }
    }
}