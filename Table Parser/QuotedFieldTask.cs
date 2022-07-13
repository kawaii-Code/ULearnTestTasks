using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("'a", 0, "a", 2)]
        [TestCase("'a b'", 0, "a b", 5)]
        [TestCase(@"'\\'", 0, @"\", 4)]
        [TestCase(@"'\''", 0, @"'", 4)]
        [TestCase(@"b ""a""", 2, "a", 3)]        
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            StringBuilder builder = new StringBuilder();
            char quote = line[ startIndex ];

            int currentIndex = startIndex;
            while (currentIndex + 1 < line.Length &&  line[++currentIndex] != quote)
            {
                if (line[ currentIndex ] == '\\')
                    builder.Append(line[ ++currentIndex ]);
                else
                    builder.Append(line[ currentIndex ]);
            }
            return new Token(builder.ToString(), startIndex, currentIndex + 1 - startIndex); 
        }
    }
}