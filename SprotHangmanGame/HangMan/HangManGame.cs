using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprotHangmanGame
{
    public class HangManGame
    {
        public enum GameStatus
        {
            Gameisunstarted,
            Gameisunfinished,
            Gameinprogress,
            Won,
            Lost
        }
        private readonly int AllowMisses;

        private int TriesCounter { get; set; }

        public int RemainingTies
        {
            get
            {
                return AllowMisses - TriesCounter;
            }
        }
        public GameStatus gamestatus { get; set; } = GameStatus.Gameisunstarted;
        public string Word { get; private set; }

        public bool[] openindexes { get; private set; }

        private string triedLetters;
        public string TriedLetters
        {
            get
            {
                var chars = triedLetters.ToCharArray();
                Array.Sort(chars);
                return new string(chars);
            }
        }
        public HangManGame(int AllowMisses = 6)
        {
            this.AllowMisses = AllowMisses;
            TriesCounter = 0;
        }

        public string Setword()
        {
            string[] words = File.ReadAllLines("C:\\Users\\Dell\\source\\repos\\SprotHangmanGame\\SprotHangmanGame\\HangMan\\WordsStockRus.txt");
            Random r = new Random();
            int randomindex = r.Next(words.Length - 2);
            Word = words[randomindex];

            gamestatus = GameStatus.Gameinprogress;

            openindexes = new bool[Word.Length];

            return Word;
        }

        public string GuessLetter(char letter)
        {
            if (TriesCounter == AllowMisses)
            {
                throw new InvalidOperationException($"Exceeded the max misses number: {AllowMisses}");
            }

            if (gamestatus != GameStatus.Gameinprogress)
            {
                throw new InvalidOperationException($"Inaproppriate status of the game {gamestatus}");
            }

            bool openAny = false;
            string result = string.Empty;
            
            for (int i = 0; i < Word.Length; i++)
            {


                if (Word[i] == letter)
                {
                    openindexes[i] = true;
                    openAny = true;
                }

                if (openindexes[i])
                {
                    result +=Word[i];
                }
                else
                {
                    result += "-";
                }
            }
            if (!openAny)
            {
                TriesCounter++;
            }

            triedLetters += letter;


            if (isWin())
            {
                gamestatus = GameStatus.Won;

            }
            else if (TriesCounter == AllowMisses)
            {
                gamestatus = GameStatus.Lost;
            }
            return result;
        }

        private bool isWin()
        {
            foreach (var cur in openindexes)
            {
                if (cur == false)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
