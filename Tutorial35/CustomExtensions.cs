using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanguageExt;

namespace Tutorial35
{
    // Note a 'Successful' either is in the Right state while a 'unsucessful' either is in Left state
    public static class EitherExtensions
    {
       
        /// <summary>
        /// Throws an exception if the either is in left state ie failed
        /// </summary>
        public static TRight ThrowIfFailed<TLeft, TRight>(this Either<TLeft, TRight> either) where TLeft : IAmFailure 
            => either.IfLeft(failure => throw new UnexpectedFailureException(failure));
        
        //Given Either in Failure state (IAmFailure) to None, if the check on the failure fails - ie checking if IAmFailure is suitable
        public static Either<IAmFailure, Option<T>> FailureToNone<T>(this Either<IAmFailure, Option<T>> item, Func<IAmFailure, bool> predicate) 
            => item.Match(rightValue => rightValue, failure => !predicate(failure) 
                                                                            ? Prelude.Left<IAmFailure, Option<T>>(failure) 
                                                                            : Option<T>.None, null);
        
        // Given Either with an optional Right state. If right state is none, make a failure using provided failure, otherwise use the valid value as the valid/right value of the Either
        public static Either<IAmFailure, T> NoneToFailure<T>(this Either<IAmFailure, Option<T>> item, Func<IAmFailure> failure) 
            => item.Bind(e => e.Match(value => value.ToSuccess<T>(), () => Prelude.Left<IAmFailure, T>(failure())));
        
        /// <summary>
        /// Given An Either with an optional Right value. If option value is valid/some, make a Either Failure using provided failure, otherwise a Successful Either
        /// </summary>
        public static Either<IAmFailure, Option<T>> SomeToFailure<T>(this Either<IAmFailure, Option<T>> item, Func<IAmFailure> failure)
            => item.Bind(either => either.Match(_ => Prelude.Left<IAmFailure, Option<T>>(failure()), () => either.ToSuccess()));
        
        public static Func<IEnumerable<Option<T>>, Either<IAmFailure, IEnumerable<T>>> NoneToFailure<T>(Func<IAmFailure> failure) 
            => items => items.Sequence<T>().Match(item => item.ToSuccess<IEnumerable<T>>(), () => Prelude.Left<IAmFailure, IEnumerable<T>>(failure()));

        /// <summary>
        /// Make a Either<IAmFailure, T> in left(failure) state if the list is empty (use failure producing function provided), otherwise return the either in right state using the first item in the list as the light value in the either
        /// </summary>
        public static Either<IAmFailure, T> HeadOrFailure<T>(this IEnumerable<T> list, Func<IAmFailure> failure) 
            => list.Match(() => Prelude.Left<IAmFailure, T>(failure()), s => Prelude.Right<IAmFailure, T>(s.First()));

        /// <summary>
        /// Make Either<IAmFailure, T> in right state
        /// </summary>
        public static Either<IAmFailure, T> ToSuccess<T>(this T value) 
            => Prelude.Right<IAmFailure, T>(value);

        // Make an IAmFailure to a Either<IAmFailure, T> ie convert a failure to a either that represents that failure ie an either in the left state
        public static Either<IAmFailure, T> ToEither<T>(this IAmFailure failure) 
            => Prelude.Left<IAmFailure, T>(failure);

    }

    public class UnexpectedFailureException : Exception
    {
        public UnexpectedFailureException(IAmFailure failure)
        {
            
        }
    }
}
