using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial34
{
    // BiMap()
    class Program
    {
        static void Main(string[] args)
        {
            //Procedural way 

            var step1 = DivideBy(225, 5);
            var step2 = Add5ToIt(step1);
            var result = PerformPensionCalculations(step2);
            

            // Fluent way
            Option<int> result1 = DivideBy1(225, 5)
                                .Bind(input => Add5ToIt1(input))
                                .Bind(input => PerformPensionCalculations1(input));

            // Transform both the invalid and valid versions of the options to a single string
            Option<string> results = result1.BiMap(Some: validInteger => "Valid", None: () => "Invalid");

            Console.WriteLine($"The result is '{results}'");

        }

        static Option<int> PerformPensionCalculations1(Option<int> input) 
            => input.Map(CalculateYourPension);

        static int PerformPensionCalculations(int input)
            => CalculateYourPension(input);


        static int CalculateYourPension(int input) 
            => (input * 3) / 26;

        static int DivideBy(int thisNumber, int dividedByThatNumber) 
            => dividedByThatNumber == 0 ? 0 : thisNumber / dividedByThatNumber;

        static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber) =>
            dividedByThatNumber != 0 
                ? thisNumber / dividedByThatNumber 
                : Option<int>.None;

        static Option<int> Add5ToIt1(Option<int> input) 
            => input.Map(validInput => validInput + 5);

        static int Add5ToIt(int input)
            => input + 5;
    }      
}
