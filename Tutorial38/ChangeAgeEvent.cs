namespace Tutorial37
{
    public class ChangeAgeEvent : Event<Person>
    {
        public ChangeAgeEvent(int newAge)
        {
            NewAge = newAge;
            EventDescrition = "\nContains inforamtion about changing a persons age";
            EventName = "ChangeAgeEvent";
        }

        public int NewAge { get; }

        public override Person ApplyEventTo(Person person)
        {
            Person updated = new Person(person);
                updated.Age = NewAge;
            updated.History.Add($"\nChanged age to " + NewAge);
            return updated;
        }
    }
}
