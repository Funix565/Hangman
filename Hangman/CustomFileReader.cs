using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    public class CustomFileReader
    {
        private string FileName { get; }

        private IEnumerable<string> WordList { get; }

        public CustomFileReader(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileName = fileName;

                // Read file in constructor once and store its contents for the whole game.
                WordList = File.ReadLines(FileName);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public string TakeWord()
        {
            // Retrieve an element from an array or collection at random
            // https://learn.microsoft.com/en-us/dotnet/api/system.random?view=net-7.0#retrieve-an-element-from-an-array-or-collection-at-random
            return WordList.ElementAt(new Random().Next(0, WordList.Count()));
        }
    }
}
