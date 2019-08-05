using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial16
{
    // This tutorial shows you what a what an Either<>'s BiExists() 
    
    class Program
    {
        static void Main(string[] args)
        {

            int number = 5;
            string word = "five";

            Either<int, string> amount = 5;
            amount = word;

            // like BiBind() allows you to provide both transform functions and the correct transform will run depending on is it is a right or left type contained within it - you can do something similar here.
            // BiExists allows you to test/use/inspect the content and return true/false based on it.
            // BiExists can be viewed as a the 'existance' of a validation check being successful
            var isEitherGreaterThanNothing = amount.BiExists(stringRight => stringRight.Length > 0 ? true: false, integerleft => integerleft > 0 ? true : false);

            Console.WriteLine($"The result value is {isEitherGreaterThanNothing}");
        }
        
    }      
}
