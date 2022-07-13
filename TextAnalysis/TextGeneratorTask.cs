using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        private static string GetTwoLastWords(List<string> words)
        {
            return words[words.Count - 2] + " " + words[words.Count - 1];
        }

        private static List<string> GenerateWordList(
            Dictionary<string, string> nextWords, 
            string phraseBeginning
            , int wordsCount)
        {
            List<string> words = phraseBeginning.Split(' ').ToList();
            for(int i = 0 ; i < wordsCount ; i++)            
                if (words.Count > 1 && nextWords.ContainsKey(GetTwoLastWords(words)))
                    words.Add(nextWords[ GetTwoLastWords(words) ]);
                else if (nextWords.ContainsKey(words[ words.Count - 1 ]))
                    words.Add(nextWords[ words[ words.Count - 1 ] ]);
            return words;
        }

        private static string ParseListToString(List<string> words)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(words[ 0 ]);
            for (int i = 1 ; i < words.Count ; i++)
            {
                builder.Append(" ");
                builder.Append(words[i]);
            }
            return builder.ToString();
        }

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            List<string> words = GenerateWordList(nextWords, phraseBeginning, wordsCount);
            return ParseListToString(words);
        }
    }
}