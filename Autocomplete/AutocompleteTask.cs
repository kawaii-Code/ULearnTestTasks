using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            List<string> result = new List<string>();
            int leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int prefixedCount = GetCountByPrefix(phrases, prefix);

            for (int i = 0 ; i < count && i < prefixedCount ; i++)
                result.Add(phrases[ leftBorder + 1 + i ]);
            return result.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            int leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return rightBorder - leftBorder - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        private Phrases testPhrases = new Phrases(
                new[] { "play", "write" },
                new[] { "quickly", "hastely" },
                new[] { "code", "phrase" }
            );

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var emptyPhrases = new Phrases(new string[0], new string[0], new string[0]);
            var actualTopWords = AutocompleteTask.GetTopByPrefix(emptyPhrases, "", 10);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var expectedCount = 8;
            var actualCount = AutocompleteTask.GetCountByPrefix(testPhrases, "");
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
