using System;

namespace Tutorial40
{
    public class ExternalLibraryFailure : IAmFailure
    {
        public ExternalLibraryFailure(Exception exception)
        {
            Reason = exception.Message;
            Exception = exception;
        }

        public ExternalLibraryFailure(string message)
        {
            Reason = message;
        }

        public string Reason { get; set; }
        public Exception Exception { get; }

        public static IAmFailure Create(string message) => new ExternalLibraryFailure(message);
        public static IAmFailure Create(Exception e) => new ExternalLibraryFailure(e);
    }
}