
namespace Tutorial41
{
    public class NotFound : IAmFailure
    {
        public NotFound(string message)
        {
            Reason = message;
        }
        public string Reason { get; set; }

        public static IAmFailure Create(string message) => new NotFound(message);
    }
}
