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
            var resultA = DivideBy(25, 5);
            var resultB = DivideBy1(25, 0);

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

        static int DivideBy(int thisNumber, int dividedByThatNumber)
        {
            if (dividedByThatNumber == 0)
                return 0;
            return thisNumber / dividedByThatNumber;
        }

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
           // I can assume thats its valid, becasue bind will run a tranformation function on the valid input
           Option<int> t = input.Map(validInput => validInput + 5); // if its invalid the Validation phase of the map() function will return a None ie an invalid input and so the
           
           // the result of the Map will be an option<T> will a None or invalid input in it. Remember a Map always returns a Monad, in this case Option<T> and it automatically lifts it for you
           return t;
        }
    }      
}
