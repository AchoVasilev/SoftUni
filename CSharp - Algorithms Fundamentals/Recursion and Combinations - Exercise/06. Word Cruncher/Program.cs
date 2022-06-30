using System;
using System.Collections.Generic;

namespace WordCruncher
{
    class Program
    {
        private static Dictionary<int, List<string>> wordsByIndex;
        private static Dictionary<string, int> wordsCount;
        private static LinkedList<string> usedWords;

        public static void Main()
        {
            wordsByIndex = new Dictionary<int, List<string>>();
            wordsCount = new Dictionary<string, int>();
            usedWords = new LinkedList<string>();

            var words = Console.ReadLine().Split(", ");
            var target = Console.ReadLine();

            foreach (var word in words)
            {
                var index = target.IndexOf(word);

                if (index == -1)
                {
                    continue;
                }

                if (wordsCount.ContainsKey(word))
                {
                    wordsCount[word]++;
                    continue;
                }

                wordsCount[word] = 1;

                while (index != -1)
                {
                    if (!wordsByIndex.ContainsKey(index))
                    {
                        wordsByIndex[index] = new List<string>();
                    }

                    wordsByIndex[index].Add(word);

                    index = target.IndexOf(word, index + 1);
                }
            }

            GenerateSolutions(0, target);
        }

        private static void GenerateSolutions(int index, string target)
        {
            if (index == target.Length)
            {
                Console.WriteLine(string.Join(" ", usedWords));
                return;
            }

            if (wordsByIndex.ContainsKey(index) == false)
            {
                return;
            }

            foreach (var word in wordsByIndex[index])
            {
                if (wordsCount[word] == 0)
                {
                    continue;
                }

                wordsCount[word]--;
                usedWords.AddLast(word);

                GenerateSolutions(index + word.Length, target);

                wordsCount[word]++;
                usedWords.RemoveLast();
            }
        }
    }
}
