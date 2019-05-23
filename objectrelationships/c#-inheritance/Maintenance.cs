using System;

namespace c__inheritance{
class Maintenance{
    public string description{get;set;}

    public Person reportFiler{get; set;}

    public Employee assignedEmployee{get;set;}

    public Building reportBuilding {get; set;}

    public bool isCompleted{get;set;}
}


}
// A description of the problem to be fixed (string)
// A property to tell us who filed the report
// An assignedEmployee property of type Employee
// A property that tells us which building the report was filed for
// A property called Completed that holds a boolean