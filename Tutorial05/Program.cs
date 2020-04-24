using System;
using System.Linq;
using Tutorial05;
// Shows various invocations of Bind() and Map() using lambda functions as the user defined transform functions, explicit named functions, and
// using both linq query syntax and fluent forms.
namespace Tutorial05
{
    class Program
    {
        static void Main(string[] args)
        {
            // A Box
            Box<int[]> boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
            Box<int[]> boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

            // Do something with or to the Box, uses user defined function specified in the form of lambdas to the Map() and Bind() functions

            var doubled1 = boxOfIntegers
                            .Bind(extract => new Box<int[]>(extract.Select(x => x * 2).ToArray())); // Extract, Validate and transform using Bind()

            var doubled2 = boxOfIntegers
                            .Map(numbers => numbers.Select(x => x * 2).ToArray()); // Extract, Validate and transform using Bind()

            // Extract, Validate and transform using SelectMany()
            var doubled3 = from extract in boxOfIntegers 
                           from transformed in DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value 
                select transformed; // project(extract, transformedAndLiftedResult) part of SelectMany
            
            var doubled4 = from extract in boxOfIntegers
                select DoubleNumbers(extract).Extract; // Use Select via linq expression syntax
            
            // Note we can use Map or Bind for transformation, but it becomes necessary to choose/use a specific one depending
            // on if or not the provided transformation function returns a box or not (lifts or doesn't),
            // ie is transformed in a call to Bind() or Map()
            Box<int[]> doubleDouble1 = boxOfIntegers
                .Bind(numbers => DoubleNumbers(numbers)) // need to use a transformation function that will lift
                .Map(DoubleNumbers) // need to use a transformation that does not already lift
                .Bind(box => box.Bind( numbers => DoubleNumbers(numbers) )); // same as above bind() case

            // Using Linq query syntax
            var doubleDouble2 = from numbers in boxOfIntegers
                from redoubled in DoubleNumbers(numbers)  // transformation function needs to lift
                select redoubled;  // Box's Select() function will do the lift here into Box<int[]> so no need to in this line
                
            
            // Give me a box of Double Double of my Box
            var doubleDouble3 = from firstDoubleTransformation in DoubleMyBox(boxOfIntegers)
                                            from secondDoubleTransformation in DoubleNumbers(firstDoubleTransformation) //VET: bind part of SelectMany()
                                                select secondDoubleTransformation; // project(reDouble, firstDouble)
        }

        /// <summary>
        /// Takes a Box of numbers and produces a box of doubled numbers
        /// </summary>
        private static Box<int[]> DoubleMyBox(Box<int[]> boxOfIntegers)
        {
            return from extract in boxOfIntegers
                    from doubledNumber in DoubleNumbers(extract)
                        select doubledNumber;
        }

        // transform Extracted, and Lift it
        static Box<int[]> DoubleNumbers(int[] extract)
        {
            return new Box<int[]>(extract.Select(x => x * 2).ToArray());
        }
    }
}
