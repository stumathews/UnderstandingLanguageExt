using System;
using System.Linq;

/*
 This tutorial shows you how pipelining is used to call funtions.
 This tutorial shows how to use the Linq Fluent and Expression syntax to achive the same thing
 This also demonstrates Perfect map and find function and show when to use map and bind and why
*/
namespace Tutorial06
{
    class Program
    {
        static void Main(string[] args)
        {
            // A Box
            Box<int[]> boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
            Box<int[]> boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

            // Do something with or to the Box

           
            /* Using the select many way, ie the linq expression syntax as shown below allows an extracted item from the box, then to be
             * passed down a series of transforms by way of those transform functions being compatible with the bind() phase of the SelectMany() function
             which means that they can see the prior transformation and can act on it.
             And as each transformation function is run as part of the SelectMany() implementation of Box, it will also be subject to the VETL phases
             which means if the input is not valid, it will will return a invalid value and subsequent transforms upon recieving that invalid input will also 
             return an invalid input and in all those cases i doing that the underlying transform is not run.             
             */
            Box<object> doubled1 = from extract in boxOfIntegers // extract items out
                from transformed in DoubleNumbers(extract) // bind() part of SelectMany() ie transform extracted value (and below):
                from transformed2 in DoubleNumbers(transformed) // use/transform/doublenumbers() the last transformed result
                from transformed3 in DoubleNumbers(transformed2) // use/transform/doublenumbers() the last transformed result
                from transformed4 in DoubleNumbers(transformed3) // use/transform/doublenumbers() the lsat transformed result
                    select Dosomethingwith(transformed, transformed2, transformed3, transformed4); // project(extract, transformed) part of SelectMany which always does an automatic result of the projected result (as a box<>)
                
            Box<object> doubled2 = boxOfIntegers
                .Bind(extract => DoubleNumbers(extract))
                .Bind(transformed => DoubleNumbers(transformed))
                .Bind(transformed => DoubleNumbers(transformed))
                .Bind(transformed => DoubleNumbers(transformed))
                .Map(transformed => Dosomethingwith(transformed)); // I have to use map here because DoSomethingWith() does not return a Box and the result of a Map(will always do that) or Bind must do make its transform function do that
            
        }

        // Perfect Map function to use with a Map as map will lift this and so this function does not have to lift its result
        private static object Dosomethingwith(params int[][] varargs)
        {
            // Note we dont have to return a Box<> because as we''ll be running within a SelectMany() it automatically lifts the result in this case a object type
            return new object();
            // Note that this function will be acting as the bind() transformation function within the SelectMany() function
        }

        // transform Extracted, and Lift it.
        // Perfect bind function to be used in a Bind() because it lifts the result
        static Box<int[]> DoubleNumbers(int[] extract)
        {
            /* do something with the numbers we extracted from the box and then put them back in the box again
             because this function will be run in the bind() phase of the SelectMany() function (see the selectMany function implementation) and that function
             requires a signature of :
             int[] => Box<int[]>
            */
            return new Box<int[]>(extract.Select(x => x * 2).ToArray());
        }
    }
}
