using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial28
{
    class Program
    {
        static void Main(string[] args)
        {
            // An optional type can hold an integer or a none
            Option<int> optionalInteger = 34;
            optionalInteger = Option<int>.Some(34); // save as above.
            optionalInteger = Option<int>.None;
            // optionalInteger = null; Options effectively eliminate nulls in your code.

            var resultA = DivideBy(25, 5);
            var resultB = DivideBy1(25, 5);

            Console.WriteLine($"The result A is '{resultA}' and B is '{resultB}'");
        }

        static int DivideBy(int thisNumber, int dividedByThatNumber)
        {
            return thisNumber / dividedByThatNumber;
        }

        static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber)
        {
            return thisNumber / dividedByThatNumber;
        }

    }      
}
