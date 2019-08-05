using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial18
{
    //  Iter: run an arbitary function on the either if its value is right type or BiIter() to specify a function to run on both types 

    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString = 45;

            bool didFunctionRun = false;

            // extracts the right content and if it is right, it run this non-transforming ie void returning function on it. The function wont  run it its as left type
            intOrString.Iter(rightString => RunAFunctionOnRightContents(rightString));

            // both actions are void returning
            intOrString.BiIter(rightString => RunAFunctionOnRightContents(rightString), leftInteger => Console.WriteLine($"Interger is {leftInteger}"));

            Console.WriteLine($"Did function run? {didFunctionRun}");

            void RunAFunctionOnRightContents(string str)
            {
                Console.WriteLine($"Hello {str}");
                didFunctionRun = true;

            }
        }
        
    }      
}
