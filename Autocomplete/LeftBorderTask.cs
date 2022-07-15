using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        /// <returns>
        /// Возвращает индекс левой границы.
        /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
        /// Если такой нет, то возвращает -1
        /// </returns>
        /// <remarks>
        /// Функция должна быть рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {             
            if (right - left <= 1)   
                return left;
            
            int middle = left + (right - left) / 2;
            if (string.Compare(phrases[middle], prefix, StringComparison.OrdinalIgnoreCase) <= 0 &&
                !phrases[middle].StartsWith(prefix))
                return GetLeftBorderIndex(phrases, prefix, middle, right);
            else
                return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}
