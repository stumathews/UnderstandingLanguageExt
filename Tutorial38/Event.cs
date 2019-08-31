namespace Tutorial37
{
    public abstract class Event<T>
    {
        public string EventName {get;set;}
        public string EventDescrition {get;set;}
        public abstract T ApplyEventTo(T thing);
    }
}
