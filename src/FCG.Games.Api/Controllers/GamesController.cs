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
}
