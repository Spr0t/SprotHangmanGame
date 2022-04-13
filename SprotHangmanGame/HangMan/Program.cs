using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprotHangmanGame
{
    public class Program
    {
        static void Main(string[] args)
        {
           while (true)
            {
                HangManGame game = new HangManGame();
                string word = game.Setword();
                Console.WriteLine($"Guess the word!\nThe word is consist of {word.Length} - letters");
                while (game.gamestatus == HangManGame.GameStatus.Gameinprogress)
                {
                    Console.WriteLine("Pick a letter");
                    char s = Console.ReadLine().ToCharArray()[0];
                    string curState = game.GuessLetter(s);
                    Console.WriteLine(curState);

                    Console.WriteLine($"Remaining tries = {game.RemainingTies}\nTriedLetters: {game.TriedLetters}");
                }

                if (game.gamestatus == HangManGame.GameStatus.Lost)
                {
                    Console.WriteLine($"You`re hanged...\nThe word was: {game.Word}");

                }
                else if (game.gamestatus == HangManGame.GameStatus.Won)
                {
                    Console.WriteLine("You won!");
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
