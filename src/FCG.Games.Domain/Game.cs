namespace FCG.Games.Domain;

public class Game(
    Guid? key,
    string title,
    string description,
    Catalog catalog
) : EntityBase(key)
{
    public string Title { get; private set; } = title;

    public string Description { get; private set; } = description;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private set; }

    public Catalog Catalog { get; private set; } = catalog;

    public IReadOnlyCollection<Evaluation> Evaluations { get; private set; } = [];

    public IReadOnlyCollection<Download> Downloads { get; private set; } = [];
}
