using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LanguageExt;

namespace Tutorial41
{
    public static class Statics
    {      

        /// <summary>
        /// Reduces multiple failures into one failure ie aggregates it
        /// </summary>
        /// <param name="eithers"></param>
        /// <returns></returns>
        public static Either<IAmFailure, T> AggregateFailures<T>(this IEnumerable<Either<IAmFailure, T>> eithers, T left)
        {
            List<IAmFailure> GetFailed() => eithers.Lefts().ToList();
            return GetFailed().Any() 
                ? new AggregatePipelineFailure(GetFailed()).ToEitherFailure<T>() 
                : left.ToEither();
        }

        /// <summary>
        /// Returns either an AggregatePipelineFailure or the orignal list of eithers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eithers"></param>
        /// <returns></returns>
        public static Either<IAmFailure, IEnumerable<Either<IAmFailure, T>>> AggregateFailures<T>(this IEnumerable<Either<IAmFailure, T>> eithers)
        {
            var es = eithers.ToList();
            var failed = es.Lefts().ToList();

            return failed.Any() 
                ? AggregatePipelineFailure.Create(failed).ToEitherFailure<IEnumerable<Either<IAmFailure, T>>>() 
                : es.AsEnumerable().ToEither();
        }

        public static Either<IAmFailure, Unit> AggregateUnitFailures(this IEnumerable<Either<IAmFailure, Unit>> failures)
        {
            var failed = failures.Lefts().ToList();
            return failed.Any() ? new AggregatePipelineFailure(failed).ToEitherFailure<Unit>() : Nothing.ToEither();
        }

        public static IAmFailure AsFailure(this Exception e) 
            => new ExceptionFailure(e);

        /// <summary>
        /// Make Either<IFailure, T> in right state
        /// </summary>
        public static Either<IAmFailure, T> ToEither<T>(this T value)
            => Prelude.Right<IAmFailure, T>(value);

        public static Either<L, T> ToEither<L, T>(this T value)
            => Prelude.Right<L, T>(value);

        public static Either<IAmFailure, T> ToEither<T>(this Option<T> value)
            => value.Match(Some: (t)=>Prelude.Right(t), None: ()=>ShortCircuitFailure.Create("None").ToEitherFailure<T>());

        public static Either<IAmFailure, T> ToEither<T>(this Option<T> value, string FailureReason)
            => value.Match(Some: (t)=>Prelude.Right(t), None: ()=>ShortCircuitFailure.Create(FailureReason).ToEitherFailure<T>());

        /// <summary>
        /// Make an IFailure to a Either&lt;IFailure, T&gt; ie convert a failure to a either that represents that failure ie an either in the left state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="failure"></param>
        /// <returns></returns>
        public static Either<IAmFailure, T> ToEitherFailure<T>(this IAmFailure failure)
            => Prelude.Left<IAmFailure, T>(failure);

        public static Either<L, R> ToEitherFailure<L, R>(this L left)
            => Prelude.Left<L, R>(left);


        /// <summary>
        /// Runs code that is contains external dependencies and 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Either<IAmFailure, Unit> Ensure(Action action)
            => action.TryThis();
        
        public static Either<IAmFailure, T> Ensure<T>(T arg, Action<T> action)
            => action.TryThis<T>(arg);

        /// <summary>
        /// Ensures code that might throw exceptions doesn't and returns IFailure instead.
        /// This is so that we know exactly outcomes a function can have ie IFailure or Unit
        /// </summary>
        /// <typeparam name="L">Type of the left side of either</typeparam>
        /// <param name="action">Function to run</param>
        /// <param name="failure">instance of the left hand side considered a failure</param>
        /// <returns></returns>
        public static Either<L, Unit> Ensure<L>(Action action, L failure)
            => action.TryThis<L>(failure);

        public static Either<IAmFailure, Unit> Ensure(Action action, string ifFailedReason)
            => action.TryThis(UnexpectedFailure.Create(ifFailedReason));

        public static Either<IAmFailure, T> EnsureWithReturn<T>(Func<T> action)
            => action.TryThis();

        public static Either<L, T> EnsureWithReturn<L, T>(Func<T> action, L left)
            => action.TryThis<L, T>(left);

        public static Either<IAmFailure, T> EnsureWithReturn<T>(T arg, Func<T, T> action, bool returnInput = false)
            => action.TryThis<T>(arg, returnInput);

        
        // not tested directly
        internal static Either<L, T> _ensuringBindReturn<L, T>(Func<T> action, L left) where L : IAmFailure
            => action._ensuringBindTry<L, T>(left);

        public static void IfFailed<R>(this Either<IAmFailure, R> either, Action<IAmFailure> action)
            => either.IfLeft(action);

