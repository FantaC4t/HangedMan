using System;
using System.Collections.Generic;

namespace MODEL
{
    public class Words
    {
        private List<string> wordList;
        private Random       rnd;

        public Words()
        {
            wordList = new List<string>();
            rnd = new Random();
        }

        public void AddWord(string word)
        {
            wordList.Add(word);
        }

        public string GetWord()
        {
            return wordList[rnd.Next(wordList.Count)];
        }
        public int GetLength()
        {
            return wordList.Count;
        }
    }
}
