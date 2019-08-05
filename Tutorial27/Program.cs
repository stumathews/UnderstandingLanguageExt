using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial27
{
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Either<int, string> intOrString1 = "Stuart";
            Either<int, string> intOrString2 = "Jenny";
            Either<int, string> intOrString3 = "Bruce";
            Either<int, string> intOrString4 = "zxcmbasdjkfkejrfg";
            Either<int, string> intOrString5 = 66;
            Either<int, string> intOrString6 = 234;

            IEnumerable<Either<int, string>> listOfEithers = new Either<int, string>[] { intOrString1, intOrString2, intOrString3, intOrString4, intOrString5, intOrString6, };

            // Go through each of the Eithers and depending on their types and their content, transform them
            // and we can reprsent values from either type, string or int as one uniform type, in this case we can represent a left and a right as a string and
            // thus return the set of all these representation as a list of strings
            string MakeGenderAwareString(string rightString)
            {
                string[] boysNames = new[] {"Stuart", "Bruce"};
                string[] girlsNames = new[] {"Jenny"};

                if (boysNames.Contains(rightString))
                    return $"{rightString} is a Boys name";
                if (girlsNames.Contains(rightString))
                    return $"{rightString} is a Girls name";
                return $"i dont know if {rightString} not registered with me as a boys name or a girls name";
            }

            string MakeOneHundredAwareString(int leftInteger)
            {
                return leftInteger > 100 ? $"{leftInteger} is greater than 100" : $"{leftInteger} is less tan 100";
            }

            var result = listOfEithers.Match(MakeGenderAwareString, MakeOneHundredAwareString);

            // note the order is not maintained
            foreach (var transform in result)
            {
                Console.WriteLine(transform);
            }
        }

    }      
}
