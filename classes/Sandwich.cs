using System;
using System.Collections.Generic;
namespace classes{
    class Sandwich{
        public Sandwich(string nameParam, double priceParam, int calorieParam){
            name = nameParam;
            price = priceParam;
            _numberOfCalories = calorieParam;
        }

         public void getTotalCalories(int numberOfServings){
        int totalCalories = _numberOfCalories*numberOfServings;
        Console.WriteLine(totalCalories);
        }
        public string breadType {get; set;}
        public double price {get; set;}
        private int _numberOfCalories {get; set;}

        public string name {get; set;}
        public List<String> IngredientList = new List<string>();
    }
}