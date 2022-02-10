using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL
{
    public class HangMan
    {
        private Words  words;
        private string word;
        private string userWord;
        private int guesses;
        private int lives;

        public HangMan()
        {
            words   = new Words();
            guesses = 0;
            lives   = 6;
        }

        public HangMan(int lives) : this()
        {
            this.lives = lives;
        }

        public void NewGame()
        {
            guesses = 0;
            GetRandomWord();
        }

        public void GetRandomWord()
        {
            word     = words.GetWord();
            userWord = userWord.PadLeft(word.Length, '_');
        }

        public string GetWord()
        {
            return word;
        }

        public List<int> CheckLetter(char c)
        {
            List<int> index = new List<int>();

            guesses++;

            for (int i = 0; i < word.Length; i++)
                if (c == word[i])
                {
                    index.Add(i);
                    userWord = userWord.Substring(0, i) + c + userWord.Substring(i + 1, userWord.Length - i - 1);
                }

            return index;
        }

        public bool HasLives()
        {
            return lives != guesses;
        }

        public bool EndGame()
        {
            return word == userWord;
        }
    }
}