using System;
using System.Linq;

namespace Tutorial07
{
    class Program
    {
        static void Main(string[] args)
        {
            // A Box
            Box<int[]> boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
            Box<int[]> boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

            // Do something with or to the Box

            Box<object> doubled3 = from extract in boxOfIntegers 
                from transformed in DoubleNumbers(extract)
                from transformed2 in DoubleNumbers(transformed) 
                from transformed3 in DoubleNumbers(transformed2)
                from transformed4 in DoubleNumbers(transformed3)
                    select Dosomethingwith(transformed, transformed2, transformed3, transformed4);

            Box<object> doubleDouble12 = boxOfIntegers
                .Bind(extract => DoubleNumbers(extract))
                .Bind(transformed => DoubleNumbers(transformed))
                .Bind(transformed => DoubleNumbers(transformed))
                .Bind(transformed => DoubleNumbers(transformed))
                .Map(transformed => Dosomethingwith(transformed)); 

            // This shows that you can use either map or bind (they do the same thing ie both transform their input) but map will lift automatically and bind needs its transform
            // function to do that.
            // This dicision of using map or bind will ipact on how the subsequent functions deal with either a lifted or a non-lifted result.
            // So depending on what transformation function you use on the prior input, will affect how the next step deals with either
            // a lifted result or a non-lifted result
            Box<int[]> doubleDouble1 = boxOfIntegers
                .Bind(numbers => DoubleNumbers(numbers)) // Non automatically lifted result, so DoubleNumbers will have to lift, and it has to lift becasue subsequent Map or Bind need to work from a lifted value ie a Box<>
                .Map(DoubleNumbers) // now we have DoubleNumbers lifting the transformed value and becasue we used Map to transform it, it automatically lifts it again... so we really should have used bind() but no matter, we'll deal with it:
                .Bind(box => box.Bind( numbers => DoubleNumbers(numbers) )) // due to to the double lift, ie Box<Box<>> we first the extracted item is a Box<Box<>> which is a Box<>
                // so to extract something form that box i need to use a map or bind, in this case I chose bind, whch additionally will not lift and i can use Bind next:
                .Bind(numbers => DoubleNumbers(numbers)) 
                .Bind(numbers => DoubleNumbers(numbers));
        }

        private static object Dosomethingwith(params int[][] varargs)
        {
            // Note we dont have to return a Box<> because as we''ll be running within a SelectMany() it automatically lifts the result in this case a object type
            return new object();
            // Note that this function will be acting as the bind() transformation function within the SelectMany() function
        }

        // transform Extracted, and Lift it
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
