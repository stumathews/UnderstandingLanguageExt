using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial31
{

    class Program
    {
        static void Main(string[] args)
        {

            var resultB = DivideBy1(225, 5);

            var result2 = Add5ToIt(resultB);

            // Now we can continue with the knowledge that result2 as 5 added to it or not (and everyone else will do that too:)

            var result = PerformPensionCalculations(result2);

            Unit noValue = result.IfSome(validInput => Console.WriteLine("yay valid input is {validInput}"));
            int defaultPensionIfInvalidInput = result.IfNone(() => -1); // IfNone turns your result into a valid value, effectively a Some() but its actual value, int
        }

        static Option<int> PerformPensionCalculations(Option<int> input)
        {
            // Extract, Validate and transform it using Map
            return input.Map(validInput => CalculateYourPension(validInput));
            // We can call a normal function (non monad returning or accepting) inside a Map or Bind
        }


        static int CalculateYourPension(int input)
        {
            return (input * 3) / 26;
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

        static Option<int> Add5ToIt(Option<int> input)
        {
            // View a Bind/Map as 'Try to add 5' if the input is valid ie not a None
            return input.Bind(validInput =>
            {
                Option<int> option = validInput + 5;
                return option;
            });
        }
    }
}