        public static Option<bool> ToOption(this bool thing) 
            => thing 
            ? Option<bool>.Some(true) 
            : Option<bool>.None;

        public static Option<Unit> WhenTrue(Func<bool> predicate)
            => predicate() 
            ? Option<Unit>.Some(Unit.Default) 
            : Option<Unit>.None;

        public static Option<bool> Maybe(Func<bool> predicate) 
            => predicate() 
            ? Option<bool>.Some(true) 
            : Option<bool>.Some(false);

        public static Option<T> ToSome<T>(this T t) 
            => t == null ? Prelude.None : Prelude.Some<T>(t);

        public static Option<T> ToOption<T>(this T thing) 
            => thing != null 
            ? Option<T>.Some(thing) 
            : Option<T>.None;
                
        /// <summary>
        /// Captures exceptions and returns a failure
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Either<IAmFailure, Unit> TryThis(this Action action)
            => new Try<Unit>(() => { action(); return new Unit(); })
                .Match(unit => unit.ToEither(), exception => new ExternalLibraryFailure(exception));

        public static Either<IAmFailure, T> TryThis<T>(this Action<T> action, T arg)
            => new Try<T>(() => { action(arg); return default(T); })
                .Match(unit => unit.ToEither(), exception => new ExternalLibraryFailure(exception));

        public static Either<IAmFailure, T> TryThis<T>(this Func<T, T> action, T arg, bool returnArg = false)
            => new Try<T>(() => returnArg? arg : action(arg))
                .Match(unit => unit.ToEither(), exception => new ExternalLibraryFailure(exception));

        public static Either<IAmFailure, T> TryThis<T>(this Action action, T arg, bool returnArg = false)
            => new Try<T>(() =>
                {
                    if (returnArg)
                        return arg;
                    action();
                    return arg;
                })
                .Match(unit => unit.ToEither(), exception => new ExternalLibraryFailure(exception));

       public static Either<IAmFailure, T> TryThis<T>(this Func<T> action)
            => new Try<T>(() => action())
                .Match(
                    unit => unit == null
                        ? new NotTypeExceptionFailure(typeof(T))
                        : unit.ToEither(),
                    exception => new ExternalLibraryFailure(exception));



        public static Either<L, Unit> TryThis<L>(this Action action, L failure)
        => new Try<Unit>(() => { action(); return Nothing; })
            .Match(
                unit => unit.ToEither<L, Unit>(),
                exception => failure);



        public static Either<L, T> TryThis<L, T>(this Func<T> action, L failure)
            => new Try<T>(() => action())
                .Match(
                    unit => unit == null
                        ? failure.ToEitherFailure<L,T>()
                        : unit.ToEither<L, T>(),
                    exception => failure);

        
      
        // not tested directly
        internal static Either<L, T> _ensuringBindTry<L, T>(this Func<T> action, L failure) where L : IAmFailure
            => new Try<T>(() => action())
                .Match(
                    unit => unit == null
                        ? failure.ToEitherFailure<L, T>()
                        : unit.ToEither<L,T>(),
                    exception => failure.WithException<L>(exception));

        public static L WithException<L>(this L failure,  Exception e) where L : IAmFailure
        {
            failure.Reason = e.Message;
            return failure;
        }

        public static TRight ThrowIfFailed<TLeft, TRight>(this Either<TLeft, TRight> either) where TLeft : IAmFailure
            => either.IfLeft(failure => throw new UnexpectedFailureException(failure));

        public static T ThrowIfNone<T>(this Option<T> option) => option.IfNone(() 
            => throw new UnexpectedFailureException(InvalidDataFailure.Create("None was returned unexpectedly")));

        public static Unit ThrowIfSome<T>(this Option<T> option) => option.IfSome((some) 
            => throw new UnexpectedFailureException(InvalidDataFailure.Create("None was returned unexpectedly")));

        public static TRight ThrowIfFailed<TLeft, TRight>(this Either<TLeft, TRight> either, IAmFailure specificFailure) where TLeft : IAmFailure
            => either.IfLeft(failure => throw new UnexpectedFailureException(specificFailure));

        public static T ThrowIfNone<T>(this Option<T> option, IAmFailure failure)
            => option.IfNone(() => throw new UnexpectedFailureException(failure));

        /// <summary>
        /// Explicitly turns failures into Right values
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <param name="returnAs"></param>
        /// <returns></returns>
        public static Either<L, R> IgnoreFailure<L,R>(this Either<L, R> either, R returnAs )
            => either.IfLeft(returnAs);

        /// <summary>
        /// Explicitly turns a failure into a uint
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="either"></param>
        /// <returns></returns>
        public static Either<L, Unit> IgnoreFailure<L>(this Either<L, Unit> either)
            => either.IfLeft(Nothing);

