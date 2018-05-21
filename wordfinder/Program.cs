using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wordfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<string, List<LetterCount>> LetterCountsForWords = new ConcurrentDictionary<string, List<LetterCount>>();

            string WordsFilePath = @"/Users/balazskerper/Projects/test_data/words_alpha.txt";

            List<string> words = File.ReadAllLines(WordsFilePath).ToList();

            Parallel.ForEach(words, (word) =>
            {
                LetterCountsForWords.AddOrUpdate(word, GetLetterCountsForWord(word), (Key, Value) => Value);
            });

            Console.WriteLine(words.Count);
            Console.WriteLine(LetterCountsForWords.Count);
        }
        public static List<LetterCount> GetLetterCountsForWord(string word)
        {
            List<LetterCount> letterCounts = new List<LetterCount>();
            //TODO: create logic for getting LetterCounts
            return letterCounts;
        }
    }

    struct LetterCount
    {
        char Char { get; set; }
        byte Count { get; set; }
    }


}
