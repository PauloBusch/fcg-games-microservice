namespace FCG.Games.Domain;

public class User(string name) : EntityBase
{
    public string Name { get; private set; } = name;
}
