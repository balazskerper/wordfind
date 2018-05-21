using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace wordfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            ConcurrentDictionary<string, List<LetterCount>> LetterCountsForWords = new ConcurrentDictionary<string, List<LetterCount>>();

            string WordsFilePath = @"/Users/balazskerper/Projects/test_data/words_alpha.txt";
            char[] Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();

            List<string> words = File.ReadAllLines(WordsFilePath).ToList();

            Parallel.ForEach(words, (word) =>
            {
                LetterCountsForWords.AddOrUpdate(word, GetLetterCountsForWord(word, Characters), (OldKey, OldValue) => OldValue);
            });

            stopper.Stop();
            Console.WriteLine($"Processing took {stopper.Elapsed.TotalSeconds}s");
        }

        public static List<LetterCount> GetLetterCountsForWord(string word, char[] characters)
        {
            List<LetterCount> letterCounts = new List<LetterCount>();
            foreach(char character in characters)
            {
                if (word.Any(a => a.Equals(character)))
                {
                    letterCounts.Add(new LetterCount(character, (byte)word.Count(c => c.Equals(character))));
                }
            }
            return letterCounts;
        }
    }

    public struct LetterCount
    {
        public char Char { get; set; }
        public byte Count { get; set; }

        public LetterCount(char c, byte count)
        {
            Char = c;
            Count = count;
        }
    }


}
