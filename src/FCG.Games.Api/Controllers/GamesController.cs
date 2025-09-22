namespace FCG.Games.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreateGameResponse>>> GetGamesAsync(CancellationToken ct)
    {
        // TODO: Implementar listagem de jogos
        return Ok(Array.Empty<CreateGameResponse>());
    }

    [HttpGet("{key:guid}")]
    public async Task<ActionResult<CreateGameResponse>> GetGameByIdAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        // TODO: Implement this action
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CreateGameResponse>> CreateGameAsync(
        [FromBody] CreateGameRequest request,
        CancellationToken ct
    )
    {
        var input = request.ToUseCase();
        var output = await mediator.Send(input, ct);
        var response = output.ToResponse();
        return CreatedAtAction(
            nameof(GetGameByIdAsync),
            new { key = response.Key },
            response
        );
    }

    [HttpPut("{key:guid}")]
    public async Task<IActionResult> UpdateGameAsync(
        [FromRoute] Guid key,
        [FromBody] CreateGameRequest request,
        CancellationToken ct
    )
    {
        // TODO: Implementar atualização de jogo
        return NoContent();
    }

    [HttpDelete("{key:guid}")]
    public async Task<IActionResult> DeleteGameAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        // TODO: Implementar exclusão de jogo
        return NoContent();
    }
}
