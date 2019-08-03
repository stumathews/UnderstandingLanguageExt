using System;
using System.Collections.Generic;
using System.Linq;
namespace Tutorial09
{
    // Procedural thinking -> Pipeline thinking
    class Program
    {
        static void Main(string[] args)
        {
            // 2 Boxes!
            Box<int[]> boxOfIntegers = new Box<int[]>(new[] { 3, 5, 7, 9, 11, 13, 15 });
            Box<int[]> boxOfNewIntegers = new Box<int[]>(new[] { 3, 5, 88, 29, 155, 123, 1 });

            int[] portfolioIds = new int[]{ 77,88,99 };
            DateTime HoldingFrom = new DateTime(2019, 07, 25);

            /* Procedural */

            // 1. Go get the portfolio names for the following portfolio ids:
            Portfolio[] portfolios = GetPortfoliosByIds(portfolioIds);
            
            // 2. Get the holdings for these portfolios which are dated later than xyz
            Portfolio[] portfoliosWithHoldings = PopulatePortfolioHoldings(portfolios, HoldingFrom);
            
            // 3. do something with these populated portfolios
            var result = DoSoething(portfoliosWithHoldings);
            

            /* Pipline way of thinking */

            // Linq Fluent syntax way ie not using SelectMany()
            Box<Portfolio[]> pipelineResult1 = GetPortfoliosByIds1(portfolioIds) // note same input, portfolioIds can be used but this is returning a box now
                .Bind(ports => PopulatePortfolioHoldings1(ports, HoldingFrom)) // notice you can use the same name of the transformed item as the last(ports)
                .Map(ports => DoSoething(ports).ToArray());

            // Linq Expression syntax way ie using SelectMany()
            Box<Portfolio[]> pipelineResult2 = from ports in GetPortfoliosByIds1(portfolioIds)
                                       from ports1 in PopulatePortfolioHoldings1(ports, HoldingFrom) // notice you cant use the name of last transform again
                                        select DoSoething(ports1);

            /* Notes:
             * When using a Monad like Box in a SelectMany statement like above, remember the implemntation of te Monads SelectMany():
             *  for each bind() phase of the SelectMany(), that phase requires a function such PopulatePortfolioHoldings1() and any following ones
             *  to transform the extracted item and then put it back into a Box to thats why each function must return a Box<> in the pipeline.
             *  Also note how the logical way of planning the steps can be replicated in both the procedural and pipeline ways (you dont have to think diffirently!)
             *
             */

           // but wait I can make a pipeline too, right?
           
            var result1 = DoSoething(PopulatePortfolioHoldings(GetPortfoliosByIds(portfolioIds), HoldingFrom)); // yeah but not (V)ET aka short circuit ability
            // that being said you could do it, it would mean every function would need to check for invalid data whereas in a Monad like Box,
            // that validation is built into the Select and SelectMany() implementation as thus us automatic on each usage of those function on the monad!
           
        }

        /// <summary>
        /// Scottish for do something
        /// </summary>
        private static Portfolio[] DoSoething(Portfolio[] portfoliosWithHoldings)
        {
            throw new NotImplementedException();
        }

        private static Portfolio[] PopulatePortfolioHoldings(Portfolio[] portfolios, DateTime holdingFrom)
        {
            foreach( var portfolio in portfolios)
            {
                portfolio.Holdings = GetPortfolioHoldingsFrom(portfolio, holdingFrom);
            }
            return portfolios;
        }

        /// <summary>
        /// Returns a Box
        /// </summary>
        private static Box<Portfolio[]> PopulatePortfolioHoldings1(Portfolio[] portfolios, DateTime holdingFrom)
        {
            var listOfPortfolios = PopulatePortfolioHoldings(portfolios, holdingFrom);
            return new Box<Portfolio[]>(listOfPortfolios);                    
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

            return portfolios.Where(x=>portfolioIds.Contains(x.Key))
                .Select(x=>x.Value)
                .ToArray();
        }

        public static Box<Portfolio[]> GetPortfoliosByIds1(int[] portfolioIds)
        {
            Dictionary<int, Portfolio> portfolios = new Dictionary<int, Portfolio> 
            {
                { 1, new Portfolio("Portfolio1",1) },
                { 2, new Portfolio("Portfolio2", 2) },
                { 77, new Portfolio("Portfolio77", 77) },
                { 88, new Portfolio("Portfolio88", 88) },
                { 99, new Portfolio("Portfolio99", 99) },
            };

            var listofPortfolios = portfolios.Where(x=>portfolioIds.Contains(x.Key))
                .Select(x=>x.Value)
                .ToList();
            return new Box<Portfolio[]>(listofPortfolios.ToArray());
        }
    }

    class Portfolio
    {
        public string Name {get;set;}
        public int Id {get;set;}
        public DateTime[] Holdings{get;set;}
        public Portfolio(string name, int id)
        {
            Id = id;
            Name = name;
        }
    }
}
