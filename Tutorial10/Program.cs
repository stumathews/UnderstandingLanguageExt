using System;
using System.Collections.Generic;
using System.Linq;
namespace Tutorial10
{
    // Procedural thinking -> Pipeline thinking
    class Program
    {
        static void Main(string[] args)
        {
            int[] portfolioIds = new int[]{ 77, 88, 99 };
            DateTime HoldingFrom = new DateTime(2019, 07, 25);
                               
            Portfolio[] portfolios = GetPortfoliosByIds(portfolioIds);            
            Portfolio[] portfoliosWithHoldings = PopulatePortfolioHoldings(portfolios, HoldingFrom);
            var result = DoSoething(portfoliosWithHoldings);
            
            // Notice how the result of the Bind or map always returns a Box<>             
            Box<Portfolio[]> pipelineResult1 = new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
                .Bind(ports =>                 new Box<Portfolio[]>(PopulatePortfolioHoldings(ports, HoldingFrom))) // Notice how bind must return a box during transformation
                .Map(ports => DoSoething(ports).ToArray()); // note how map doesn't require the transfomming funtion to return a Box<>, it will so do automatically
            
            Box<Portfolio[]> pipelineResult2 = from ports in  new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
                                               from ports1 in new Box<Portfolio[]>(PopulatePortfolioHoldings(ports, HoldingFrom)) // this transforms the extracted item form Box, Portfolio[] and puts back into box
                                        select DoSoething(ports1);     // This is a select() so it will automatically lift to a Box<>
        }

        /* Some interesting observations:
         * Map and Bind both extract and validate the item within the box (that it's not empty) ie do VETL and then proceeds to run the transform function on it if its not empty, otherwise returns empty box.
         * Map and Both are equivalent in as much as they perform VETL but differ in what form they require their transform function to either lift or not lift the transformation
         * In the Linq Syntax query method, Box's SelectMany() is used to transform successive transformations.
         * You have access to each of the transformed results in subsequent transformations below. 
         * The final select statement is the Box's Select() function and therefor it will automatically be lifted and you dont need to do it.
         * The Fluent mechanism, used Box's Map and Bind functions. 
         * Each Map and Bind has access to the last transformation before it, and unlike the linq expression syntax cannot see beyond the last transformation (as that is the input it gets)
         * transformations from a call to Bind and Map must result in a Box<> either explicitly via Bind() or automatically via Map()
         * Your logical planning or thinking of logical programming tasks in your design can equally be represented procedurally and using pipelining.
         */
        
        // Attach all the holdings for each portfolio where the holding is greated than holdingFrom 
        private static Portfolio[] PopulatePortfolioHoldings(Portfolio[] portfolios, DateTime holdingFrom)
        {
            foreach(var portfolio in portfolios)
            {
                portfolio.Holdings = GetPortfolioHoldingsFrom(portfolio, holdingFrom);
            }
            return portfolios;
        }

               
        private static DateTime[] GetPortfolioHoldingsFrom(Portfolio portfolio, DateTime holdingFrom)
        {
            Dictionary<int, List<DateTime>> portfolioHoldings = new Dictionary<int, List<DateTime>>
            {
                { 1, new List<DateTime> { new DateTime(2019,07,23),new DateTime(2019,07,24), new DateTime(2019,07,25), new DateTime(2019,07,26)  } },
                { 2, new List<DateTime> { new DateTime(2019,07,23),new DateTime(2019,07,24), new DateTime(2019,07,25), new DateTime(2019,07,26)  } },
                { 77, new List<DateTime> { new DateTime(2019,07,23),new DateTime(2019,07,24), new DateTime(2019,07,25), new DateTime(2019,07,26)  } },
                { 88, new List<DateTime> { new DateTime(2019,07,23),new DateTime(2019,07,24), new DateTime(2019,07,25), new DateTime(2019,07,26)  } },
                { 99, new List<DateTime> { new DateTime(2019,07,23),new DateTime(2019,07,24), new DateTime(2019,07,25), new DateTime(2019,07,26)  } },
            };

            // Recap of the Linq Fluent syntax
            return portfolioHoldings
                .Where(x=>x.Key == portfolio.Id)
                .SelectMany(x=> x.Value)
                .Where(x=>x >= holdingFrom).ToArray();
        }

        public static Portfolio[] GetPortfoliosByIds(int[] portfolioIds)
        {
            Dictionary<int, Portfolio> portfolios = new Dictionary<int, Portfolio> 
            {
                { 1, new Portfolio("Portfolio1",1) },
                { 2, new Portfolio("Portfolio2", 2) },
                { 77, new Portfolio("Portfolio77", 77) },
                { 88, new Portfolio("Portfolio88", 88) },
                { 99, new Portfolio("Portfolio99", 99) },
            };

            // Why not throw in some linq expression syntax 
            return (from portfolio in portfolios
                        let portfolioId = portfolio.Key
                        let actual_portfolio = portfolio.Value
                        where portfolioIds.Contains(portfolioId)
                            select actual_portfolio
                    ).ToArray();
        }

        public static Box<Portfolio[]> GetPortfoliosByIds1(int[] portfolioIds) 
            => new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds)); // note how we've just wrapped this into a Box<> - this is a perfect Bind() transformation function thusly

        
        // Scottish for do some thing        
        private static Portfolio[] DoSoething(Portfolio[] portfoliosWithHoldings)
        {
            Console.WriteLine("Good lord, a set of portfolios! What will we do with them?");
            foreach(var portfolio in portfoliosWithHoldings)
            {
                Console.WriteLine($"These are the holdings from the date specificed for portfolio '{portfolio.Name}'");
                foreach(var holding in portfolio.Holdings)
                {
                    Console.WriteLine($"\t{holding}");
                }
            }
            return portfoliosWithHoldings;
        }
    }

    class Portfolio
    {
        public string Name {get;set;}
        public int Id {get;set;}
        public DateTime[] Holdings{get;set;} // A portfolio's holdings are just a series of dates
        public Portfolio(string name, int id)
        {
            Id = id;
            Name = name;
        }
    }
}
