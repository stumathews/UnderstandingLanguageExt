using System;

namespace Tutorial01
{
    class Program
    {
        static void Main(string[] args)
        {
            // A Box, can hold any type but in this case it holds and makes provision for an int. 
            // Look at the implementation of the Box, notice its just a C# Class I've created.
            Box<int> myNumberBox1 = new Box<int>(1);
           
            // I can look into the Box
            Console.WriteLine($"The contents of my NumberBox is initially is '{myNumberBox1.Item}'");

            // But this Box is different. It can be used in a Linq Expression. 
            // This is because it has a special function defined for it call Select(). Have a look at Box class again, check out the extension method at the bottom.
            // This single function allows the following Linq usages
            var result = from number1 in myNumberBox1
                select number1 + 1; // This is call the Linq Expression Syntax

            // This is called the Linq Fluent syntax
            Box<int> result2 = myNumberBox1.Select(x => x + 1); // x=> x+1 is the transformation function also known as a mapping function

            // Have a Look at Box class again, and specifically the Select() extension method again and try and understand what its doing.

            Console.WriteLine($"The number is now after having passed a mapping function1 to it results in its contents being '{result.Item}'");
            Console.WriteLine($"The number is now after having passed a mapping function2 to it results in its contents being '{result2.Item}'");
        }
    }
}
