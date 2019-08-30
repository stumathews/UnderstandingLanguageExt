using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.DataTypes.Serialisation;


namespace Tutorial37
{
    class Person
    {
        public int Age {get;set;}
        public Person()
        {
            Age = 0;
        }
        public Person(Person p) : this()
        {
            this.Age = p.Age;
            this.History = p.History;
        }

        public List<string> History = new List<string>();
        public override string ToString()
        {
            return $"Person is {Age} years old now. Previous state chnges were: {string.Join(',', History)}";
        }
    }

    // This tutorial shows how you can use the Fold() function in languageExt to change the state of an object over time
    class Program
    {
        static void Main(string[] args)
        {            
            
            List<int> years = new List<int>{ 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000,
                                            2001,2002,2003,2004,2005,2006,2007,2008,2009,2010,2011,2012,2013,2014,2015,2016,2017,2018,2019};
            Person person = new Person();
            
            
            // A state (InitialResult) changes over time and it changes using results of the previous change. It uses an item from the array in changing the state each time.
            // The state changes the number of elements in the IEnumerable
            // For a Lst which has multiple items in it, the state will change that many times

            // NB: years represent the state changes that will occur
            var changedPerson = years.Fold(/*initial state*/ person, (previousResult, extract) => ChangeState(extract, previousResult));
            
            // The result is the last state change
            Console.WriteLine($"The result value is {changedPerson}");

            // local function
            Person ChangeState(int year, Person previousResult)
            {                
                Person n = new Person(previousResult);                
                       n.History.Add($"\nIn {year}, this person was {n.Age} years old");                
                       n.Age++;
                return n;
            }                 
        } 
    }      
}
