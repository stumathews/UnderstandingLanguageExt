using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial25
{
    // This tutorial shows you how you can transform a List of Eithers, effectively doing a Map on each either in the list, and this Bi variety allows you to specify how make provision to map/transform both types
    
    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString1 = "Stuart";
            Either<int, string> intOrString2 = "Jenny";
            Either<int, string> intOrString3 = "Bruce";
            Either<int, string> intOrString4 = "Bruce";
            Either<int, string> intOrString5 = 66;

            // basically pass yourself ie your current value to the function provided.
            var resultA =  intOrString5.Apply(datas => UseThis(datas));

            // the function can transform the type of the either
            var resultB = intOrString5.Apply(datas => UseThisAndChangeType(datas));

            Console.WriteLine($"ResultA = {resultA}, ResultB = {resultB}");

            IEnumerable<Either<int, string>> listOfEithers = new Either<int, string>[] { intOrString1, intOrString2, intOrString3, intOrString4, intOrString5 };

            // So in the same way, give your self ie your content (which is an List of Eithers) to the provided function
            var result = listOfEithers.Apply(enumerable => UseThisListOfEithers(enumerable));

            Console.WriteLine($"The result of is '{result}'");

            Either<int, string> UseThis(Either<int, string> useThis)
            {
                var str = useThis.Match(rightString => rightString, leftInteger => "was left");
                Either<int, string> t = str;
                return t;
            }

            Either<int, char> UseThisAndChangeType(Either<int, string> useThis)
            {
                var str = useThis.Match(rightString => 'T', leftInteger => 'F');
                Either<int, char> t = str;
                return t;
            }

            IEnumerable<Either<int, string>> UseThisListOfEithers(IEnumerable<Either<int, string>> useMe)
            {
                // Transform the things in whatever state (type ie left or right) they are in
                return useMe.BiMapT(rightString => rightString, leftInteger => leftInteger * 2);
            }
        }
        
    }      
}
