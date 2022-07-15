using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class WordInfo
    {
        public Dictionary<int, List<int>> Positions;

        public WordInfo()
        {
            Positions = new Dictionary<int, List<int>>();
        }
    }

    public class Indexer : IIndexer
    {
        private Dictionary<string, WordInfo> _words = 
            new Dictionary<string, WordInfo>();

        private readonly char[] separators =
        {
            ' ', '.', ',', '!', '?', ':', '-','\r','\n'
        };

        public void Add(int id, string documentText)
        {
            string[] words = documentText.Split(separators);
            int currentPosition = 0;
            foreach(string word in words)
            {
                if(!_words.ContainsKey(word))   
                    _words.Add(word, new WordInfo());
                if (!_words[word].Positions.ContainsKey(id))
                    _words[ word ].Positions.Add(id, new List<int>());
                _words[ word ].Positions[ id ].Add(currentPosition);
                
                currentPosition += word.Length + 1;
            }
        }

        public List<int> GetIds(string word)
        {
            if(!_words.ContainsKey(word))
                return new List<int>();            
            return _words[ word ].Positions.Keys.ToList();
        }

        public List<int> GetPositions(int id, string word)
        {            
            if(!_words.ContainsKey(word) ||
               !_words[word].Positions.ContainsKey(id))
                return new List<int>();            
            return _words[ word ].Positions[id];
        }

        public void Remove(int id)
        {
            foreach(var word in _words.Keys)
            {
                if (_words[word].Positions.ContainsKey(id))
                    _words[word].Positions.Remove(id);
            }
        }
    }
}
