using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial33
{
    // ToEither extension method to convert a value to a right sided Either<L,R>
    class Program
    {
        static void Main(string[] args)
        {
            //Procedural way 
            int startingAmount = 225;
            var step1 = DivideBy(startingAmount, 5);
            var step2 = Add5ToIt(step1);
            var result = PerformPensionCalculations(step2);
            

            // Fluent way
            Option<int> result1 = DivideBy1(startingAmount, 5)
                                .Bind(input => Add5ToIt1(input))
                                .Bind(input => PerformPensionCalculations1(input));

            // The some value of the Option<T> ie the integer will be the right value of the either, adn then you need to choose a left value:
            Either<int, int> either = result1.ToEither(() => 23);

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
