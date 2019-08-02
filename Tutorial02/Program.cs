using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tutorial01;

namespace Tutorial02
{
    class Program
    {
        static void Main(string[] args)
        {
            Box<int[]> numbers1 = new Box<int[]>(new []{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
            Box<int[]> numbers2 = new Box<int[]>(new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 });
            
            // Validate, Extract, Transform and YouLift(If Valid)
            Box<string> result1 = numbers1.Bind(i => new Box<string>("Hello"));

            // Validate, Extract, Transform and automatic Lift (If Valid) 
            Box<string> result2 = numbers2.Map(numbers => "Hello");

            Console.WriteLine($"The result of the Bind operation was {result1.Item}");
            Console.WriteLine($"The result of the Map operation was {result2.Item}");
        }
    }
}
