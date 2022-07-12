using System.Collections.Generic;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        private static bool TryToUpper( char[] word, int index )
        {
            char old = word[ index ];
            word[ index ] = char.ToUpper(word[ index ]);
            return old != word[ index ];
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex >= word.Length)
            {                
                result.Add(new string(word));
                return;
            }
            
            word[startIndex] = char.ToLower(word[ startIndex ]);
            AlternateCharCases(word, startIndex + 1, result);

            if ( char.IsLetter(word[startIndex]) &&
                 TryToUpper(word, startIndex) )             
                AlternateCharCases(word, startIndex + 1, result);  
        }
    }
}