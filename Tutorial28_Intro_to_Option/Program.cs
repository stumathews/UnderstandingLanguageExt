using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial28
{
    // Option<T> type effectively removes the need to use NULL in your code. Nulls can produce unexpected behavior and as such have no place in pure functions where unexpected behavior would
    // render them otherwise impure
    class Program
    {
        static void Main(string[] args)
        {
            // An optional type can hold an integer or a none
            Option<int> optionalInteger = 34; // note we can just assign it straight away
            optionalInteger = Option<int>.Some(34); // save as above. Shown just for demonstration purposes
            optionalInteger = Option<int>.None;
            // optionalInteger = null; Options effectively eliminate nulls in your code.

            var resultA = DivideBy(25, 5);
            var resultB = DivideBy1(optionalInteger, 5);

            Console.WriteLine($"The result A is '{resultA}' and B is '{resultB}'");
        }

        static int DivideBy(int thisNumber, int dividedByThatNumber)
        {
            return thisNumber / dividedByThatNumber;
        }

        static Option<int> DivideBy1(Option<int> thisNumber, int dividedByThatNumber)
        {
            return thisNumber.Map( i => i / dividedByThatNumber);
        }

    }      
}
