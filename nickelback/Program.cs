using System;
using System.Collections.Generic;

namespace nickelback
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<Dictionary<string, string>> allSongs = new HashSet<Dictionary<string, string>>();
            List<Dictionary<string, string>> goodSongs = new List<Dictionary<string, string>>();

            Dictionary<string, string> cureHeaven = new Dictionary<string, string>();
            cureHeaven.Add("The Cure", "Just Like Heaven");
            allSongs.Add(cureHeaven);

            Dictionary<string, string> iverHolycene = new Dictionary<string, string>();
            iverHolycene.Add("Bon Iver", "Holycene");
            allSongs.Add(iverHolycene);

            Dictionary<string, string> nickRemind = new Dictionary<string, string>();
            nickRemind.Add("Nickleback", "How You Remind Me");
            allSongs.Add(nickRemind);

            Dictionary<string, string> nickGraph = new Dictionary<string, string>();
            nickGraph.Add("Nickleback", "Photograph");
            allSongs.Add(nickGraph);

            Dictionary<string, string> kenTree = new Dictionary<string, string>();
            kenTree.Add("Kendrick Lamar", "Money Trees");
            allSongs.Add(kenTree);

            Dictionary<string, string> nickStar = new Dictionary<string, string>();
            nickStar.Add("Nickleback", "Rockstar");
            allSongs.Add(nickStar);

            Dictionary<string, string> blackberryJam = new Dictionary<string, string>();
            blackberryJam.Add("Pearl Jam", "Black");
            allSongs.Add(blackberryJam);
            Console.WriteLine("-----------------");
            Console.WriteLine("These are all the songs:");
            foreach (Dictionary<string, string> song in allSongs)
            {
                foreach (KeyValuePair<string, string> songData in song)
                {
                    if (songData.Key != "Nickleback")
                    {
                        goodSongs.Add(song);
                    }
                Console.WriteLine($"{songData.Value} by {songData.Key}");
                }
            }
            Console.WriteLine("-----------------");
            Console.WriteLine("These are the good ones:");
             foreach (Dictionary<string, string> song in goodSongs)
            {
                 foreach (KeyValuePair<string, string> songData in song)
                {
                    Console.WriteLine($"{songData.Value} by {songData.Key}");
        }
    }
}
    }
}
