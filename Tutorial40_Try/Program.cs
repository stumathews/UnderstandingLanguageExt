using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;


namespace Tutorial40
{
    // This tutorial demonstrates the use of the Try<> Monad.
    class Program
    {
        static void Main(string[] args)
        {
            // This is an function that could throw an exception. We dont want that because that would jump straight out of our function
            // and cause our function not to complete fully. We want robust, dependable functions that always return no matter what.
            int SomeExternalCode(int numerator, int denominator)
            {
                if(denominator == 0)
                    throw new DivideByZeroException();
                return numerator / denominator;
            }

            // What we can do now is wrap this unsafe function, into a Try<> which will capture any exceptions that are thrown (they dont leave the function)
            Try<int> try1 = new Try<int>(() =>
            {
                var result = SomeExternalCode(25, 5);
                return result;
            });

            Try<int> try2 = new Try<int>(() =>
            {
                var result = SomeExternalCode(25, 0);
                return result;
            });

            // Now we have encapsulated any exceptions into the Try<> type, we can now check if it had any failures otherwise use the result    
            var result1 = try1.Match(
                unit => unit.ToEither<IAmFailure, int>(), // was ok, no exceptions thrown
                exception => new ExternalLibraryFailure(exception)); // We got an exception, now how should we deal with it - lets turn it into a IAmFailure type

            var result2 = try2.Match(
                unit => unit.ToEither<IAmFailure, int>(), // was ok, no exceptions thrown
                exception => new ExternalLibraryFailure(exception)); // We got an exception, now how should we deal with it - lets turn it into a IAmFailure type

            // Print the results
            Console.WriteLine($"Result1 was: {result1} and Result2 was {result2}");

            // And if you required all/both to succeed you could use short-circuiting behavior in a pipeline
            var overallResult = 
                from res1 in result1
                from res2 in result2
                select res1 / res2;

            Console.WriteLine($"The combined result is {overallResult}");
        }
    }   
}

