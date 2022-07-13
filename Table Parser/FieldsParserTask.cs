using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using System.Text;

namespace TableParser
{    
    public class FieldParserTaskTests
    {
        [TestCase("", new string[0])]

        #region FieldsTests
        [TestCase("a", new[] { "a" })]
        [TestCase(" a  ", new[] { "a" })]
        [TestCase("b a", new[] { "b", "a" })]
        [TestCase("b    a", new[] { "b", "a" })]
        [TestCase("a b c", new[] { "a", "b", "c" })]
        [TestCase("ba", new[] { "ba" })]
        #endregion
  
        #region QuoteTest
        [TestCase(@"'a'", new[] { "a" })]
        [TestCase(@"'a", new[] { "a" })]
        [TestCase(@"' '", new[] { " " })]
        [TestCase(@"' ", new[] { " " })]
        [TestCase(@"a''", new[] { "a", "" })]
        [TestCase(@"b'a", new[] { "b", "a" })]
        [TestCase(@"""b"" ""a"" ", new[] { "b", "a" })]
        [TestCase(@"""b"" c ""a""", new[] { "b", "c", "a" })]
        [TestCase(@"'' a", new[] { "", "a" })]
        #endregion

        #region SlashTests
        [TestCase(@"\", new[] { @"\" })]
        [TestCase(@"\\", new[] { @"\\" })]
        [TestCase(@"'\\'", new[] { @"\" })]
        [TestCase(@"""\""""", new[] { @"""" })]
        [TestCase(@"'\''", new[] { @"'" })]
        [TestCase(@"'\""'", new[] { @"""" })]
        [TestCase(@"""\'""", new[] { @"'" })]
        #endregion

        public static void RunTests(string input, string[] expectedOutput) => Test(input, expectedOutput);   

        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
    }

    public class FieldsParserTask
    {
        private static readonly char[] quotes = new[] { '\'', '"' }; 

        public static List<Token> ParseLine(string line)
        {
            List<Token> result = new List<Token>();

            for(int i = NotSpaceIndex(line, 0) ; i < line.Length ; i = NotSpaceIndex(line, i))
            {
                Token currentField;

                if (quotes.Contains(line[i]))                
                    currentField = ReadQuotedField(line, i);                
                else                
                    currentField = ReadField(line, i);                   
                
                i += currentField.Length;
                result.Add(currentField);
            }

            return result;
        }
        
        private static int NotSpaceIndex(string line, int currentIndex)
        {
            while (currentIndex < line.Length && line[currentIndex] == ' ')
                currentIndex++;
            return currentIndex;
        }

        private static Token ReadField(string line, int startIndex)
        {
            StringBuilder builder = new StringBuilder();
            int currentIndex = startIndex;

            while (IsPartOfAFieldAt(line, currentIndex))
                builder.Append(line[ currentIndex++ ]);
            return new Token(builder.ToString(), startIndex, currentIndex - startIndex);
        }

        private static bool IsPartOfAFieldAt(string line, int currentIndex)
        {
            return currentIndex < line.Length 
                && line[ currentIndex ] != ' ' 
                && !quotes.Contains(line[ currentIndex ]);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}