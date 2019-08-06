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
