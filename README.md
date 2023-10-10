# Hangman Application

The "Hangman" console game implemented in C#. Description of the game rules on Wikipedia - [link](https://en.wikipedia.org/wiki/Hangman_(game)).

## Application features

1. Application has console menu.
2. User can start a new game or exit the application.
3. A word is randomly selected from "words.txt" file.
4. After each entered letter, console displays the error counter and the current state of the hangman (drawn is ASCII characters).
5. When the game is over, console displays the result (win or lose) and the application returns to state -- start a new game or exit the application.

## Implementation

Application has four classes.
* `Program.cs` -- the main entry point.
* `Game.cs` -- game logic and infinite loop.
* `HangmanDraw` -- ASCII art for Hangman.
* `CustomFileReader.cs` -- file I/O methods.

## Technical Choices
* Application has data validation.
	* Menu accepts only "1" or "2". Don't want to bother with infinit prompt, so just exit on invalid input.
	* Guess input accepts only one character at a turn. No strings or numbers.
* Hitting wrong character multiple times is not considered as error.
* I use arrays instead of collections because I have fixed size and can track position.
* File `words.txt` must exist in the current execution directory. For example, in `...\Hangman\Hangman\bin\Debug\net7.0\words.txt` in case of debug in Visual Studio.

## Idea and Inspiration

Check this Java Backend Learning Course - [link](https://zhukovsd.github.io/java-backend-learning-course/Projects/Hangman/)
