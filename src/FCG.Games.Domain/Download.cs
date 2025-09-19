namespace FCG.Games.Domain;

public class Download(DateTime date) : EntityBase
{
    public DateTime Date { get; private set; } = date;
}
