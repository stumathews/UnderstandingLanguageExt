using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;
using Tutorial35;

namespace Tutorial33
{
    // ThrowIfFailed and the standard wrapped return type Option<T> - wrapped in an Either of that or a failure.
    // T can be any type your function deals with, as you'd use in any normal function you create, only you make it an Option. You also bundle with your return type a failure if there is one, by 
    // returning an all enconcompassing return value of Either<IAmFailure, Option<T>>
    class Program
    {
        static void Main(string[] args)
        {
           // Note that a common Either of form Either<IAmFailure, Option<T>> is used here to 
           // 1) Communicate if there anything or any reason while processing the Option<T> that is eroneous - that will then failfast and return a IAmFailure ie Either in Left state
           // 2) If there wasn't while inspecting the Option<T> return that or a transformation of that ie an Option<T> 
            var result1 = DivideBy1(225, 5)
                                .Bind(input => Add5ToIt1(input).ThrowIfFailed()) // throw if failed either, otherwise return the right value
                                .Bind(input => PerformPensionCalculations1(input).ThrowIfFailed()); // Inspect for either for a failure and throw if it is, otherwise return the value as-is
            
            Console.WriteLine($"The result is '{result1}'");

        }

        static Either<IAmFailure, Option<int>> PerformPensionCalculations1(Option<int> input)
        {
            /* Remember, We can return a IAmFailure or a Option<int> */

            // Generate a failure why looking at te option, by extracting the value and determining if its valid or not
            var isGreaterThan100 = input.Match(Some: number => number > 100 ? true : false, None: false);
            if (!isGreaterThan100)
                return new GenericFailure("Must be greater than 100");
            // Do some validation and fail fast, otherwise return the value
            return input.Map(CalculateYourPension); //Retrun Option<T> 
        }
        
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

        static Either<IAmFailure, Option<int>> Add5ToIt1(Option<int> input)
        {
            // arbitary test
            var isValid = input.Match(Some: number => number > 12, None: false);
            
            // We can return a IAmFailure or a Option<int>
            if(!isValid)
                return new GenericFailure("must be greater than 12");
            return input.Map(validInput => validInput + 5); //Return Option<T>
        }

        static int Add5ToIt(int input)
            => input + 5;
    }      
}
