namespace Tutorial37
{
    public class ChangeExpertiseEvent : Event<Person>
    {
        public string NewExpertise {get;set;}
        public ChangeExpertiseEvent(string newExpertise)
        {
            NewExpertise = newExpertise;
        }
        public override Person ApplyEventTo(Person person)
        {
            var updated = new Person(person);
                updated.Expertise = NewExpertise;
                updated.History.Add($"\nChanged Expertise to {NewExpertise}");
            return updated;
        }
    }
}
