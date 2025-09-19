namespace FCG.Games.Application._Common;

public interface IUseCase<in TInput, TOutput>
    : IRequestHandler<TInput, TOutput>
    where TInput : IUseCaseInput<TOutput>
{
    new Task<TOutput> Handle(TInput input, CancellationToken ct);
}

public interface IUseCaseHandler<in TInput>
    : IRequestHandler<TInput>
    where TInput : IUseCase
{
    new Task Handle(TInput input, CancellationToken ct);
}
