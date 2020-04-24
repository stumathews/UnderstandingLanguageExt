using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial20
{
    // Using Match to extract the contents of an Either<> but  not put it back into and either types (as map() and Bind() would do)

    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString = "Stuart";
            
            // Match will run a function for which ever type is contained within the either.
            // Transform functions for both types of content are specified.  The function will run depending on the underlying type in the either.
            // The return result of each function must be the same type (both string or both int) such that you can assign the result of the Match to a types variable
            // Only one function will run, as only one of the two types can be in the either at any one moment in time.
            string result = intOrString.Match(rightString => $"Right value is {rightString}", leftInteger => $"left value is {leftInteger}");

            Console.WriteLine($"Result is {result}");

            intOrString = 32;
            result = intOrString.Match(rightString => $"Right value is {rightString}", leftInteger => $"left value is {leftInteger}");

            Console.WriteLine($"Result is {result}");
        }
        
    }      
}
