using System;
using Tutorial01;

namespace Tutorial03
{
    // We continue working on our Monad type, Box.
    // Showing how short-circuiting works when chaining or cascading multiple Bind() or Map() operations
    class Program
    {
        static void Main(string[] args)
        {
            // Before working further on out Monad Box, read this : 
            // Why do we actually need Monads? Why do we have to have Map() and Bind()
            // Here is why? https://stackoverflow.com/questions/28139259/why-do-we-need-monads

            Box<int> numberHolder = new Box<int>(25);
            Box<string> stringHolder = new Box<string>("Twenty Five");

            //Validate, Extract, Transform, lift
            Box<string> result1 = numberHolder.Map(i => "Dude Ive Been Transformed To a string automatically");
            
            // Transform the contents of the box by passing it down a series of Bind()s
            // so there are multiple associated VETL->VETL->VETL steps that represents the Binds()
            // effectively representing a pipeline of data going in and out of Bind(VETL) functions
            Box<string> result2 = stringHolder
                .Bind(s => new Box<int>(2)) //Validate Extract Transform lift
                .Bind(i => new Box<int>()) // Validate step only  
                .Bind(i => new Box<string>("hello")); // Validate step only 

            // Remember that the 'validate' step is implicit and is actually coded directly into the Bind() or Map() functions.
            // The validity of a box in this case, is if it is empty or not(look at the Bind functions' code that explicitly checks this),
            // It is empty(invalid) it will not run the the user provided transformation function, otherwise it will.
           
            // This is an example of 'short-circuiting' out-of the 
            // entire set of transformations early. So if the First Bind short-circuits, the next Binds will too and so they will not run

            Console.WriteLine($"The contents of result 2 is {result2.Item}");
        }
    }
}
