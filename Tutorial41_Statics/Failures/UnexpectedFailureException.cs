
using System;

namespace Tutorial41
{
    public class UnexpectedFailureException : Exception, IAmFailure
    {
        public UnexpectedFailureException(IAmFailure failure): base(failure.Reason)
        {
            Reason = failure.Reason;
        }

        public string Reason { get; set; }
        public static IAmFailure Create(IAmFailure failure) => new UnexpectedFailureException(failure);
    }
}
