using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        private static bool CharacterIsAPartOfWord(char character)
        {
            return char.IsLetter(character) || character == '\'';
        }

        private static void AddWord(List<string> wordList, StringBuilder wordBuilder)
        {
            if (wordBuilder.Length == 0) return;
            wordList.Add(wordBuilder.ToString().ToLower());
            wordBuilder.Clear();
        }

        private static List<string> ReadWords(string sentence)
        {
            List<string> words = new List<string>();
            StringBuilder wordBuilder = new StringBuilder();

            foreach (char character in sentence)
            {
                if (CharacterIsAPartOfWord(character))
                    wordBuilder.Append(character);
                else 
                    AddWord(words, wordBuilder);       
            }
            AddWord(words, wordBuilder);

            return words;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            foreach (string sentence in text.Split('.', '!', '?',';',':','(',')'))
            {
                List<string> words = ReadWords(sentence);
                if(words.Count > 0)
                    sentencesList.Add(words);
            }

            return sentencesList;
        }
    }
}