using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GuessWord
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(" ************************ ");
            Console.WriteLine(" ************************ ");
            Console.WriteLine(" **  Guess the word!   ** ");
            Console.WriteLine(" ************************ ");
            Console.WriteLine(" ************************ ");
            Console.WriteLine();

            Console.WriteLine("Select level:");
            Console.WriteLine("1: words with less then 5 letters.");
            Console.WriteLine("2: words with more then 5 letters.");
            Console.WriteLine("3: words tricky to spell.");

            var words = GetWords();

            var randomList = new Random();
            var randomWord = randomList.Next(0, 7);
            string secretWord = words[randomWord].ToUpper();
            var guess = new char[secretWord.Length];
            var guesses = new List<char>();

            //display the word with *
            for (int i = 0; i < secretWord.Length; i++)
                guess[i] = '*';

            int life = 10;
            int lettersShowing = 0;
            bool winning = false;

            //start the game
            Console.WriteLine();
            Console.Write("The secret word is: ");
            Console.WriteLine(guess);
            Console.WriteLine($"You have {life} guesses, shoot.");

            while (!winning && life > 0)
            {
                Console.WriteLine();
                char playerGuess = SetGuess(guesses);
                // if the letter does not exist in the word
                bool guessWasCorrect = false;

                for (int j = 0; j < secretWord.Length; j++)
                {
                    if (playerGuess == secretWord[j])
                    {
                        guess[j] = secretWord[j];
                        lettersShowing++;
                        //the letter exist in the word
                        guessWasCorrect = true;
                    }
                }

                if (!guessWasCorrect)
                {
                    life -= 1;
                }

                if (lettersShowing == secretWord.Length)
                {
                    winning = true;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The word was: {secretWord}");
                    Console.WriteLine(" ************************ ");
                    Console.WriteLine(" **  You won, yey!!    ** ");
                    Console.WriteLine(" ************************ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to exit. :)");
                    Console.ReadKey();
                }
                else
                {
                    foreach (var letter in guesses)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{letter}, ");
                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(guess);
                    Console.WriteLine($"You have {life} chances left.");
                }
            }

            if (life == 0)

            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ************************ ");
                Console.WriteLine(" **  Game over :,(     ** ");
                Console.WriteLine(" ************************ ");
                Console.WriteLine($"The word was: {secretWord}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to exit. :)");
                Console.ReadKey();
            }
        }

        private static bool CheckIfValidChar(char value)
        {
            return Regex.IsMatch(value.ToString(), "[a-zåäö]", RegexOptions.IgnoreCase);
        }

        private static char SetGuess(List<char> guesses)
        {
            Console.Write("Please enter a letter: ");

            char input = Console.ReadKey().KeyChar;

            char playerGuess = char.ToUpper(input);
            Console.WriteLine();

            if (guesses.Contains(playerGuess))
            {
                Console.WriteLine();
                Console.WriteLine("That letter was already in list, try again!");
                return SetGuess(guesses);
            }

            var valid = CheckIfValidChar(playerGuess);
            if (!valid)
            {
                Console.WriteLine();
                Console.WriteLine("That was not a letter, try again!");
                return SetGuess(guesses);
            }

            guesses.Add(playerGuess);

            return playerGuess;
        }

        private static List<string> GetWords()
        {

            Console.Write("Please choose level 1, 2 or 3: ");

            var level = Console.ReadLine();

            if (!int.TryParse(level, out int choice) || choice == 0 || choice > 3)
            {
                return GetWords();
            }

            var levelChoice = (Levels)choice;

            if (levelChoice == Levels.levelOne)
            {
                return new List<string>
            {
                "jeans",
                "alien",
                "table",
                "dance",
                "home",
                "guess",
                "ring"
            };
            }

            if (levelChoice == Levels.levelTwo)
            {
                return new List<string>
            {
                "family",
                "genius",
                "orange",
                "sandwich",
                "rainbow",
                "chicken",
                "meatball"
            };
            }

            if (levelChoice == Levels.levelThree)
            {
                return new List<string>
            {
                "conscious",
                "baroques",
                "seducing",
                "government",
                "hippopotamus",
                "jealousy",
                "mayonnaise"
            };
            }
            return null;
        }
    }
}
