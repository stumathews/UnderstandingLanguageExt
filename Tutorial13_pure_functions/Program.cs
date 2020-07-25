using System;
using System.Collections.Generic;
using System.Linq;
namespace Tutorial13
{
    // This tutorial shows you what a pure function is.
    // A pure function's return value is a product of its arguments ie only its arguments are used to determine the return value.
    // The expectation is that if it does this, then the same input to the function will yield the same output too.
    // To ensure this, you need to further restrict the function to not use/depend on anything that might jeopordise this, ie fetch/use a source that today might be one thing and tomorrow might be something else
    // for instance, if you get a value from the DB today, tomorrow it might be removed and then the guarantee you made that the function will return the same value breaks.
    // So you can't use Input/Output as that is the source of changeable circumstances.
    
    // Side note: Pure functions are immediately parallelizable and can be used for Memoization (storing the result and input of the function requires that the function never needs to run again)
    class Program
    {
        static void Main(string[] args)
        {

           Box<int[]> aBoxOfNumbers = new Box<int[]>(new int[]{ 1, 2, 3, 4, 5, 6, 7});

           var result = aBoxOfNumbers.Map(numbers => GenerateFibonacciSeriesFrom(numbers)); // note non-method group notation(not important)
           var result2 = aBoxOfNumbers.Map(ImpureGenerateFibonacciSeries); // not method group notation(not important)

            Console.WriteLine($"fibos are {string.Join(',', result.Extract)} and are gaurenteed to be this provided the same input is used");
            Console.WriteLine($"fibos generated from impure function are {string.Join(',', result2.Extract)} is not to be this always.");

            // Pure function only uses its input to generate its result and doesn't depend/get/fetch data from anywhere else that might impact the result. 
            // It certainly does not depend on things like I/O and db calls that might not bring the same result on subsequent calls of the function
            // and it does not throw exceptions(later tutorial show how to remove exceptions from your code using Either<IFailure, Result>). 
            int[] GenerateFibonacciSeriesFrom(int[] numbers)
           {
               var fibs = new List<int>(numbers.Length);
               for (var i = 0; i < numbers.Length + 1; i++)
               {
                   if (i > 1)
                       fibs.Insert(i, i - 1 + i);
                   else
                       fibs.Insert(i, i);
               }

               return fibs.ToArray();
           }

            int[] ImpureGenerateFibonacciSeries(int[] numbers)
            {
                var fibs = new List<int>(numbers.Length);
                for (var i = 0; i < numbers.Length + 1; i++)
                {
                    fibs.Insert(i, i > 1 ? AddFn(i - 1, i) : i);
                }

                // Impure add function because it is not guarantee to always do a number1+number2 addition consistently, as it depends on the day of the week which can change the result
                // even though the input numbers are the same on calls to it.
                int AddFn(int number1, int number2)
                {
                    if(DateTime.Today.DayOfWeek == DayOfWeek.Thursday) // Dependency on this condition breaks guarantee that the same input provided in number1 and number2 will ALWAYS yield the same result.
                        return number1 + number2;
                    else
                        return (number1 + number2 + 1);
                }

                // Eg. Input is 1,2,3
                // On Monday:    0,1,4,6,8,110,12,14
                // On Tuesday:   0,1,4,6,8,110,12,14
                // On Wednesday: 0,1,4,6,8,110,12,14
                // On Thursday:  0,1,3,5,7,9,11,13   <--- Breaks that promise that for input 1,2,3 you get the same out put as 0,1,4,6,8,110,12,14
                // On Friday:    0,1,4,6,8,110,12,14
                // On Saturday:  0,1,4,6,8,110,12,14
                // On Sunday:    0,1,4,6,8,110,12,14

                return fibs.ToArray();
            }
        }
    }      
}
