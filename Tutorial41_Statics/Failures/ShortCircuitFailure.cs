
namespace Tutorial41
{
    public class ShortCircuitFailure : IAmFailure
    {
        public ShortCircuitFailure(string message)
        {
            Reason = message;
        }

        public string Reason { get; set; }
        public static IAmFailure Create(string msg) => new ShortCircuitFailure(msg);
    }
}
