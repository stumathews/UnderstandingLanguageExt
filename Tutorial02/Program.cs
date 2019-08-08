using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tutorial01;

namespace Tutorial02
{
    class Program
    {
        static void Main(string[] args)
        {
            // Now my Box contains another kind of thing, a list of integers. So effectively my Box is a box of numbers!
            Box<int[]> numbers1 = new Box<int[]>(new []{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
            Box<int[]> numbers2 = new Box<int[]>(new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });

            // Now have a look at Box class again, it has changed a little with two new extension methods.

            // Notice how the follow an interesting trend of doing VETL or Validate content, Extract Content, Transform Content and Lift the content
            
            // Validate, Extract, Transform and YouLift(If Valid)
            Box<int[]> transformedResult1 = numbers1.Bind(contents => MyFunction(contents)); // user define transformation, just ignores the contents and returns a new set of numbers

            // Validate, Extract, Transform and automatic Lift (If Valid) 
            Box<int[]> transformedResult2 = numbers2.Map(contents => MyFunction2(contents)); // same transformation result, but we didn't have to return a Box

            // The Box class is now considered a Monad, becasue it has these two addition functions (map and bind).
            // Note in both cases of Bind() and Map() we did a transformation of the contents. In both cases we didn't use/read the contents of the box, so we didn't really transform the contents, but 
            // we could have included the contents in out transformation. 
            // The take away is that Map() and Bind() do the same thing but Bind() requires you to put your transformation result back in the box, while Map doesn't.

            Console.WriteLine($"The result of the Bind operation was {transformedResult1.Item}");
            Console.WriteLine($"The result of the Map operation was {transformedResult2.Item}");

            // But here is something sneaky:
            Box<string> transformedResult3 = numbers1.Map(contents => "I'm a string!"); // Look, we've been able to change the type of the Box from a int[] to a string! This is by virtue of thats what you transformation function returned.
            // Its still in a Box, but is a string. Map() and Bind() can do this.
        }

        private static int[] MyFunction2(int[] numbers)
        {
            return new int[] {3, 4, 5};
        }

        private static Box<int[]> MyFunction(int[] integerArray)
        {
            return new Box<int[]>(new int[] {1, 2});
        }
    }
}
