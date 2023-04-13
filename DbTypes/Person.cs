using DbTypes.Interfaces;

namespace DbTypes;

public class Person : IIdentification
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime UtcBirthday { get; }
    public double Age => (DateTime.UtcNow - UtcBirthday).TotalSeconds;

    public Person(string name, DateTime birthday)
    {
        Id = Guid.NewGuid();
        Name = name;
        UtcBirthday = birthday.ToUniversalTime();
    }
}