using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial17
{
    // This tutorial shows you what fold() does
    
    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString = "start";
            
            // A state (InitialResult) changes over time and it changes using results of the previous change. It uses an item from the array in changing the state each time.
            // The state changes the number of elements in the either, there will only be one. the state will change once based on the one value in the either.
            // For a Lst which has multiple items in it, the state will change that many times
            var result = intOrString.Fold("InitialState", (previousResult, extract) => changeState(extract, previousResult));
            
            // The result is the last state change
            Console.WriteLine($"The result value is {result}");

            string changeState(EitherData<int, string> extracted, string previousResult)
            {
                var content = extracted.State == EitherStatus.IsLeft ?  $"{extracted.Left}" : $"{extracted.Right}" ;
                var newResult = $"{previousResult} and {content}";
                return newResult;
            }
        }
        
    }      
}
