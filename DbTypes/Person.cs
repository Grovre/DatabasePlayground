using DbTypes.Interfaces;

namespace DbTypes;

/// <summary>
/// Base class defining the basic traits of a person. Includes a name and birthday
/// </summary>
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