namespace FCG.Games.Domain._Common.Exceptions;

public abstract class FcgException(string? message = default)
    : Exception(message) { }
