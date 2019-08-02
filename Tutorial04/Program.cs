using System;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using Tutorial01;

namespace Tutorial04
{
    /// <summary>
    /// Performing operations on a Box using Map() and Bind(), Select and SelectMany()
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // A Box
            Box<int[]> boxOfIntegers = new Box<int[]>(new []{ 3, 5, 7, 9, 11, 13, 15});

            // Do something with or to the Box
            var doubled1 = DoubleBox1(boxOfIntegers); // Use Bind
            var doubled2 = DoubleBox2(boxOfIntegers); // use Map
            var doubled3 = DoubleBox3(boxOfIntegers); // Use SelectMany via linq expression syntax
            var doubled4 = DoubleBox4(boxOfIntegers); // Use Select via linq expression syntax
        }


        /// <summary>
        /// Transform(double) the contents of a box ie the value extracted from it using Bind, which will not automatically lift the transformed value.
        /// You'll need to lift the transformed value into a Box again
        /// </summary>
        /// <param name="boxOfIntegers"></param>
        /// <returns></returns>
        private static Box<int[]> DoubleBox1(Box<int[]> boxOfIntegers)
        {
            // If the transformed is already lifted, use Bind to achieve the transform without a lift applied
            return boxOfIntegers.Bind(numbers => DoubleNumbers(numbers)); // can use bind instead of Double2's version which uses SelectMany which automatically lifts
        }


        /// <summary>
        /// Transform(double) the contents of a box ie the value extracted from it using Map, which will happily and automatically lift the transformed value ina box so you don't have to.
        /// </summary>
        private static Box<int[]> DoubleBox2(Box<int[]> boxOfIntegers)
        {
            // Use Map to automatically lift transformed result
            return boxOfIntegers.Map(numbers => DoubleNumbersNoLift(numbers));
        }

        /// <summary>
        /// Extract the contents of the box using Select and then transform it using SelectMany via the Linq Expression syntax
        /// </summary>
        private static Box<int[]> DoubleBox3(Box<int[]> boxOfIntegers)
        {
            return 
                from extract in boxOfIntegers
                let transformedAndLifted = DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value
                from transformed in transformedAndLifted //SelectMany
                         select transformed; // project(extract, transformedAndLiftedResult) part of SelectMany
            /*Note: not using 'extract' value in this project function, just the transformed value*/
        }

        /// <summary>
        /// Like DoubleBox3 but shows that you dont have to put the same type (int[]) of item back in the box, you can put any type of item back in the box (string)
        /// </summary>
        private static Box<string> DoubleBox11(Box<int[]> boxOfIntegers)
        {
            return
                from start in boxOfIntegers
                from startTransformed in DoTransformToAnyBox(start) // bind() part of SelectMany() ie transform extracted value
                select start + startTransformed; // Project(extract, transformedAndLifted) part of SelectMany

            Box<string> DoTransformToAnyBox(int[] input)
            {
                return new Box<string>($"{input.Sum()}");
            }
        }

        /// <summary>
        /// Shows how Select is like Map
        /// </summary>
        /// <param name="boxOfIntegers"></param>
        /// <returns></returns>
        private static Box<int[]> DoubleBox4(Box<int[]> boxOfIntegers)
        {
            Box<Box<int[]>> t = from extract in boxOfIntegers  
                select DoubleNumbers(extract); // Remember a Select does a automatic lift

            return t.Item;
        }


        // transform Extracted, and Lift it
        static Box<int[]> DoubleNumbers(int[] extract)
        {
            return new Box<int[]>(extract.Select(x => x * 2).ToArray());
        }

        static int[] DoubleNumbersNoLift(int[] extract)
        {
            return extract.Select(x => x * 2).ToArray();
        }
    }
}
