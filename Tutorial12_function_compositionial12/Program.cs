using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace Tutorial12
{
    // This tutorial shows you what function composition is and how it works. 
    // See: https://github.com/louthy/language-ext/wiki/Thinking-Functionally:-Function-composition
    // Scenario: You have a program that has existing functions. You change one of those functions to now return a Monad. You need to ensure that you program still works ie 
    // existing functions can still use your Monad returning function, without having to further change any of the existing code that didn't expect a monad.
    // So we'll compose a new function that will take the monad, turn it into a string (which the existing functions expect) and thus you can use those existing functions.
    class Program
    {
        // This is our main function
        static void Main(string[] args)
        {

            var encrypted = EncryptWord("Stuart");

            Console.WriteLine($"The encrypted word is '{encrypted}'");
            
        }

        
        public static string EncryptWord(string word)
        {
            // First, reverse the characters
            var reversedBox = ReverseCharacters(word);

            // But SwapFirstAndLastChar() doesn't expect our newly monadic version of ReverseCharacters! It still expects a string, not a monad.
            // So, the following now will not work...
            //var swapped = SwapFirstAndLastChar(reversed); // because reversed now returns a monad and SwapFirstAndLastChar expects it to be a string. 
            
            // So to ensure that we can still use the existing Code ie SwapFirstAndLastChar, we'll need to 'compose' an adapter function, which will 
            // basically convert the now incompatible input from ReverseCharacters ie a Box<> monad into a string which it can use as was expected in the commented out call above.

            var swapAdapter = Compose<Box<string>, string, string>(inputWord => SwapFirstAndLastChar(inputWord), converterFunction: box => box.Extract as string);

            // So now We can 'replace' calls to SwapFirstAndLastChar() with calls to its 'replaced' adapter function, which is composed of the original function (so you dont really replace it, its still there)
            // Pass in the now monadic result of the modified ReverseCharacters function to the adapter we just composed and it will call our original SwapFirstAndLastChar() for use
            // after it turns the moandic input into the orignal form that SwapFirstAndLastChar expected.

            string swapped = swapAdapter(reversedBox/*takes in the box and extracts the string out of it so it can be passed to the original function that needs a string*/);

            // Alternatively you can use the LanguageExt function compose() to do the same thing - I've not included langaugeExt into these early tutorials yet.

            return swapped;
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

        /// <summary>
        /// This function used to return a string, but we've decided that it should return a Monad. Now in order to use this function in places that previously expected it to be a string,
        /// we'll need to create an adapter for it. And we'll do this be composing a new function which takes this function's output, now a monad, and returns it as a string...see above.
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
        /// This is the existing function that we don't want to change and it requires a string, still
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
    }      
}

/*
 * Note: It can be considered that the pipelineing effect produced by chaining Map().Map().Map() is also function composition,
 * where you've composed one action as a composition of transformation
 */
