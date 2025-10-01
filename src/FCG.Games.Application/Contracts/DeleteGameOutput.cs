namespace FCG.Games.Application.Contracts;
public class DeleteGameOutput
{
    public bool Success { get; set; }

    public static DeleteGameOutput Ok() => new DeleteGameOutput { Success = true };
    public static DeleteGameOutput Failed() => new DeleteGameOutput { Success = false };
}
