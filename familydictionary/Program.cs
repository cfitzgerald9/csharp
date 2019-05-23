using System;
using System.Collections.Generic;

namespace familydictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, string>> myFamily = new Dictionary<string, Dictionary<string, string>>();

            myFamily.Add("big sister", new Dictionary<string, string>(){
    {"name", "Jessica"},
    {"age", "30"},
});
     myFamily.Add("little sister", new Dictionary<string, string>(){
    {"name", "Bailey"},
    {"age", "16"},
});
     myFamily.Add("youngest sister", new Dictionary<string, string>(){
    {"name", "Darby"},
    {"age", "12"},
});
            myFamily.Add("wife", new Dictionary<string, string>(){
    {"name", "Mesha"},
    {"age", "25"}
});
            myFamily.Add("mom", new Dictionary<string, string>(){
    {"name", "Lori"},
    {"age", "50"}
});
            foreach (KeyValuePair<string, Dictionary<string, string>> familyMember in myFamily)
            {
                Console.WriteLine($"{familyMember.Value["name"]} is my {familyMember.Key} and is {familyMember.Value["age"]} years old.");
            }
        }
    }
}
