
using LanguageExt;

namespace Tutorial41
{
    internal class UninitializedFailure : IAmFailure
    {
        public UninitializedFailure(string what)
        {
            Reason = what;
        }
        public string Reason { get; set; }
        public static IAmFailure Create(string what) => new UninitializedFailure(what);
        public static Either<IAmFailure, T> Create<T>(string what) => new UninitializedFailure(what);
    }
}
