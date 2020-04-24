using System;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using Tutorial01;

namespace Tutorial04
{
    /// <summary>
    /// Performing operations on a Box using Map() and Bind() via Select and introducing the SelectMany() extension method on Box
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
        /// Transform(double) the contents of a box ie get the value extracted from box using Bind,
        /// and then transform(though using Bind() will not automatically lift the transformed value, so you need to do that).
        /// You'll need to lift the transformed value into a Box again
        /// </summary>
        /// <param name="boxOfIntegers"></param>
        /// <returns></returns>
        private static Box<int[]> DoubleBox1(Box<int[]> boxOfIntegers)
        {
            // If the transformed result is already lifted, which it is as DoubleNumbers() already returns a Box,
            // use Bind to achieve the transform without an explicitly lift into the Box being applied
            return boxOfIntegers.Bind(numbers => DoubleNumbers(numbers));
        }

        // transform Extracted, and Lift it
        static Box<int[]> DoubleNumbers(int[] extract)
        {
            // Remember a Select() run a function on the item in the box
            // Also note that the Select() function here is provided by the .NET runtime and it not the Select() we
            // wrote for the Box type, as extract is of type int[].
            // This version of select behaves similarly to what the Box's Select does - we just can't see it (it runs the user provided transform function)
            return new Box<int[]>(extract.Select(x => x * 2).ToArray());
        }

        /// <summary>
        /// Transform(by doubling) the contents of a box ie. the value extracted from it using Map, 
        /// which will happily and automatically lift the transformed value into a box so you don't have to.
        /// </summary>
        private static Box<int[]> DoubleBox2(Box<int[]> boxOfIntegers)
        {
            // Use Map to automatically lift transformed result
            return boxOfIntegers.Map(numbers => DoubleNumbersNoLift(numbers));
        }

        /// <summary>
        /// Extract the contents of the box and then transform it using SelectMany via the Linq Expression syntax
        /// </summary>
        private static Box<int[]> DoubleBox3(Box<int[]> boxOfIntegers)
        {
            // We can use the SelectMany() extension method to Validate, Extract, and transform its contents.
            // Have a look at Box's SelectMany() implementation now
            // and realize that its this that is used to allow this 'double from from' Linq expression construct, that you see from time to time in
            // peoples code

            return 
                from extract in boxOfIntegers
                let transformedAndLifted = DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value
                from transformed in transformedAndLifted
                         select transformed; // see internals of SelectMany function --> project(extract, transformedAndLiftedResult) as this select statement is this project() function in SelectMany implementation

            /* Note: we are not using 'extract' value in this project function (the final select), just the transformed value
             * we could have used in during our transformation, because it in scope and is accessible to be included */
        }

        /// <summary>
        /// Like DoubleBox3 but shows that you dont have to put the same type (int[]) of item back in the box, 
        /// you can put any type of item back in the box (string)
        /// </summary>
        private static Box<string> DoubleBox11(Box<int[]> boxOfIntegers)
        {
            return
                from start in boxOfIntegers
                from startTransformed in DoTransformToAnyBox(start) // bind() part of SelectMany() ie transform extracted value
                select start + startTransformed; // Project(extract, transformedAndLifted) part of SelectMany

            // local function
            Box<string> DoTransformToAnyBox(int[] input)
            {
                return new Box<string>($"{input.Sum()}");
            }
        }

        /// <summary>
        /// Shows how our Select() is called in a linq expression using from
        /// </summary>
        /// <param name="boxOfIntegers"></param>
        /// <returns></returns>
        private static Box<int[]> DoubleBox4(Box<int[]> boxOfIntegers)
        {
            Box<int[]> t  = from extract in boxOfIntegers  
                                     select DoubleNumbersNoLift(extract); // Remember Select() does the lift!

            return t;
        }

        // As this does not return a Monad, it can be used as a transformation function, that is passed to Map()
        static int[] DoubleNumbersNoLift(int[] extract)
        {
            return extract.Select(x => x * 2).ToArray();
        }
    }
}
