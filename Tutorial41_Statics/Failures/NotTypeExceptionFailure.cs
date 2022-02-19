
using System;

namespace Tutorial41
{
    public class NotTypeExceptionFailure : IAmFailure
    {
        public NotTypeExceptionFailure(Type type)
        {
            Reason = $"Function did not return expected type of '{type}'";
        }

        public string Reason { get; set; }

        public static IAmFailure Create(Type type) => new NotTypeExceptionFailure(type);
    }
}
