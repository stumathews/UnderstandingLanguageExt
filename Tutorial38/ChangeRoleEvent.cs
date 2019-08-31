namespace Tutorial37
{
    public class ChangeRoleEvent : Event<Person>
    {
        public ChangeRoleEvent(string newRole)
        {
            NewRole = newRole;
        }

        public string NewRole { get; }

        public override Person ApplyEventTo(Person person)
        {
            var updated = new Person(person);
                updated.Role = NewRole;
                updated.History.Add($"\nChanged Role to {NewRole}");
            return updated;
        }
    }
}
