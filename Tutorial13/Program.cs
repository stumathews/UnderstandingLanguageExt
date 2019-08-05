using System;
using System.Collections.Generic;
using System.Linq;
namespace Tutorial13
{
    // This tutorial shows you what a pure function is
    
    class Program
    {
        static void Main(string[] args)
        {

           Box<int[]> aBoxOfNumbers = new Box<int[]>(new int[]{ 1, 2, 3, 4, 5, 6, 7});

           var result = aBoxOfNumbers.Map(numbers => GenerateFibonacciSeries(numbers));

           // Method group notation
           var result2 = aBoxOfNumbers.Map(ImpureGenerateFibonacciSeries);

            Console.WriteLine($"fibos are {string.Join(',', result.Extract)} and are gaurenteed to be this provided the same input is used");
            Console.WriteLine($"fibos generated from impure function are {string.Join(',', result2.Extract)} is not to be this always.");

            // Pure function only uses its input to generate its result and doesn't depend/get/fetch data from anywhere else that might impact the result. 
            // It certainly does not depend on things like I/O and db calls that might not bring the same result on subsequent calls of the function
            int[] GenerateFibonacciSeries(int[] numbers)
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

                // Impure add function becasue it is not gaurenteed to always do a number1+number2 addition consistently, as it depends on the day of the week which can change the result
                // even though the input numbers are the same on calls to it.
                int AddFn(int number1, int number2)
                {
                    if(DateTime.Today.DayOfWeek == DayOfWeek.Monday) // Dependency breaks gaurenteed that the same input provided in number1 and number2 will ALWAYS yield the same result.
                        return number1 + number2;
                    else
                        return (number1 + number2 + 1);
                }

                return fibs.ToArray();
            }

        }
        
    }      
}
