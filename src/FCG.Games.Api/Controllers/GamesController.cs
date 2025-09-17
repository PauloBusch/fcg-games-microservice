namespace FCG.Games.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    [HttpPost]
    public async Task<CreateGameResponse> CreateGame(
        [FromServices] CreateGameUseCase useCase,
        [FromBody] CreateGameRequest request,
        CancellationToken ct
    ) => await useCase.ExecuteAsync(request, ct);
}
