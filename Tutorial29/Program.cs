using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial29
{
    class Program
    {
        static void Main(string[] args)
        {
            int         resultA = DivideBy(25, 5);
            Option<int> resultB = DivideBy1(25, 0);

            Console.WriteLine($"The result A is '{resultA}' and B is '{resultB}'");

            /*
             * Discussion:
             * DivideBy1 will return an Option<T> and it knows what an invalid result is - its encapsulated within the option<T> 
             * DivideBy will return an int, but the caller needs to know that 0 is an invalid result.
             */

            var result1 = Add5ToIt(resultA);
            var result2 = Add5ToIt(resultB);

            Console.WriteLine($"The result A is '{result1}' and B is '{result2}'");
        }


        /// <summary>
        /// Normal function, not using optional parameters
        /// </summary>
        /// <param name="thisNumber"></param>
        /// <param name="dividedByThatNumber"></param>
        /// <returns>integer</returns>
        static int DivideBy(int thisNumber, int dividedByThatNumber)
        {
            if (dividedByThatNumber == 0)
                return 0;
            return thisNumber / dividedByThatNumber;
        }

        /// <summary>
        /// Function returns Monad, one construct that represents both failure and success.
        /// </summary>
        /// <returns>Option of an integer</returns>
        static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber)
        {
            if (dividedByThatNumber == 0)
            {
                Option<int> t = Option<int>.None;
                return t;
            }

            return thisNumber / dividedByThatNumber;
        }

        static int Add5ToIt(int input)
        {
            // Whoops, I've forgotten to check if input is valid.
            return input + 5;
            // And even if i did, I've have to know what an invalid input is - its 0 in this case.
        }

        static Option<int> Add5ToIt(Option<int> input)
        {
           // I can assume that its valid, because Map will run a transformation function on the valid input
           Option<int> t = input.Map(validInput => validInput + 5); // if its invalid the Validation phase of the map() function will return a None ie an invalid input and so the
           
           // the result of the Map will be an option<T> of None or invalid input in it.
           // Remember a Map always returns a Monad, in this case Option<T> and it automatically lifts it for you by Map()
           return t;
        }
    }      
}
