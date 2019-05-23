using System;
using System.Collections.Generic;

namespace dictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
    Create a dictionary with key value pairs to
    represent words (key) and its definition (value)
*/
Dictionary<string, string> wordsAndDefinitions = new Dictionary<string, string>();

// Add several more words and their definitions
wordsAndDefinitions.Add("Awesome", "The feeling of students when they are learning C#");
wordsAndDefinitions.Add("Turrible", "BADBADNOTGOOD");
wordsAndDefinitions.Add("Banana", "A delicious yellow fruit");
wordsAndDefinitions.Add("Chair", "A seat");

/*
    Use square bracket lookup to get the definition two
    words and output them to the console
*/

/*
    Loop over dictionary to get the following output:
        The definition of [WORD] is [DEFINITION]
        The definition of [WORD] is [DEFINITION]
        The definition of [WORD] is [DEFINITION]
*/
foreach (KeyValuePair<string, string> word in wordsAndDefinitions)
{
    Console.WriteLine($"The definition of {word.Key}: {word.Value}" );
}

// Make a new list
List<Dictionary<string, string>> dictionaryOfWords = new List<Dictionary<string, string>>();


Dictionary<string, string> excitedWord = new Dictionary<string, string>();
Dictionary<string, string> sadWord = new Dictionary<string, string>();
// Add each of the 4 bits of data about the word to the Dictionary
sadWord.Add("word", "sad");
sadWord.Add("definition", "feeling or showing sorrow; unhappy or depressed");
sadWord.Add("example sentence", "I am sad because we are learning C#.");
sadWord.Add("part of speech", "adjective");
excitedWord.Add("word", "excited");
excitedWord.Add("definition", "having, showing, or characterized by a heightened state of energy, enthusiasm, eagerness");
excitedWord.Add("example sentence", "I am excited to learn C#!");
excitedWord.Add("part of speech", "adjective");


// Add Dictionary to your `dictionaryOfWords` list

dictionaryOfWords.Add(excitedWord);
dictionaryOfWords.Add(sadWord);

// create another Dictionary and add that to the list


/*
    Iterate your list of dictionaries and output the data

    Example output for one word in the list of dictionaries:
        word: excited
        definition: having, showing, or characterized by a heightened state of energy, enthusiasm, eagerness
        part of speech: adjective
        example sentence: I am excited to learn C#!
*/

// Iterate the List of Dictionaries
foreach (Dictionary<string, string> word in dictionaryOfWords)
{
    foreach (KeyValuePair<string, string>wordData in word)
    {
        Console.WriteLine($"{wordData.Key}: {wordData.Value}");
    }
}

Dictionary<string, List<string>> idioms = new Dictionary<string, List<string>>();
idioms.Add("Penny", new List<string> { "A", "penny", "for", "your", "thoughts" });
idioms.Add("Injury", new List<string> { "Add", "insult", "to", "injury" });
idioms.Add("Moon", new List<string> { "Once", "in", "a", "blue", "moon" });
idioms.Add("Grape", new List<string> { "I", "heard", "it", "through", "the", "grapevine" });
idioms.Add("Murder", new List<string> { "Kill", "two", "birds", "with", "one", "stone" });
idioms.Add("Limbs", new List<string> { "It", "costs", "an", "arm", "and", "a", "leg" });
idioms.Add("Grain", new List<string> { "Take","what","someone","says","with","a","grain","of","salt" });
idioms.Add("Fences", new List<string> { "I'm", "on", "the", "fence", "about", "it" });
idioms.Add("Sheep", new List<string> { "Pulled", "the", "wool", "over", "his", "eyes" });
idioms.Add("Lucifer", new List<string> { "Speak", "of", "the", "devil" });
String sep   = " ";


foreach (KeyValuePair<string, List<string>> oneIdiom in idioms)
{
   String result = string.Join(sep, oneIdiom.Value);
    {
        Console.WriteLine($"{oneIdiom.Key}: {result}");
    }
}

List<string> planetList = new List<string>(){"Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune"};
List<KeyValuePair<string, string>> probeDestinations = new List<KeyValuePair<string, string>>();
probeDestinations.Add(new KeyValuePair<string, string>("Viking 1", "Mars"));
probeDestinations.Add(new KeyValuePair<string, string>("Mariner 3", "Mars"));
probeDestinations.Add(new KeyValuePair<string, string>("Mariner 7", "Mars"));
probeDestinations.Add(new KeyValuePair<string, string>("Mariner 10", "Mercury"));
probeDestinations.Add(new KeyValuePair<string, string>("MESSENGER", "Mercury"));
probeDestinations.Add(new KeyValuePair<string, string>("Mariner 1", "Venus"));

// Iterate planets
foreach (string planet in planetList)
{
    // List to store probes that visited the planet
    List<string> matchingProbes = new List<string>();

    // Iterate probeDestinations
    foreach(KeyValuePair<string, string> probe in probeDestinations)
    {
        if(probe.Value == planet){
            matchingProbes.Add($"{probe.Key}");
        }

    }
  String result = String.Join( ", ",  matchingProbes);
            if (planet!= null){
        Console.WriteLine($"{planet}: {result}");
            }
        }
    }
}
}



