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

    /// <summary>
    /// Search game by title within a specific catalog by ID
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="title"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("/catalogs/{catalogKey}/games")]
    [ActionName(nameof(GetGameByTitleAsync))]
    public async Task<ActionResult<GetGameResponse>> GetGameByTitleAsync(
        [FromRoute] Guid catalogKey,
        [FromQuery] string title,
        CancellationToken ct
    )
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest("Title is required.");

        var input = GamesRequestToUseCaseMapping.ToUseCase(catalogKey, title);

        var output = await mediator.Send(input, ct);

        if (output.IsNotFound)
            return NotFound();

        var response = GetGameResponse.FromOutput(output);

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
    /// Post a evaluation for a game in a specific catalog
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="gameKey"></param>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("/catalogs/{catalogKey}/games/{gameKey}/evaluations")]
    [ActionName(nameof(CreateEvaluationAsync))]
    public async Task<ActionResult<CreateEvaluationResponse>> CreateEvaluationAsync(
    [FromRoute] Guid catalogKey,
    [FromRoute] Guid gameKey,
    [FromBody] CreateEvaluationRequest request,
    CancellationToken ct)
    {
        if (request is null)
            return BadRequest("Request body is required.");

        var input = GamesRequestToUseCaseMapping.ToUseCase(catalogKey, gameKey, request);

        var output = await mediator.Send(input, ct);

        if (output.IsNotCreated)
            return UnprocessableEntity("Evaluation could not be created.");

        var response = new CreateEvaluationResponse
        {
            EvaluationId = output.EvaluationId,
            CreatedAt = output.CreatedAt
        };

        return CreatedAtAction(nameof(CreateEvaluationAsync), new { catalogKey, gameKey }, response);
    }

    /// <summary>
    /// This endpoint retrieves the evaluations for a specific game within a specific catalog.
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="gameKey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("catalogs/{catalogKey:guid}/games/{gameKey:guid}/evaluations")]
    public async Task<ActionResult<GetGameEvaluationsResponse>> GetGameEvaluationsAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        CancellationToken ct
    )
    {
        var input = GamesRequestToUseCaseMapping.ToUseCase(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// This endoint retrieves the download information for a specific game within a specific catalog.
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="gameKey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("catalogs/{catalogKey:guid}/games/{gameKey:guid}/download")]
    public async Task<ActionResult<GetGameDownloadResponse>> GetGameDownloadAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        CancellationToken ct
    )
    {
        var input = GamesRequestToUseCaseMapping.ToDownloadUseCase(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// This endpoint updates the details of a specific game.
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="gameKey"></param>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPut("catalogs/{catalogKey:guid}/games/{gameKey:guid}")]
    public async Task<ActionResult<UpdateGameResponse>> UpdateGameAsync(
        [FromRoute] Guid catalogKey,
        [FromRoute] Guid gameKey,
        [FromBody] UpdateGameRequest request,
        CancellationToken ct
    )
    {
        var input = request.ToUseCase(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// Delete game by game ID within a specific catalog by ID
    /// </summary>
    /// <param name="catalogKey"></param>
    /// <param name="gameKey"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("/catalogs/{catalogKey}/games/{gameKey}")]
    [ActionName(nameof(DeleteGameAsync))]
    public async Task<IActionResult> DeleteGameAsync(
    [FromRoute] Guid catalogKey,
    [FromRoute] Guid gameKey,
    CancellationToken ct)
    {
        var input = GamesRequestToUseCaseMapping.ToDeleteGameUseCase(catalogKey, gameKey);

        var output = await mediator.Send(input, ct);

        if (!output.Success)
            return NotFound();

        return NoContent();
    }

}
