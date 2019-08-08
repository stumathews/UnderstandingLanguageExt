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
            Either<int, string> intOrString = 45; // put it in left state by default (integer)

            bool didFunctionRunForRight = false;
            bool didFunctionRunForLeft = false;

            // extracts the right content and if it is right, it run this non-transforming ie void returning function on it. The function wont  run it its as left type
            intOrString.Iter(rightString => RunAFunctionOnRightContents(rightString));

            // both actions are void returning (Unit represents a typed void result)
            Unit ret = intOrString.BiIter(rightString => RunAFunctionOnRightContents(rightString), leftInteger => didFunctionRunForLeft = true);

            Console.WriteLine($"Did function run on right contents? {didFunctionRunForRight}");
            Console.WriteLine($"Did function run on left contents? {didFunctionRunForLeft}");

            void RunAFunctionOnRightContents(string str)
            {
                Console.WriteLine($"Hello {str}");
                didFunctionRunForRight = true;

            }
        }
        
    }      
}
