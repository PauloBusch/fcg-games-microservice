namespace FCG.Games.Api.Controllers;

[Authorize]
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
    [Authorize(Policy = Policies.OnlyAdmin)]
    public async Task<ActionResult<CreateGameResponse>> CreateGameAsync(
        [FromBody] CreateGameRequest request,
        CancellationToken ct
    )
    {
        var input = new CreateGameInput(
            request.Key,
            request.Title,
            request.Description,
            request.Catalog.ToUseCase()
        );

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return CreatedAtAction(
            nameof(GetGameByIdAsync),
            new { key = response.Key },
            response
        );
    }
    
    [HttpGet("{key:guid}/evaluations")]
    public async Task<ActionResult<GetGameEvaluationsResponse>> GetGameEvaluationsAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        var input = new GetGameEvaluationsInput(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }
    
    [HttpGet("{key:guid}/download")]
    public async Task<ActionResult<GetGameDownloadResponse>> GetGameDownloadAsync(
        [FromRoute] Guid key,
        CancellationToken ct
    )
    {
        var input = new GetGameDownloadInput(key);

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }
    
    [HttpPut("{key:guid}")]
    [Authorize(Policy = Policies.OnlyAdmin)]
    public async Task<ActionResult<UpdateGameResponse>> UpdateGameAsync(
        [FromRoute] Guid key,
        [FromBody] UpdateGameRequest request,
        CancellationToken ct
    )
    {
        var input = new UpdateGameInput(
            key,
            request.Title,
            request.Description
        );

        var output = await mediator.Send(input, ct);

        var response = output.ToResponse();

        return Ok(response);
    }
}
