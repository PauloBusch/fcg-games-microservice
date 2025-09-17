namespace FCG.Games.Domain;

public class Evaluation : EntityBase
{
    public int Stars { get; private set; }

    public string? Comment { get; private set; }

    public virtual User User { get; private set; }
}
