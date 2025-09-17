namespace FCG.Games.Domain;

public class Game : EntityBase
{
    public Game(
        Guid key,
        string title,
        string description,
        Catalog catalog
    ) : base(key)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        Catalog = catalog;
    }

    public Guid Key { get; protected set; }
    
    public string Title { get; private set; }
    
    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public Catalog Catalog { get; private set; }

    public IReadOnlyCollection<Evaluation> Evaluations { get; private set; }
    
    public IReadOnlyCollection<Download> Downloads { get; private set; }
}
