// Create a person object
const person = {
  isEmployee: false,
  printIntroduction: function() {
    console.log(
      `My name is ${this.name} and I'm ${this.isEmployee ? "an employee" : "a customer"}.`
    );
  },
  fileMaintenanceRequest: function(descriptionOfProblem) {
    console.log(
      `${this.name} has filed a maintenance request: ${descriptionOfProblem}`
    );
  }
};


// person.name = "Josh";
person.printIntroduction();
console.log(person);



// Create an employee object that inherits all the properties and methods from the person object (its prototype)
const employee = Object.create(person);
employee.company = "Awesome Door Builders Inc";
employee.fileMaintenanceRequest = descriptionOfProblem => {
  console.log(
    `The employee ${
      this.name
    } has filed a maintenance request: ${descriptionOfProblem}`
  );
};

// console.log(employee);



const kim = Object.create(employee);
kim.name = "Kim"; // the .name property exists on kim, but not on employee or person
kim.isEmployee = true; // inherited properties can be overwritten
console.log(kim);
