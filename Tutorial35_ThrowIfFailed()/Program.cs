﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;
using Tutorial35;

namespace Tutorial35
{
    // ThrowIfFailed and a way to make functions return a standard return type of an Either of Right(T) or a failure(Left).
    // T can be any type your function deals with, as you'd use in any normal function you create, only you make it an Option. 
    // You also bundle with your return type a failure if there is one, by returning an all encompassing return value of Either<IAmFailure, Option<T>>
    
    class Program
    {
        static void Main(string[] args)
        {
           // Note that a common Either of form Either<IAmFailure, Option<T>> is used here to 
           // 1) Communicate if there anything or any reason while processing the Option<T> that is erroneous - that will then fail-fast and return a IAmFailure ie Either in Left state
           // 2) If there wasn't while inspecting the Option<T> return that or a transformation of that ie an Option<T> 
            var result1 = DivideBy(225, 5)
                                .Bind(input => Add5ToIt(input).ThrowIfFailed()) // throw if failed either, otherwise extract and return the right value
                                .Bind(input => PerformPensionCalculations(input).ThrowIfFailed()); // Inspect for either for a failure and throw if it is, otherwise return the value as-is
            
            Console.WriteLine($"The result is '{result1}'");
        }

        static Either<IAmFailure, Option<int>> PerformPensionCalculations(Option<int> input)
        {
            /* Remember, We can return a IAmFailure or a Option<int> */

            // Generate a failure while looking at the option, by extracting the value and determining if its valid or not
            var isGreaterThan100 = input.Match(Some: number => number > 100 ? true : false, None: false);
            if (!isGreaterThan100)
                return new GenericFailure("Must be greater than 100");
            // Do some validation and fail fast, otherwise return the value
            return input.Map(CalculateYourPension); //return Option<T> 
        }

        static int CalculateYourPension(int input) 
            => (input * 3) / 26;

        static Option<int> DivideBy(int thisNumber, int dividedByThatNumber) =>
            dividedByThatNumber != 0 
                ? thisNumber / dividedByThatNumber 
                : Option<int>.None;

        static Either<IAmFailure, Option<int>> Add5ToIt(Option<int> input)
        {
            // arbitary test
            var isValid = input.Match(Some: number => number > 12, None: false);
            
            // We can return a IAmFailure or a Option<int>
            if(!isValid)
                return new GenericFailure("must be greater than 12");
            return input.Map(validInput => validInput + 5); //Return Option<T>
        }
    }      
}
