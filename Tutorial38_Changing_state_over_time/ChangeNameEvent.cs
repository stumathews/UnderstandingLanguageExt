namespace Tutorial37
{
    public class ChangeNameEvent:  Event<Person>
    {
        public ChangeNameEvent(string name)
        {
            NewName = name;
            EventName = "ChangedNameEvent";
            EventDescrition = "Changes a Persons name";
        }

        public string NewName { get; }

        public override Person ApplyEventTo(Person person)
        {
            Person updated = new Person(person);
                   updated.Name = NewName;
            updated.History.Add($"\nChanged name to {NewName}");
            return updated;
        }
    }
}
