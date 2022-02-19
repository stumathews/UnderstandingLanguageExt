using LanguageExt;
using System;

namespace Tutorial41
{
    // This tutorial shows you how to protect binding transformations from exceptions,
    // and also override how the problem is surfaced to the user.
    class Program
    {
        static void Main(string[] args)
        {
            // Create some products based on an assembly line (pipeline)
            var product1 = GetAnalysisResult()
                            .Bind(analysisResult => GetDesignResult())
                            .Bind(designResult => GetImplementationResult());

            // Design fails internally by throwing an exception:
            var product2 = GetAnalysisResult()
                            .EnsuringBind(analysisResult => GetDesignResult(throwException: true)) // We can Ensure the transformation function against exceptions
                            .Bind(designResult => GetImplementationResult()); // We assume there aren't any exceptions here 

            // Implementation fails internally by throwing an exception:
            var product3 = GetAnalysisResult()
                            .Bind(analysisResult => GetDesignResult())
                            .EnsuringBind(designResult => GetImplementationResult(throwException: true));

            
            // However sometimes the internal error caused somewhere in the guts of the function isn't as useful,
            // and re-interpreting it to add more context at a higher level is more useful...
            
            // Same process but you can override how internal error is surfaced/presented using MapLeft:

            var product4 = GetAnalysisResult()
                            .EnsuringBind(analysisResult => GetDesignResult(throwException: true))  
                                // Whatever the left value is, override/customize/surface it diffirently as a new Failure and add better context, ie product4
                                .MapLeft( failure => UnexpectedFailure.Create($"Design Error Occured while creating product 4. Details were '{failure.Reason}'")) 
                            .Bind(designResult => GetImplementationResult());
            var product5 = GetAnalysisResult()
                            .Bind(analysisResult => GetDesignResult())
                            .EnsuringBind(designResult => GetImplementationResult(throwException: true))
                                // Whatever the left value is, override/customize/surface it diffirently as a new Failure and add better context, ie product5
                                .MapLeft( failure => UnexpectedFailure.Create($"Implementation Error Occured while creating product 5. Details were '{failure.Reason}'")); 
            
            Console.WriteLine($"Product1 result: {product1}");
            Console.WriteLine($"Product2 result: {product2}");
            Console.WriteLine($"Product3 result: {product3}");
            Console.WriteLine($"Product4 result: {product4}");
            Console.WriteLine($"Product5 result: {product5}");


        }

        /// <summary>
        /// Do some Analysis Work
        /// </summary>
        /// <returns></returns>
        public static Either<IAmFailure, string> GetAnalysisResult()
        {
            // Do some analysis work...
            return "Analysis Success";
        }

        /// <summary>
        /// Do some design work
        /// </summary>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static Either<IAmFailure, string> GetDesignResult(bool throwException = false)
        {
            // Do some design work...
            return throwException 
                ? throw new UnexpectedFailureException(UnexpectedFailure.Create("Design Width is too large. Error DES623 Ref=KJE871"))
                : "Design Success";
        }

        /// <summary>
        /// Do some implementation
        /// </summary>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static Either<IAmFailure, string> GetImplementationResult(bool throwException = false)
        {
            // Do some implemantion work...
            return throwException 
                ? throw new UnexpectedFailureException(UnexpectedFailure.Create("Implementation Volume exceeds mainframe size. Error IMP371 Ref=K763P"))
                : "Implementation Success";
        }
    }   
}
