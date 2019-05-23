using System;
namespace classes
{
    //     Untitled
    // # Lightning Exercise Three
    // 1. Add a method to your customer class called `AddRewardPoints`. This method should accept a parameter of the number of reward points the customer earned (an `int`) and add it to the customers `RewardPoints` property. Then it should write to the console the new value of the customer's `RewardPoints`.
    class Customer
    {
        public string firstName { get; }
        public string lastName { get; }
        public int rewardPoints { get; set; }
        public string email { get; set; }
        public Sandwich favoriteSandwich { get; set; }
//         # Lightning Exercise Four
// 1. Add a constructor to the customer class that sets the customer's first name, last name, and email.
// 3. Refactor wherever you created your instances of customers to pass data into the constructor method.
 public Customer(string firstNameParam, string lastNameParam, string emailParam){
            firstName = firstNameParam;
            lastName = lastNameParam;
            email = emailParam;
        }
        public void AddRewardPoints(int pointsToAdd){
           rewardPoints += pointsToAdd;
           Console.WriteLine(rewardPoints);
        }
        public void eatSandwich(Sandwich sandwichToEat)
        {
            if (sandwichToEat.name == favoriteSandwich.name)
            {
                Console.WriteLine("delicious");
            }
            else
            {
                Console.WriteLine("gross");
            }
        }
    }
}