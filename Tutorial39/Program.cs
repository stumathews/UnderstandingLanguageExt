using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LanguageExt.DataTypes.Serialisation;


namespace Tutorial39
{
    // This tutorial demonstrates creating immutable objects, and using them as you would use any other OOP objects.
    // However, this object is immutable in as much as its specifically designed not to have its state changed by operations
    // The operations that it does have, create a new object with the modification and leaves the origninal object untouched.
    class Program
    {
        static void Main(string[] args)
        {
            // Note we cannot create a person through a conventional constructor
            // we use a smart constructor ie Of() to perform validation on construction and return an Option<Person> depending the construction
            // of the person is valid - no exceptions are thrown when they are not - which is usually what happens with normal constructors.

            var originalMe = Person.Of("Steward", "Mathews");
            

            // Perform a state change on the person, hwever we enforce creation of a new copy of person, preinitialized with previous data. 
            // We therefore don't modify state - particularly of the person that we are about to modify - this is not a member function that changes te object as is traditionally done
            var changedMe = originalMe.Bind(person => person.Rename("Steward Rob Charles", "Mathews")); // Remember we're binidng here as Rename returns a Monad already if it didn't we'd use Map()

            // We can do this declararatively also
            var stateChangedMe1 = from person in originalMe
                                  from renamedperson in person.Rename("Steward Rob Charles", "Mathews")
                                  select renamedperson;

            // We can also chain the modifications using the extension method

            var lastChange = changedMe
                .Rename("Stuart", "Mathews")
                .Rename("Stuart Robert Charles", "Mathews");

            Console.WriteLine($"First I was {originalMe}, then ultimately I was {lastChange}");
            
        }
    }   
}

