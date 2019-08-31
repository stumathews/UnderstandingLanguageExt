using System.Collections.Generic;


namespace Tutorial37
{
    public class Person
    {
        public int Age {get;set;} = 0;
        public string Name {get;set;} = "NoNameSet";
        public string Expertise {get;set;} = "NoExpertiseSet";
        public string Role {get;set;} = "NoRoleSet";
        public Person() 
            => Age = 0;

        public Person(Person p) : this()
        {
            Age = p.Age;
            History = p.History;
            Name = p.Name;
            Expertise = p.Expertise;
            Role = p.Role;
        }

        public List<string> History = new List<string>();
        public override string ToString() 
            => $"Person's name is {Name} and is {Age} years old now. Expertise is '{Expertise}', while Role is '{Role}'\nPrevious state chnges were: {string.Join(',', History)}";
    }
}
