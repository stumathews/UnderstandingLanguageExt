using System;
using LanguageExt;


namespace Tutorial39
{
    /// <summary>
    /// This is an immutable object because:
    /// a) Has private setters and that means its state cannot be changed after creation
    /// b) All properties are immutable - either strings or native types or System.Collections.Immutable types
    /// c) Any change that needs to take place must result in a newly create object of this type
    /// </summary>
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        /// <summary>
        /// Notice this is a private constructor so a Person cannot be created normally...there must be another way in!
        /// It must be created indirectly via .Of() or .New() which then can use the private constructor below
        /// </summary>
        /// <param name="firstName">the persons first name</param>
        /// <param name="lastName">The persons last name</param>
        private Person(string firstName, string lastName)
        {
            if (!IsValid(firstName, lastName))
            {
                throw new ArgumentException("Invalid input");
            }
            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString() 
            => $"{FirstName} {LastName}";

        /// <summary>
        ///  This pretends to be a constructor by being the only method allowed to call the constructor.
        ///  The actual and real constructor is only called if input is valid to this function 
        ///  As a result it gaurentees no exceptions can ever be thrown during creation of the object through
        ///  this method.
        /// </summary>
        /// <param name="firstName">Persons first name</param>
        /// <param name="lastName">Persons last name</param>
        /// <returns></returns>
        public static Option<Person> Of(string firstName, string lastName) 
            => IsValid(firstName, lastName) 
                ? Option <Person>.Some(new Person(firstName, lastName)) 
                : Option <Person>.None;

        private static bool IsValid(string firstName, string lastName)
            => !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);

        // Instance method allows this function to be chained.
        // With Helper to make object creation easier as properties of the object change
        public Option<Person> Rename(string firstName = null, string lastName = null) 
            => Of(firstName, lastName);
    }
}

