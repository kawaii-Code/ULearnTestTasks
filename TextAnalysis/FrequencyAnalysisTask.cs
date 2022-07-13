using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        private static Dictionary<(string, string), int> dataStorage = 
            new Dictionary<(string, string), int>();

        private static (string, string) InitNgramObject( string[] words )
        {
            if (words.Length < 2) return (null, null);

            (string, string) result = (string.Empty, string.Empty);   
            result.Item1 = words[ 0 ];
            for (int i = 1 ; i < words.Length - 1 ; i++)
                result.Item1 += ' ' + words[ i ];
            result.Item2 = words[ words.Length - 1 ];

            return result;
        }
        private static void AccountNgram(params string[] words)
        {
            var nGram = InitNgramObject(words);
            if (nGram == (null, null)) return;
            IncrementOn(nGram);
        }
        private static void IncrementOn((string, string) key)
        {
            if (!dataStorage.ContainsKey(key))
                dataStorage.Add(key, 1);
            else
                dataStorage[ key ]++;
        }

        private static void AccountNgrams(List<List<string>> text)
        {
            dataStorage.Clear();
            for(int i = 0 ; i < text.Count ; i++)
            {
                if (text[ i ].Count <= 1) continue;
                AccountNgram(text[ i ][ 0 ], text[ i ][ 1 ]);
                for (int j = 2 ; j < text[i].Count ; j++)
                {
                    AccountNgram(text[ i ][ j - 1 ], text[ i ][ j ]);
                    AccountNgram(text[ i ][ j - 2 ], text[ i ][ j - 1 ], text[ i ][ j ]);
                }
            }
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            AccountNgrams(text);

            var temp = new Dictionary<string, (string, int)>();
            foreach ((string, string) data in dataStorage.Keys)            
                if (!temp.ContainsKey(data.Item1))
                    temp.Add(data.Item1, ( data.Item2, dataStorage[ data ] ));
                else                 
                    if ( dataStorage[ data ] > temp[ data.Item1 ].Item2 || 
                        (dataStorage[data] == temp[data.Item1].Item2 && 
                        string.CompareOrdinal(data.Item2, temp[data.Item1].Item1) < 0) )
                        temp[ data.Item1 ] = (data.Item2, dataStorage[ data ]);

            foreach (string key in temp.Keys)
                result.Add(key, temp[ key ].Item1);
            return result;
        }
    }
}