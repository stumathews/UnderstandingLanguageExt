
namespace Tutorial41
{
    public class ConditionNotSatisfiedFailure : IAmFailure
    {
        public ConditionNotSatisfiedFailure(string caller = "Caller not captured")
        {
            Reason = $"Condition not satisfied. Caller: {caller}";
        }
        public string Reason { get; set; }
        public static IAmFailure Create(string caller) => new ConditionNotSatisfiedFailure(caller);
    }
}
