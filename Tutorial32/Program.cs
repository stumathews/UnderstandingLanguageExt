using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial32
{

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

            // Expression way 
            Option<int> result2 = from input1 in DivideBy1(225, 5)
                from input2 in Add5ToIt1(input1)
                from input3 in PerformPensionCalculations1(input2)
                select input3;

            // Oh Wow, we've really simplified the code, its smaller, all the steps are functions and we are happy
            // Passing and sending Monads have helped us create step-by-step logical means to execute code entirely from functions!
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
