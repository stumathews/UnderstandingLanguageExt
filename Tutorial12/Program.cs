using System;
using System.Collections.Generic;
using System.Linq;
namespace Tutorial12
{
    // This tutorial shows you what function composition is and how it works. 
    // See: https://github.com/louthy/language-ext/wiki/Thinking-Functionally:-Function-composition
    class Program
    {
        static void Main(string[] args)
        {

            // Make a new function by composing it of two. ie the output of the one will be the input of the second, by the new composed function will do that behind the scenes
            Box<string> secretWordInABox = new Box<string>("hello");

                                        // TA, TB, TC
            var padAndUpperCase = Compose<string, string, string>(PadRightWith, UpperCaseWord);
            

            var boxedResult = secretWordInABox.Map(secret => padAndUpperCase(secret));

            Console.WriteLine($"The result of the new function is: {boxedResult.Extract}");

        }

        public static string PadRightWith(string word) => word.PadRight(12, '#');
        public static string UpperCaseWord(string word) => word.ToUpper();

        /// <summary>
        /// Makes a new function ie composes one, from two other functions
        /// </summary>
        /// <typeparam name="TA">Input type of the first functions input</typeparam>
        /// <typeparam name="TB">The Transformed output type of the first function</typeparam>
        /// <typeparam name="TC">The transformed output type of the result of the second function using the first's output</typeparam>
        /// <param name="secondFunction">the function that will run on the output of the first function</param>
        /// <param name="firstFunction">the first function</param>
        /// <returns>A new function ie a delegate</returns>
        public static Func<TA, TC> Compose<TA, TB, TC>(Func<TA, TB> secondFunction, Func<TB, TC> firstFunction)
            => input => firstFunction(secondFunction(input));
    }      
}
