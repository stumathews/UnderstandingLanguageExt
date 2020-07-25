using System;
using LanguageExt;

namespace Tutorial40
{
    
    public static class Extensions
    {
        /// <summary>
        /// Allows you to express any value as an Either
        /// </summary>
        /// <typeparam name="L">The left value - usually a failure representative</typeparam>
        /// <typeparam name="R">the right value - usually a success representative</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Either<L, R> ToEither<L, R>(this R value)
            => Prelude.Right<L, R>(value);
    }
}

