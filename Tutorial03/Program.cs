using System;
using Tutorial01;

namespace Tutorial03
{
    // We continue working on our Monad type, Box.
    class Program
    {
        static void Main(string[] args)
        {

            // Before working further on out Monad Box, read this : Why do we actually need Monads? Why do we have to have Map() and Bind() - here is why? https://stackoverflow.com/questions/28139259/why-do-we-need-monads

            Box<int> numberHolder = new Box<int>(25);
            Box<string> stringHolder = new Box<string>("Twenty Five");

            //Validate Extract Transform lift
            Box<string> result1 = numberHolder.Map(i => "Dude Ive Been Transformed To AString automatically");
            
            // Transform the contents of the box by passing it down a series of Bind()s so there are multiple associated VETL->VETL->VETL steps that represents the Binds(), effectively representing a pipeline of data going in and out of Bind(VETL) functions
            Box<string> result2 = stringHolder
                .Bind(s => new Box<int>(2)) //Validate Extract Transform lift
                .Bind(i => new Box<int>()) // Validate step only  
                .Bind(i => new Box<string>("hello")); // Validate step only 

            Console.WriteLine($"The contents of result 2 is {result2.Item}");
        }
    }
}
