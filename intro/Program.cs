using System;
using System.Collections.Generic;
// Lightning Exercise One: Data Types

// In your console application, we're going to keep track of some information about a taco shop.

// 1. In the `Main` method of your `Program.cs` file, create variables to hold the following data:
// - The name of the taco shop (a string)
// - The address of the taco shop (a string)
// - The monthly operating budget (a double)
// - Whether the taco shop is currently open (a boolean)
// - The number of employees at the taco shop (an integer)
// - The number of items on the menu of the taco shop (an integer)
// 2. Create a sentence about the taco shop using at least three of the variables you just created.
// 3. Print your sentence to your terminal.
// 4. Example output: "Jordan's Awesome Taco Shop is located at 45 Taco Road and has 10 employees".
namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean isOpen = false;
            string shopName = "Blargy Boy Tacos";
            string shopAddress = "1234 Bean Blaster Way";
            double operationBudget = 10000.67;
            int numberOfEmployees = 5;
            int numberOfItems = 25;
            string foodItem = "Taco Salad";
            int numberOfTacoSalads = 3;
            double priceOfTacoSalad = 3.50;
            Console.WriteLine($"Welcome to {shopName}, located at {shopAddress}. Are we open today? {isOpen}.");

            List<string> employeeList = new List<string>(){
               "Dale",
               "Bobby",
               "Connor"
            };

            foreach (string singleEmployee in employeeList)
            {
                if (singleEmployee == "Connor")
                {
                    Console.WriteLine($"{singleEmployee} is the best employee ever!");
                }
                else
                {
                    Console.WriteLine($"{singleEmployee} is a so-so-employee");
                }
            }
                       //Lightning Exercise Four: Lists Methods

            // 1. Use .Add() to add another employee to your list of employees
            // 2. Use .Insert() to add a new employee to the end of the list.
            // 3. Create a new list of strings to represent the names of customers at the taco shop.
            // 4. Use .Find() to extract a customer named "Kim", if one exists, from your list of customers.
            // 5. Kim was a customer at the taco shop, but they've decided to hire her. Use .Remove() to remove her from the customers list and add her to the employees list.
            // 6. How many customers does the taco shop have?
            // 7. The taco shop would like to see a list of everyone who's in the store regularly, both customers and employees. Use .AddRange() to combine the list of customers and employees and then use a .ForEach() loop print the newly combined list to the console.
            employeeList.Add("Tommy");
            employeeList.Insert(2, "Matt");
            foreach (string singleEmployee in employeeList)
            {
                Console.WriteLine($"{singleEmployee}");
            }
            List<string> customerList = new List<string>(){
               "Jordan",
               "Kim",
               "Josh"
            };
            string bestCustomer = customerList.Find(customer => customer == "Kim");

            customerList.Remove(bestCustomer);
            employeeList.Add(bestCustomer);
            Console.WriteLine($"{bestCustomer} is our best customer!");
            foreach (string singleEmployee in employeeList)
            {
                Console.WriteLine($"{singleEmployee}");
            }

            Console.WriteLine(customerList.Count);
            List<string> errybody = new List<string>();
            errybody.AddRange(customerList);
            errybody.AddRange(employeeList);
            foreach (string singleBody in errybody)
            {
                Console.WriteLine($"{singleBody}");
            }

  //         Lightning Exercise Five: Dictionaries
        // 1. Create a dictionary to represent the menu at the taco shop. Each KeyValuePair in the dictionary should have a key of a string that represents the name of the menu item and a value that represents its price (double).
        // 2. Create a dictionary to represent the employees at the taco shop. Each KeyValuePair should have a key of a string that represents the employee's name and a value of a string that represents their favorite menu item.
        // 3. Loop over the dictionary of employees. For each employee, print their favorite menu item and it's price. Example output: "Jenny's favorite dish is fish tacos for only $6.99"
        Dictionary<String, double> tacoMenu = new Dictionary<string,double>(){
                {"Taco", 1.59},
                {"Enchilada", 2.00},
                {"Nachos", 1.00}
        };
         Dictionary<string, string> tacoEmployees = new Dictionary<string, string>(){
                {"Jenny", "Enchilada"},
                {"Barty", "Nachos"},
                {"TrickIckyIcky", "Taco"}
        };
        foreach (KeyValuePair<string, string> employeeFood in tacoEmployees){
            string name = employeeFood.Key;
            string food = employeeFood.Value;
            double price = tacoMenu[food];
            Console.WriteLine($"{name}'s favorite food is the {food}, which costs ${price}");
        }
        {

        }


        }


    }
}
