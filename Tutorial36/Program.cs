using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;


namespace Tutorial36
{
    // We can convert a 'failed' standard lusid function signature to a None. This is helpful if you want to turn a failure into a 'valid' standard lusid either but with None Right value
    class Program
    {
        static void Main(string[] args)
        {
            var result1 = 
                from divideResult in DivideBy1(225, 66)
                from addResult in Add5ToIt1(divideResult).FailureToNone(failure => true)
                from calcResult in PerformPensionCalculations1(addResult).FailureToNone(failure => true)
                select calcResult;

            Console.WriteLine($"The result is '{result1.ThrowIfFailed()}'"); // this wont throw even though we failed, becasue we converted failures to None above!
        }

        static Either<IAmFailure, Option<int>> PerformPensionCalculations1(Option<int> input)
        {
            var isGreaterThan100 = input.Match(Some: number => number > 1, None: false); //match is like flattening an either in one common result type, in this case: bool
            return !isGreaterThan100
                ? (Either<IAmFailure, Option<int>>) new GenericFailure("Must be greater than 100")
                : input.Map(CalculateYourPension);
        }
        
        static int PerformPensionCalculations(int input)
            => CalculateYourPension(input);

        static int CalculateYourPension(int input) 
            => (input * 3) / 26;

        static int DivideBy(int thisNumber, int dividedByThatNumber) 
            => dividedByThatNumber == 0 ? 0 : thisNumber / dividedByThatNumber;

        static Either<IAmFailure, Option<int>> DivideBy1(int thisNumber, int dividedByThatNumber) =>
            dividedByThatNumber == 0 
                ? new GenericFailure("Can't divide by 0") 
                : Option<int>.Some(thisNumber / dividedByThatNumber).ToSuccess();

        static Either<IAmFailure, Option<int>> Add5ToIt1(Option<int> input)
        {
            var isValid = input.Match(Some: number => number > 12, None: false);
            return !isValid
                ? (Either<IAmFailure, Option<int>>) new GenericFailure("must be greater than 12")
                : input.Map(validInput => validInput + 5);
        }

        static int Add5ToIt(int input)
            => input + 5;
    }      
}
