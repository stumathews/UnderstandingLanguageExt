

using System;

namespace Tutorial41
{
    public class ExceptionFailure : Exception, IAmFailure
    {
        public ExceptionFailure(Exception e) => Reason = e.Message;
        public string Reason { get; set; }
    }
}
