using System;
using System.Collections.Generic;

namespace classes
{
        class Program
    {
        static void Main(string[] args)
        {
//            # Lightning Exercise One

// 1.  Create a new dictionary of to represent a sandwich. The dictionary should store the follow data:
//     - Bread type
//     - Price
//     - Number of calories
//     - A comma-seperated, stringified list of ingridients
// Dictionary<string, string> sandvich = new Dictionary<string, string>(){
//     {"Bread type", "French"},
//     {"Price", "1.99"}
//         };
Sandwich tuna = new Sandwich("tuna", 1.99, 150);
tuna.breadType = "white";
tuna.price= 1.99;
tuna.getTotalCalories(3);

tuna.name = "tuna";
tuna.IngredientList = new List<string>(){
    "Bread", "Mayo","Tuna"
};
Sandwich PBJ = new Sandwich("Peanut butter and jelly", 1.50, 100);
PBJ.breadType = "white";
PBJ.price= 1.50;
PBJ.getTotalCalories(4);
PBJ.name = "Peanut butter and jelly";
PBJ.IngredientList = new List<string>(){
    "Bread", "Peanut Butter","Jelly"
};

// # Lightning Exercise Two
// 1. Create a new class that represents a customer at the sandwich shop
// 2. Give the customer the following public properties:
//     - FirstName (string)
//     - LastName (string)
//     - RewardPoints (int)
//     - Email (string)
// 3. In the `Main()` method of your `Program` class, create a list of customers.
// 4. Print each customer's first name and last name to the console.
Customer dude = new Customer("Jeff" ,"Lebowski", "thedudeabides@gmail.com");
dude.rewardPoints = 0;
dude.favoriteSandwich = tuna;
dude.eatSandwich(tuna);


Customer metal = new Customer("Nathan", "Explosion", "murmaider@gmail.com");
metal.rewardPoints = 666;
metal.favoriteSandwich = PBJ;
metal.eatSandwich(tuna);
metal.AddRewardPoints(100);



List<Customer> customerList = new List<Customer>() {
                metal,
                dude
            };
            customerList.ForEach (customer=> Console.WriteLine($"{customer.firstName} {customer.lastName}"));
            {

            }
    }
}
}
