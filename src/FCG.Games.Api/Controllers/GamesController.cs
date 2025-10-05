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

    /// <summary>
    /// This endpoint retrieves the evaluations for a specific game within a specific catalog.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{key:guid}/evaluations")]
    public async Task<ActionResult<GetGameEvaluationsResponse>> GetGameEvaluationsAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        var input = GamesRequestToUseCaseMapping.ToUseCase(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// This endoint retrieves the download information for a specific game within a specific catalog.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{key:guid}/download")]
    public async Task<ActionResult<GetGameDownloadResponse>> GetGameDownloadAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        var input = GamesRequestToUseCaseMapping.ToDownloadUseCase(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// This endpoint updates the details of a specific game.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("{key:guid}")]
    public async Task<ActionResult<UpdateGameResponse>> UpdateGameAsync(
        [FromRoute] Guid key,
        [FromBody] UpdateGameRequest request,
        CancellationToken ct
        
    )
    {
        var input = request.ToUseCase(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }
}
