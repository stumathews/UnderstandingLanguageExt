using System;
using System.Linq;
using LanguageExt;
using Tutorial05;

namespace Tutorial05
{
    class Program
    {
        static void Main(string[] args)
        {
            // A Box
            Box<int[]> boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
            Box<int[]> boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

            // Do something with or to the Box

            var doubled1 = boxOfIntegers
                            .Bind(extract => new Box<int[]>(extract.Select(x => x * 2).ToArray())); 

            var doubled2 = boxOfIntegers
                            .Map(numbers => numbers.Select(x => x * 2).ToArray());
            
            var doubled3 = from extract in boxOfIntegers
                let transformedAndLifted = DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value
                from transformed in transformedAndLifted //SelectMany
                select transformed; // project(extract, transformedAndLiftedResult) part of SelectMany
            
            var doubled4 = from extract in boxOfIntegers
                select DoubleNumbers(extract).Extract; // Use Select via linq expression syntax


            Box<int[]> doubleDouble1 = boxOfIntegers
                .Bind(numbers => DoubleNumbers(numbers))
                .Map(DoubleNumbers)
                .Bind(box => box.Bind( numbers => DoubleNumbers(numbers) ));

            var doubleDouble2 = from numbers in boxOfIntegers
                from redoubled in DoubleNumbers(numbers)
                select redoubled;
                
            
            // Give me a box of Double Double of my Box
            var doubleDouble3 = from firstDouble in DoubleMyBox(boxOfIntegers)
                                from reDouble in DoubleNumbers(firstDouble) //VET: bind part of SelectMany()
                                    select reDouble; // project(reDouble, firstDouble)


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
