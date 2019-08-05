using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;

namespace Tutorial15
{
    // This tutorial shows you what a what an Either<>'s BiBind() functionality
    
    class Program
    {
        static void Main(string[] args)
        {

            int number = 5;
            string word = "five";

            Either<int, string> amount = 5;
            amount = word;

            // Instead of only being able to run a transform on the right hand side only as bind() does, you can use BiBind() to prepare transform functions for whatever side, left or right type is assigned to it!
            var resultOfTransform = amount.BiBind(rightString => TransformExtractedRight(rightString), leftInteger => TransformExtractedLeft(leftInteger));

            Console.WriteLine($"the result of the transform is {resultOfTransform}");

            amount = number;
            
            resultOfTransform = amount.BiBind(rightString => TransformExtractedRight(rightString), leftInteger => TransformExtractedLeft(leftInteger));
            Console.WriteLine($"the result of the transform is {resultOfTransform}");


            Either<int, string> TransformExtractedRight(string rightString)
            {
                Either<int, string> ret = rightString + " is a word, i think";
                return ret;
            }

            Either<int, string> TransformExtractedLeft(int leftInteger)
            {
                Either<int, string> ret = leftInteger + 1;
                return ret;
            }
        }
        
    }      
}
