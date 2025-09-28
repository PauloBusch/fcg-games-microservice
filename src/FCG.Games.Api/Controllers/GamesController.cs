namespace FCG.Games.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{key:guid}")]
    [ActionName(nameof(GetGameByIdAsync))]
    public async Task<ActionResult<GetGameResponse>> GetGameByIdAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        var input = new GetGameInput(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
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

    [HttpGet("catalogs/{catalogKey:guid}/games/{gameKey:guid}/evaluations")]
    public async Task<ActionResult<GetGameEvaluationsResponse>> GetGameEvaluationsAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        CancellationToken ct
    )
    {
        var input = new GetGameEvaluationsInput(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    [HttpGet("catalogs/{catalogKey:guid}/games/{gameKey:guid}/download")]
    public async Task<ActionResult<GetGameDownloadResponse>> GetGameDownloadAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        CancellationToken ct
    )
    {
        var input = new GetGameDownloadInput(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    [HttpPut("catalogs/{catalogKey:guid}/games/{gameKey:guid}")]
    public async Task<ActionResult<UpdateGameResponse>> UpdateGameAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        [FromBody] UpdateGameRequest request,
        CancellationToken ct
    )
    {
        var input = new UpdateGameInput(catalogKey, gameKey, request.Title, request.Description);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }
}
