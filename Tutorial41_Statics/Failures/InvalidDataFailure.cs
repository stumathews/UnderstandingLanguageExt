
namespace Tutorial41
{
    public class InvalidDataFailure : IAmFailure
    {
        public InvalidDataFailure(string empty)
        {
            Reason = empty;
        }

        public string Reason { get; set; }
        public static IAmFailure Create(string message) => new InvalidDataFailure(message);
    }
}
