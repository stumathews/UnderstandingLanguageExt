using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial40
{
    public interface IAmFailure
    {
        string Reason { get; set; }
    }

    public class GenericFailure : IAmFailure
    {
        public GenericFailure(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; set; }
    }
}
