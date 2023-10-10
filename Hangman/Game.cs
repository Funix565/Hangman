using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Game
    {
        private const string WORDS_FILE_NAME = "words.txt";
        private const byte MAX_ERRORS = 7;

        // Finally, I realized when to use properties and when to use fields.
        // Properties are good for mutable members because we can debug access by placing breakpoint on `set`.
        // And of course we can have additional logic there.
        // Fields are good for constant data.

        // Field initialization or in constructor?
        // -- Doesn't matter: https://stackoverflow.com/a/24558
        // -- Use constructor if constructor has parameters.
        private string Word { get; set; } = "";

        // I think arrays are light-weight and ok here. I have fixed size and can track position.
        // Collections are heavy-weight and look unnecessary.
        private char[] Guessing { get; set; } = Array.Empty<char>();
        private byte ErrorCount { get; set; } = 0;
        private bool IsVictory { get; set; } = false;

        private readonly char[] _usedLetters = new char[MAX_ERRORS];
        private readonly HangmanDraw _draw = new HangmanDraw();

        public void Start()
        {
            this._draw.DrawIntro();

            while (true)
            {
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. Exit");
                HandleUserInput();
            }
        }

        private void HandleUserInput()
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
            InitGameValues();

            Console.WriteLine("We think of a word. Try to guess it by suggesting letters");

            while (this.ErrorCount != 6 && !this.IsVictory)
            {
                PrintRound();
                UserGuess();
            }

            if (!this.IsVictory)
            {
                PrintRound();
                Console.WriteLine(this._draw.DrawFail());
                Console.WriteLine($"Answer: {this.Word}");
                Console.WriteLine();
            }
        }

        private void InitGameValues()
        {
            this.IsVictory = false;
            this.Word = LoadWordFromFile();
            this.Guessing = PrepareMinus();
            Array.Clear(this._usedLetters);
            this.ErrorCount = 0;
        }

        private string LoadWordFromFile()
        {
            try
            {
                CustomFileReader cfr = new CustomFileReader(WORDS_FILE_NAME);
                return cfr.TakeWord();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Ensure that words.txt file exists in the current execution directory.");
                Environment.Exit(0);
            }

            return "hangman";
        }

        private char[] PrepareMinus()
        {
            char[] chars = this.Word.ToCharArray();

            for (int i = 0, n = chars.Length; i < n; ++i)
            {
                chars[i] = '-';
            }

            return new string(chars).ToCharArray();
        }

        private void PrintRound()
        {
            Console.WriteLine($"Word: {new string(this.Guessing)}");
            Console.WriteLine($"Errors ({this.ErrorCount}): {new string(this._usedLetters)}");
            Console.WriteLine(this._draw.SwitchErrors(this.ErrorCount));
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

            CustomContains(Convert.ToChar(input.ToLower()));

            if (this.Word == new string(this.Guessing))
            {
                this.IsVictory = true;

                InitGameValues();

                Console.WriteLine(this._draw.DrawVictory());
                Console.WriteLine();
            }
        }

        private void CustomContains(char input)
        {
            bool isContain = false;
            for (int i = 0, n = this.Word.Length; i < n; ++i)
            {
                if (this.Word[i] == input)
                {
                    isContain = true;
                    this.Guessing[i] = input;
                }
            }

            if (!isContain && !Array.Exists(this._usedLetters, l => l == input))
            {
                this._usedLetters[this.ErrorCount++] = input;
            }
        }
    }
}
