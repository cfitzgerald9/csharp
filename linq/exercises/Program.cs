using System;
using System.Collections.Generic;
using System.Linq;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            // Order these student names alphabetically, in descending order (Z to A)
            List<string> names = new List<string>()
            {
                "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                "Francisco", "Tre"
            };

            // Call Array.Sort method.
            List<string> descend = new List<string>();
            foreach (string name in names)
            {
                descend.Add(name);
                descend.Sort();
                descend.Reverse();
            }
            foreach (string name in descend)
            {
                Console.WriteLine($"This is the list of names in z-a order: {name}");
            };

            // Build a collection of these numbers sorted in ascending order
            List<int> numbers = new List<int>(){
            15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            Console.WriteLine("-------------");
            IEnumerable<int> descending = numbers.OrderByDescending(n => n);
            foreach (int number in descending)
            {
                Console.WriteLine($"This is the list of numbers in descending order: {number}");
            };


            List<int> numbers2 = new List<int>()
            {
                15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
            };
            Console.WriteLine("-------------");
            Console.WriteLine($"This is how many numbers there are in the array:{numbers2.Count()}");


            // How much money have we made?
            List<double> purchases = new List<double>(){
                2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
            };
            double total = purchases.Sum();
            Console.WriteLine($"This is the total of all purchases:{total}");


            // What is our most expensive product?
            List<double> prices = new List<double>(){
                879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
            };
            double biggestOne = prices.Max();
            Console.WriteLine($"This is the most expensive item: {biggestOne}");
            Console.WriteLine("--------");

            /*
                Store each number in the following List until a perfect square
                is detected.

                Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
            */
            Console.WriteLine("These are all the numbers until the first perfect square:");
            List<int> wheresSquaredo = new List<int>()
{
    66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
};
            List<double> newArray = new List<double>();
            foreach (var number in wheresSquaredo)
            {
                double divis = Math.Sqrt(number);
                var match = divis % 1 == 0;
                if (match == false)
                {
                    newArray.Add(number);
                }
                else
                {
                    newArray.Add(number);
                    break;
                }
            }
            foreach (var number in newArray)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine("--------------");
            Console.WriteLine("These are the millionaires:");
            // Build a collection of customers who are millionaires
            List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };

            List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };

            List<Customer> bigMoney = (from customer in customers where customer.Balance > 999999 select customer).ToList();
            bigMoney.ForEach(customer => Console.WriteLine($"{customer.Name} is a millionaire!"));
            var results = bigMoney.GroupBy(
                p => p.Bank, p => p.Name,
                (banky, name) => new
                {Key = banky, name = name});
                Console.WriteLine("--------");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key} || Millionaire count: {result.name.Count()}");
            }
            Console.WriteLine("--------");
            IEnumerable<ReportItem> millionaireReport =
            from customer in customers where customer.Balance>= 1000000
            orderby customer.Name.Split(' ')[1]
            join bank in banks on customer.Bank equals bank.Symbol
            select new ReportItem(){
                CustomerName = customer.Name,
                BankName = bank.Name
            };



            foreach (var item in millionaireReport)
            {
                Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            }
        }
    }
}




