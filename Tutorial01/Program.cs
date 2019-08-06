using System;

namespace Tutorial01
{
    class Program
    {
        static void Main(string[] args)
        {
            Box<int> myNumberBox1 = new Box<int>(1);
           
            Console.WriteLine($"The contents of my NumberBox is initially is '{myNumberBox1.Item}'");

            var result = from number1 in myNumberBox1
                select number1 + 1;

            Box<int> result2 = myNumberBox1.Select(x => x + 1); // x=> x+1 is the mapping function

            Console.WriteLine($"The number is now after having passed a mapping function1 to it results in its contents being '{result.Item}'");
            Console.WriteLine($"The number is now after having passed a mapping function2 to it results in its contents being '{result2.Item}'");
        }
    }
}