        public static Either<IAmFailure, Unit> IgnoreFailure<L, R>(this Either<L, R> either) where L : IAmFailure 
            => either.Match(Right: (r) => Nothing.ToEither(), Left: (left) => Nothing.ToEither());

        public static Either<IFailure, Unit> IgnoreFailureOf<IFailure, R>(this Either<IFailure, R> either, Type failureType) 
            => either.Match(Right: (right) => Nothing.ToEither<IFailure, Unit>(),
                            Left: (failure) => WhenTrue(() => failure.GetType() == failureType).ToEither()
                                                .Match(Right: (found) => found.ToEither<IFailure, Unit>(),
                                                        Left: (notFound) => failure.ToEitherFailure<IFailure, Unit>()));

        public static Either<IFailure, R> IgnoreFailureOfAs<IFailure, R>(this Either<IFailure, R> either, Type failureType, R OnFail) 
            => either.BindLeft((failure) => WhenTrue(() => failure.GetType() == failureType)
                     .                      Match(Some: (unit) => OnFail.ToEither<IFailure, R>(), 
                                                  None: () => failure.ToEitherFailure<IFailure, R>()));

        public static Option<Unit> IgnoreNone<T>(this Option<T> option)
            => option.Match(Some: (some)=> Prelude.Some(some), None: ()=>Prelude.Some(Nothing));

        
        /// <summary>
        /// Ensuring map will return either a transformation failure or the result of the transformation
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="either"></param>
        /// <param name="transformingFunction"></param>
        /// <returns></returns>
       public static Either<IAmFailure, T> EnsuringMap<L, T>(this Either<IAmFailure, L> either,
            Func<L, T> transformingFunction)
            => either.Map((r) => EnsureWithReturn(()
                        => transformingFunction(r),
                    TransformExceptionFailure.Create("An exception occured while ensuring a map")))
                .Match(
                    Left: failure => failure.ToEitherFailure<T>(),
                    Right: datas => datas.Match(
                        Left: failure => failure.ToEitherFailure<T>(),
                        Right: t => t.ToEither<T>()));

       
        public static Either<IAmFailure, T> EnsuringBind<R, T>(this Either<IAmFailure, R> either, Func<R, Either<IAmFailure, T>> transformingFunction) 
            => either 
                    .Bind(f: right => _ensuringBindReturn(() => transformingFunction(right), TransformExceptionFailure.Create("An exception occured while ensuring a bind")))
                    .Match( Left: failure => failure.ToEitherFailure<T>(),
                            Right: eitherData 
                                => eitherData.Match( Left: failure => failure.ToEitherFailure<T>(),
                                                Right: t => t.ToEither()));
        public static Either<IAmFailure, Unit> EnsuringBind(Func<Either<IAmFailure, Unit>> action) 
            => action.TryThis()
                .Match(
                    Left: failure => failure.ToEitherFailure<Unit>(), 
                    Right: unit => unit.Match(
                        Left:failure => failure.ToEitherFailure<Unit>(),
                        Right: unit1 => unit1));

        public static Either<IAmFailure, Unit> Nothingness(Action action)
        {
            action();
            return Nothing;
        }
        public static Either<IAmFailure, R> EnsuringBind<R>(Func<Either<IAmFailure, R>> action) 
            => action.TryThis()
                .Match(
                    Left: failure => failure.ToEitherFailure<R>(),
                    Right: unit => unit.Match(
                        Left: failure => failure.ToEitherFailure<R>(),
                        Right: unit1 => unit1));

        public static Either<IAmFailure, R> Protect<R>(this Either<IAmFailure, R> either) 
            => TryThis(()=> either)
            .Match(
                    Left: failure => failure.ToEitherFailure<R>(),
                    Right: unit => unit.Match(
                        Left: failure => failure.ToEitherFailure<R>(),
                        Right: unit1 => unit1));

        /// <summary>
        /// A Unit
        /// </summary>
        public static Unit Nothing 
            => new Unit(); 

        public static Unit Success
            => new Unit();

        /// <summary>
        /// Tests if either two objects match a condition and if so then run a function
        /// </summary>
        /// <typeparam name="T">type of either object</typeparam>
        /// <param name="one">object1</param>
        /// <param name="two">object2</param>
        /// <param name="matches">matching function that tests the objects</param>
        /// <param name="then">function that runs when either is a match, the first match is used as a parameter</param>
        /// <returns>a list of the matching objects, ie matching function suceeds on some objects</returns>
        public static List<T> IfEither<T>(T one, T two, Func<T, bool> matches, Action<T> then)
        {
            var objects = new[] {one, two};
            var matched = objects.Where(matches).ToList();
            if (matched.Count > 0) 
                then(matched.First());
            return matched;
        }              

    }
}

