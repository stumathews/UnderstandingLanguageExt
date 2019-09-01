using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial23
{
    // This tutorial shows you how you can transform a List of Eithers, effectively doing a Bind on each either in the list
    // and a Bi variety allows you to specify how make provision to map/transform both types
    
    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString1 = "Stuart";
            Either<int, string> intOrString2 = "Jenny";
            Either<int, string> intOrString3 = "Bruce";
            Either<int, string> intOrString4 = "Bruce";
            Either<int, string> intOrString5 = 66;

            IEnumerable<Either<int, string>> listOfEithers = new Either<int, string>[] { intOrString1, intOrString2, intOrString3, intOrString4, intOrString5 };

            // transform the right values (if they are there) for each either in the list
            // As this is a bind, you need to lisft the result in to a Either
            var transformedList = listOfEithers.BindT(rightString => TransformRight(rightString));

            Either<int, string> TransformRight(string rightString)
            {
                Either<int, string> t = $"My name is '{rightString}'";
                return t;
            }

            var newRights = transformedList.Rights(); /* note we dont care care about the lefts, 
            if we did we migth you match to see what both left and right values would be if they are set on the eithers we are looking at - see Tutorial 22 */
            foreach (var str in newRights)
            {
                Console.WriteLine(str);
            }
        }
    }      
}
