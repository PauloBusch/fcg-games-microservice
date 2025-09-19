namespace FCG.Games.Domain;

public class Evaluation(
    uint stars,
    string? comment,
    User user
) : EntityBase
{
    public uint Stars { get; private set; } = stars;

    public string? Comment { get; private set; } = comment;

    public User User { get; private set; } = user;
}
