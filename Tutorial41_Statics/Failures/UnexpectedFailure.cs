
namespace Tutorial41
{
    public class UnexpectedFailure : IAmFailure
    {
        public UnexpectedFailure(string reason)
        {
            Reason = reason;
        }

        public override string ToString()
        {
            return Reason;
        }

        public string Reason { get; set; }
        public static IAmFailure Create(string reason) => new UnexpectedFailure(reason);
    }
}
