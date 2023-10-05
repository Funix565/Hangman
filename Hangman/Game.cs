using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Game
    {
        // TODO: Field initialization or in constructor?
        private string _word = "";
        private char[] _guessing = Array.Empty<char>();
        private byte _errorCount = 0;
        private char[] _usedLetters = new char[7];
        private bool _isVictory = false;
        private HangmanDraw _draw = new HangmanDraw();

        // TODO: Should I print smth in constructor?
        public void Start()
        {
            this._draw.DrawIntro();

            while (true)
            {
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. Exit");
                CatchUserInput();
            }
        }

        private void CatchUserInput()
        {
            Console.Write("Input: ");
            string? userInput = Console.ReadLine();
            if (userInput == "1")
            {
                PlayGame();
            }
            else if (userInput == "2")
            {
                Console.WriteLine("Bye");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Use only numbers 1 or 2. Restart and try again");
                Environment.Exit(0);
            }
        }

        private void PlayGame()
        {
            this._isVictory = false;
            this._word = LoadWordFromFile();
            this._guessing = ListUnderscores().ToCharArray();

            Console.WriteLine("We think of a word. Try to guess it by suggesting letters");

            while (this._errorCount != 6 && !this._isVictory)
            {
                PrintRound();
                UserGuess();
            }

            if (this._isVictory == false)
            {
                PrintRound();
                Console.WriteLine(this._draw.DrawFail());
                Console.WriteLine();
            }
        }

        private string LoadWordFromFile()
        {
            return "bitcoin";
        }

        private string ListUnderscores()
        {
            char[] chars = this._word.ToCharArray();

            for (int i = 0, n = chars.Length; i < n; ++i)
            {
                chars[i] = '-';
            }

            return new string(chars);
        }

        private void PrintRound()
        {
            Console.WriteLine($"Word: {new string(this._guessing)}");
            Console.WriteLine($"Errors ({this._errorCount}): {new string(this._usedLetters)}");
            Console.WriteLine(this._draw.SwitchErrors(this._errorCount));
            Console.WriteLine();
        }

        private void UserGuess()
        {
            Console.Write("Your suggestion: ");
            string? input = Console.ReadLine();

            if (input == null || input.Length != 1 || !Char.IsLetter(Convert.ToChar(input)))
            {
                Console.WriteLine("Provide only one letter at a time. Restart and try again.");
                Environment.Exit(0);
            }

            CustomContains(Convert.ToChar(input));

            if (this._word == new string(this._guessing))
            {
                this._isVictory = true;
                Console.WriteLine(this._draw.DrawVictory());
                Console.WriteLine();
            }
        }

        private void CustomContains(char input)
        {
            bool isContain = false;
            for (int i = 0, n = this._word.Length; i < n; ++i)
            {
                if (this._word[i] == input)
                {
                    isContain = true;
                    this._guessing[i] = input;
                }
            }

            if (!isContain)
            {
                this._usedLetters[this._errorCount++] = input;
            }
        }
    }
}
