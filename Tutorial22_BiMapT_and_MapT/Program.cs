using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial22
{
    // This tutorial shows you how you can transform a List of Eithers, effectively doing a Map on each either in the list, and this Bi variety allows you to specify how make provision to map/transform both types
    // When a Bind/Map function is called on a list of monads, it is BindT or BiMapT, otherwise operating on a single monad, use Bind() or Map() alone
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

            // Extracts the rights only, ignores the lefts(they disapear from the result) - not lifted back into either - comparable to a Match() in this way
            IEnumerable<string> rights = listOfEithers.Rights();
            IEnumerable<int> lefts = listOfEithers.Lefts();

            // Runs a map transform function(automatic lift) on every either in the lift
            // Remember a map and a Bind can change the contained type of the returned either but it must be an either
            // the T in BiMapT really inndicates that the BiMapT works on a list of monads, in this case a list of Eithers
            IEnumerable<Either<char, string>> result = listOfEithers.BiMapT(rightString => $"TurnAllRightStringToThis ({rightString})", leftInteger => 'A' /*Turn all left values in the eithers to 'A'*/).ToList();

            // ok so now we've transformed the Eithers in different ways depending on their type, lets look at them
            
            // we could look at our transformations done on each using this:
            var lefts1 = result.Lefts();
            var rights1 = result.Rights();
            
            // or we could get a string representation of either type of the either and print that to show us how that either was transformed
            foreach (var either in result)
            {
                var stringResult = either.Match(rightString => rightString, leftInteger => $"{leftInteger}");
                Console.WriteLine(stringResult);
            }
            
            // Transform each either that has in in the right state, ie has a value of the right type
            // Remember to bind is to lift by yourself
            var result1 = result.MapT(rightString => TransformMe(rightString));

            // if you prefer to transform the left type for each either in the list, you can:
            var result2 = result.MapLeftT(c => char.ToUpper(c));

            // You can also transform the eithers in the list using a bind(not automatic lift)

            var result3 = result.BindT(s =>
            {
                Either<char, string> either = 'c'; // we need to lift a bind transformation function as always
                return either;
            });



            Either<char, string> TransformMe(string rightString)
             {
                 return $"the value is {rightString}";
                 
             }

             Console.WriteLine($"mapT result is:");
             foreach (var either in result1)
             {
                 var str = either.Match(Right: (rightString) => $"String is '{rightString}'", Left: (leftChar) => $"Char is '{leftChar}'");
                 Console.WriteLine(str);
             }
        }
        
    }      
}
