using LanguageExt;

namespace Tutorial39
{
    // Extension methods allow us to chain non-instance method calls,
    public static class PersonExtensions
    {
        /// <summary>
        /// Functional because 
        /// A) it does not call any non-pure functions or use any other data than the arguments(with are immutable) passed to it.
        /// B) functions should be declared separately from the data they act upon, like it is
        /// C) Creates a new Object 
        /// </summary>
        /// <param name="person"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static Option<Person> Rename(this Option<Person> person, string firstName, string lastName)
        {
            return person.Bind(per => per.Rename(firstName, lastName));
        }
    }
}

