using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace Tutorial12
{
    // This tutorial shows you what function composition is and how it works. 
    // See: https://github.com/louthy/language-ext/wiki/Thinking-Functionally:-Function-composition
    class Program
    {
        static void Main(string[] args)
        {
            var encrypted = EncryptWord("Stuart");

            Console.WriteLine($"The encrypted word is '{encrypted}'");

        }

        // This function accepts a string, but we've now restricted our return value to a monad.
        // so we need to 'compose' the monad returning function, to another function that allows it to be passed into this one
        public static string EncryptWord(string word)
        {
            // First, reverse the characters
            var reversed = ReverseCharacters(word);

            // But SwapFirstAndLastChar() doesn't expect out newly monadicly modified version of ReverseCharacters!
            // The following now will not work...
            //var swapped = SwapFirstAndLastChar(reversed);
            
            // So to ensure that we can still use the existing Code ie SwapFirstAndLastChar, we'll need to 'compose' an adapter function, which will 
            // basically convert the now incompatible input from ReverseCharacters ie a Box<> monad into a string which it can use as was expected in the commented out call above.

            var SwapFirstAndLastCharAdapter = Compose<Box<string>, string, string>(inputWord => SwapFirstAndLastChar(inputWord), converterFunction: box => box.Extract);

            // So now We can 'replace' calls to SwapFirstAndLastChar() with calls to its 'replaced' adapter function, which is composed of the original function (so you dont really replace it, its still there)
            // Pass in the now monadic result of the modified ReverseCharacters function to the adapter we just composed and it will call our original SwapFirstAndLastChar() for use
            // after it turns the moandic input into the orignal form that SwapFirstAndLastChar expected.

            var swapped = SwapFirstAndLastCharAdapter(reversed);

            return swapped;
        }

        /// <summary>
        /// This is the new Jazzed up function which now returns a Monad. 
        /// </summary>
        /// <param name="word"></param>
        /// <remarks>But how do/will the existing functions that expect the previous un-monadic form of the function? We'll create an adapter by composing one</remarks>
        /// <returns></returns>
        public static Box<string> ReverseCharacters(string word)
        {
            var charArray = word.ToCharArray();
            Array.Reverse(charArray);
            return new Box<string>(new string(charArray));
        }

        /// <summary>
        /// This is the existing function that we don't want to change
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string SwapFirstAndLastChar(string word)
        {
            var words = word.ToCharArray();
            var saveFirstChar = words.First();
            words[0] = words.Last();
            words[words.Length - 1] = saveFirstChar;
            return new string(words);
        }

        /// <summary>
        /// Makes a new function ie composes one, from two other functions
        /// </summary>
        /// <typeparam name="TA">Input type of the first functions input</typeparam>
        /// <typeparam name="TB">The Transformed output type of the first function</typeparam>
        /// <typeparam name="TC">The transformed output type of the result of the second function using the first's output</typeparam>
        /// <param name="converterFunction">the function that will run on the output of the first function</param>
        /// <param name="originalFunction">the first function</param>
        /// <returns>A new function ie a delegate</returns>
        public static Func<TA, TC> Compose<TA, TB, TC>(Func<TB, TC> originalFunction, Func<TA, TB> converterFunction)
            => input => originalFunction(converterFunction(input));
    }      
}
