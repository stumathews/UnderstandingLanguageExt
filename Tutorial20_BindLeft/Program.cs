using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial20
{
    // Shows the basics of Either<L,R>, using BindLeft() to make provision for a transform function for the left types of the either (which is unusual for the default Bind() function).
    // The transform is NOT automatically lifted(this is a bind() after all).

    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString = "Stuart";

            // Transform the left hand side, remember bind will not automatically life the result of that transformation, so you transformation function will need to do that
            // We call BindLeft because by default Either is right biased so default Bind() only transforms the right side if there is one(there there isn't as we've assigned a right value of string 'Stuart'
            var result = intOrString.BindLeft(left => TransformLeft(left));

            // Note the transformation did not occur because either contained a right type ie string
            Console.WriteLine($"result is {result}");

            intOrString = 55;
            // Note the transformation should now occur because either contained a left type and we've defined a transformation for that on this either
            result = intOrString.BindLeft(left => TransformLeft(left));

            Console.WriteLine($"result is {result}");

            Either<int, string> TransformLeft(int left)
            {
                Either<int, string> transformedResult = left + 22;
                return transformedResult;
            }
        }
        
    }      
}
