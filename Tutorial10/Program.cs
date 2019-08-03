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
            
            Box<Portfolio[]> pipelineResult1 = new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
                .Bind(ports =>                 new Box<Portfolio[]>(PopulatePortfolioHoldings(ports, HoldingFrom)))
                .Map(ports => DoSoething(ports).ToArray());
            
            Box<Portfolio[]> pipelineResult2 = from ports in  new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds))
                                               from ports1 in new Box<Portfolio[]>(PopulatePortfolioHoldings(ports, HoldingFrom)) 
                                        select DoSoething(ports1);     
        }
        

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
            => new Box<Portfolio[]>(GetPortfoliosByIds(portfolioIds));

        
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
        public DateTime[] Holdings{get;set;}
        public Portfolio(string name, int id)
        {
            Id = id;
            Name = name;
        }
    }
}
